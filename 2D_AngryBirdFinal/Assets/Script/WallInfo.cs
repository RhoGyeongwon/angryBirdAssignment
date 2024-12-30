using System.Collections;
using UnityEngine;

public class WallInfo : MonoBehaviour
{
    [SerializeField] int maxHp;
    
    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip Hit;
    int currentHP;
    void Start()
    {
        currentHP = maxHp;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 3f )
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 3f);
            }
            
            audioSource.PlayOneShot(Hit);
            currentHP -= collision.gameObject.GetComponent<BulletData>().currentBulletDamage;
            
            if (currentHP <= 0)
            {
                StartCoroutine(disappear());
            }
        }
    }

    IEnumerator disappear()
    {
        while (true)
        {
            if (gameObject.transform.localScale.x < 0.025f)
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.5f);
                Destroy(gameObject);
                yield break;
            }
            
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.9f, gameObject.transform.localScale.y * 0.9f, gameObject.transform.localScale.z * 0.9f);
            yield return null;
        }
    }
}
