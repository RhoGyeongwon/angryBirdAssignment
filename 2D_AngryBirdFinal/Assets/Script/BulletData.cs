using System;
using UnityEngine;

public enum EBulletType
{
    Normal,
    Speed,
    Fire,
    Bomb
}
public class BulletData : MonoBehaviour
{
    [NonSerialized] Bullet currentBullet;
}
