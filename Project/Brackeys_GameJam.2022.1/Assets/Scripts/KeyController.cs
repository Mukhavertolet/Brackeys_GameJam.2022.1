using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    //[SerializeField]private ParticleSystem keyParticle;
    public GameObject gameObject;


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (LayerMask.LayerToName(collision.gameObject.layer)=="Player")
        {
            Instantiate(gameObject, transform.position, transform.rotation);
        }
    }
}
