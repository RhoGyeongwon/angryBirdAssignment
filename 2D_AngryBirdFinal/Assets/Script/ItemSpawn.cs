using System.Collections;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] BoxCollider2D ItemSpawnPostion;
    [SerializeField] GameObject Item;
    private float time;
    void Start()
    {
        StartCoroutine(SpawnItem());
    }
    
    IEnumerator SpawnItem()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Item == null)
            {
                yield break;
            }
            float time = Random.Range(1f, 2f);
            yield return new WaitForSeconds(time);
            Instantiate(Item, new Vector2(Random.Range(ItemSpawnPostion.bounds.min.x, ItemSpawnPostion.bounds.max.x), ItemSpawnPostion.bounds.min.y),Quaternion.identity);
        }
    }
}
