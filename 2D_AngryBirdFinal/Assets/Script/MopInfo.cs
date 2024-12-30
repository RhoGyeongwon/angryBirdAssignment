using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum EMopType
{
    Boss,
    Minion
}
public class MopInfo : MonoBehaviour
{
    [SerializeField] public EMopType mopType;
    [SerializeField] int MaxHp;
    public int currentHp { get; private set; }
    int hitValue;
    float fullTime = 0;
    
    [Header("UI")]
    [SerializeField] GameObject profileUI;
    [SerializeField] Image profile;
    [SerializeField] Sprite profileImg;
    [SerializeField] Slider sliderHP;
    float powerPercent;
    
    [Header("Gauge")]
    [SerializeField] SpriteRenderer backGround;
    Color bgColor;
    
    [SerializeField] Animator Mopanimator;
    
    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip Hit;
    [SerializeField] AudioClip Dead;
    //[SerializeField] AudioClip Attack;
    void Start()
    {
        bgColor = backGround.color;
        currentHp = MaxHp;
    }

    void Update()
    {
        if (mopType == EMopType.Minion)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-3.2f, 0);
        }
        
        if (currentHp <= 0)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            Debug.Log(gameObject.GetComponent<Rigidbody2D>().isKinematic);
            if (mopType == EMopType.Minion) //UI 없애기
            {
                Destroy(gameObject, 0.5f);
                profileUI.gameObject.SetActive(false);
            }
            currentHp = 0;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet")); //자꾸 땅 태그인데 이게 도는 현상이 발생
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                return;
            }

            if (mopType == EMopType.Minion)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    gameObject.transform.position = gameObject.transform.position + new Vector3(10f, 0, 0);
                    return;
                }
            
                if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 20f )
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 20f);
                }
            }

            Debug.Log(other.gameObject.name);
            profileUI.gameObject.SetActive(true);
            audioSource.PlayOneShot(Hit);
            profile.sprite = profileImg;
            sliderHP.value = currentHp / (float)MaxHp;
            hitValue = other.gameObject.GetComponent<BulletData>().currentBulletDamage;
            StartCoroutine(PauseOnCollisionCourtine());
        }
    }
    
    IEnumerator PauseOnCollisionCourtine()
    {
        Mopanimator.SetTrigger("IsHit");
        backGround.color = Color.gray;
        
        float plusTime = 0f;

        for (int i = 0; i < hitValue; i++)
        {
            --currentHp;
            yield return new WaitForSeconds(0.06f);
        }

        if (currentHp <= 0)
        {
            audioSource.PlayOneShot(Dead);
        }
        backGround.color = bgColor;
    }
}
