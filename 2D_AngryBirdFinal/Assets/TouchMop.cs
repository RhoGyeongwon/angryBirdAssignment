using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMop : MonoBehaviour
{
    [SerializeField] BoxCollider2D box;

    void CollisionEnter2D(Collision2D collision)
    {
        box.enabled = false;
        Debug.Log(box.enabled);
    }
    
    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     box.enabled = true;
    // }
}
