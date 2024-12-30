using System;
using Unity.VisualScripting;
using UnityEngine;

public class MopInfo : MonoBehaviour
{
    //1. 몬스터가 이동할때는 addForce 값으로 이동하게 만들기
    //2. 튕길때는 왔던 velocity * -1 반대로 튕기게 만들기(선생님이 주신 그 뭐냐.. 그 총쏘는거..그거 코드 찾아서 참고하자^^
    public int CurrentHp { get; private set; }
    const int MaxHp = 100;
    float Speed = 0.05f;
    Rigidbody2D rb;
    public static int damageValue { get; private set; }
    private bool isEnter;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageValue = 10;
        CurrentHp = MaxHp;
    }

    //코드의 문제점
    //일단 update로 실시간으로 velocity값을 받아 앞으로 나가게 되어있
    void Update()
    {
        if (CurrentHp <= 0) //게임 끝남
        {
            Destroy(this.gameObject);
        }

        rb.velocity = new Vector2(Speed, rb.velocity.y);
        transform.position += Vector3.left * Time.deltaTime;
        //나중을 위한 코드
        //rb.velocity = new Vector2(Speed, rb.velocity.y);
        //rb.AddForce(Vector2.left, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"));
        {
            //rb.AddForce(rb.velocity * -500f, ForceMode2D.Impulse);
            transform.position += -Vector3.left;
            Speed += 0.1f;
        }
    }
}
