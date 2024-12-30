using System.Collections.Generic;
using UnityEngine;

public class ItemBulletData : MonoBehaviour
{
    [Header("BulletList")]
    [SerializeField] List<Bullet> bulletList;
    int currentBulletIndex = 0;
    [SerializeField] ParticleSystem particle;
    
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip getItemSound;
    public Sprite bulletImage { get; private set; }
    public string bulletName { get; private set; }
    public int bulletDamage { get; private set; }
    //public int bulletStickTime { get; private set; }
    public Color bulletLineColor { get; private set; }
    public ParticleSystem bulletItemEffect { get; private set; }

    void Start()
    {
        if (bulletList.Count == 0)
        {
            return;
        }
        
        currentBulletIndex = Random.Range(0, bulletList.Count);
        
        bulletImage = bulletList[currentBulletIndex].itemImage;
        bulletDamage = bulletList[currentBulletIndex].itemDamage;
        bulletName = bulletList[currentBulletIndex].itemName;
        //bulletStickTime = bulletList[currentBulletIndex].itemStickTime;
        bulletLineColor = bulletList[currentBulletIndex].itemColor;
        bulletItemEffect = bulletList[currentBulletIndex].itemeffect;
        //이거 그냥 name으로 해버리면 scriptableObject의 그냥 name으로 들어가니까 조심하장..
        //문자열은 웬만하면 이래서 안쓰는게 좋구만...ㅠ

        bulletList.RemoveAt(currentBulletIndex);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //GetComponent<Rigidbody2D>().isKinematic = true;
        //만약 트리거 처리가 안되어있다면
        //외부 물리 작용을 더 이상안할뿐이지 아예 운동량이 끝나는건 아니다. 그래서 설정해도 버섯은 멀리 날아간다. 
        //이미 값을 받아버린 상태라서. 그래서 velocity값을 0으로 만들어줘야한다.
        //사실 가장 좋은 방법은 그냥 trigger형태로 바꿔주는게 나을듯.
        //만약 총알이 not Trigger이고 bullet Tigger이면 둘 다 충돌 감지할 때는 둘 다 trigger 함수로 확인해야한다.
        if (other.CompareTag("Bullet"))
        {
            audioSource.PlayOneShot(getItemSound);
            particle.Play();
        }
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        Destroy(this.gameObject, 1f);
    }
}
