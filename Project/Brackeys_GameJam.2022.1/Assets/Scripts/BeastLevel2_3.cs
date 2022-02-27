using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastLevel2_3 : MonoBehaviour
{
    [SerializeField] private Transform batya;
    [SerializeField] private GameObject killBox;
    [SerializeField] private GameObject hideText;
    [SerializeField] private AudioClip scarySound;
    [SerializeField] public AudioSource audioSource;


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
        Instantiate(hideText, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
        Instantiate(killBox, batya.position, Quaternion.identity);
        ScarySound();



        //if (!collision.gameObject.GetComponent<PlayerController>().isHiding)
        //{
        //    StartCoroutine(killBox.GetComponent<DeathController>().DeathSequence(collision.gameObject.GetComponent<PlayerController>()));
        //}

    }

    public void ScarySound()
    {
        audioSource.PlayOneShot(scarySound);
    }

}
