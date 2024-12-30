using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    //몬스터가 가까이 왔을 때만 체력이 단다.
    public int currentHp { get; private set; } // 현재 체력
    const int MaxHp = 100;
    int damageValue = 1;
    
    void Start()
    {
        currentHp = MaxHp;
    }

    void Update()
    {
        if (MaxHp <= 0) //게임 오버
        {
            Destroy(this.gameObject);
        }
    }
}
