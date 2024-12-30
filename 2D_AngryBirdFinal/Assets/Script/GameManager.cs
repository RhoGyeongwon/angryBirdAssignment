using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject BossMop;
    [SerializeField] GameObject GameWin;
    [SerializeField] GameObject GameLose;
    [SerializeField] GameObject HPUI;

    // Update is called once per frame
    void Update()
    {
        if (BossMop.GetComponent<MopInfo>().currentHp == 0)
        {
            HPUI.SetActive(false);
            GameWin.SetActive(true);
        }
    }
}
