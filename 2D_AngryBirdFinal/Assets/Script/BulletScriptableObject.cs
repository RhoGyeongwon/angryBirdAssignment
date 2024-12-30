using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Scriptable Object/Bullet", order = int.MaxValue)]
public class Bullet : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public int itemDamage;
    //public int itemStickTime;
    public Color itemColor;
    public ParticleSystem itemeffect;
}
