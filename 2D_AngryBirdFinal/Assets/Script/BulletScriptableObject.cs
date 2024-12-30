using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Scriptable Object/Bullet", order = int.MaxValue)]
public class Bullet : ScriptableObject
{
    [SerializeField] string bulletName;
    [SerializeField] int Damage;
    [SerializeField] int  CooldownTime;
    [SerializeField] float  gravity;
}
