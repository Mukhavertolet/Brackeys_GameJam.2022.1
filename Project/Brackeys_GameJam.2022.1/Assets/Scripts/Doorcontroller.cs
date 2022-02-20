using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorcontroller : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[2];

    

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Character" && collision.gameObject.GetComponent<PlayerController>().hasKey == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            collision.otherCollider.isTrigger = true;
        }
    }
    

}
