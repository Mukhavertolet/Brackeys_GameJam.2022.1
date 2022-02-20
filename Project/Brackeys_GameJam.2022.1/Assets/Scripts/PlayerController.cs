using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasKey = false;
    
    //public GameObject door;

   
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Key")
        {
            hasKey = true;
            Destroy(other.gameObject);
        }
    }

}
