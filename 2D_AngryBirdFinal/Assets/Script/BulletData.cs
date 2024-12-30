using System;
using System.Collections;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    Vector2 velocity;
    Vector2 beforOriginPos;
    float fullTime = 0;
    
    static int currentIndex = 0;
    
    static Sprite bulletSprite;
    static string bulletName;
    static int bulletDamage;
    //static int bulletStickTime;
    static Color bulletLineColor;
    static ParticleSystem bulletEffect;
    //원본 데이터 .. 이렇게 구분하는게 맞는지 모르겠음..ㅠ
    public Sprite currentBulletSprite { get; private set; }
    public string currentBulletName { get; private set; }
    public int currentBulletDamage { get; private set; }
    //public int currentBulletStickTime { get; private set; }
    public Color currentBulletLineColor { get; private set; }
    public ParticleSystem currentbulletEffect { get; private set; }
    
    [SerializeField] Sprite defaultSprite;
    [SerializeField] TrailRenderer trail;
    SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem defaultEffect;
    [SerializeField] GameObject tempGameObject;
    public bool Isdestroy { get; private set; } = false;

    void Start()
    {
        Isdestroy = false;
        if (currentIndex == 0)
        {
            bulletSprite = defaultSprite;
            bulletName = "default";
            bulletDamage = 1;
            //bulletStickTime = 0;
            bulletLineColor = Color.cyan;
            bulletEffect = defaultEffect;
        }
        
        currentBulletSprite = bulletSprite;
        currentBulletName = bulletName;
        currentBulletDamage = bulletDamage;
        
        //currentBulletStickTime = bulletStickTime;
        currentBulletLineColor = bulletLineColor;
        currentbulletEffect = bulletEffect;
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = currentBulletSprite;
        trail.material.color = currentBulletLineColor; //다른건 돌아가는데 얘만 안돌아가는것을 보면..뭔가 잇겟지
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        GameObject temp = Instantiate(tempGameObject, gameObject.transform.position, Quaternion.identity);
        temp.GetComponent<showParticles>().ChangeEffect(currentbulletEffect);
        temp.GetComponent<showParticles>().PlayEffect();
        //currentbulletEffect이었음 디버깅중
        //temp.GetComponent<showParticles>().ps.Play();
        //temp.GetComponent<showParticles>().PlayEffect(); 요 함수는 도는 걸로 확인됨.
        
        // 원하는 오브젝트의 파티클 시스템의 주소를 불러와서
        //ps = currentbulletEffect; 파티클 시스템을 다른 
        
        Destroy(this.gameObject);
        Isdestroy = true;
        if (collision.gameObject.CompareTag("Mop")) //여기가 왜 여러번 돌지?
        {
            beforOriginPos = this.gameObject.transform.position;
            //StartCoroutine(PauseOnCollisionCourtine());
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        GameObject temp = Instantiate(tempGameObject, gameObject.transform.position, Quaternion.identity);
        temp.GetComponent<showParticles>().ChangeEffect(currentbulletEffect);
        temp.GetComponent<showParticles>().PlayEffect();
        
        if (other.CompareTag("Item"))
        {
            ++currentIndex;
            
            bulletName = other.GetComponent<ItemBulletData>().bulletName;
            bulletDamage = other.GetComponent<ItemBulletData>().bulletDamage;
            bulletSprite = other.GetComponent<ItemBulletData>().bulletImage;
            //bulletStickTime = other.GetComponent<ItemBulletData>().bulletStickTime;
            bulletLineColor = other.GetComponent<ItemBulletData>().bulletLineColor;
            bulletEffect = other.GetComponent<ItemBulletData>().bulletItemEffect;
        }
        
        Destroy(this.gameObject);
    }
}

