using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastLevel2_3 : MonoBehaviour
{
    [SerializeField] private Transform batya; 
    [SerializeField] private GameObject killBox;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Collided(GameObject player)
    {
        Debug.Log("Player collided");
        Instantiate(killBox, batya.position, Quaternion.identity);


        //if (!collision.gameObject.GetComponent<PlayerController>().isHiding)
        //{
        //    StartCoroutine(killBox.GetComponent<DeathController>().DeathSequence(collision.gameObject.GetComponent<PlayerController>()));
        //}

    }



}
