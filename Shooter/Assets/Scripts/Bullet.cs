using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float power = 0f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
    }
}
