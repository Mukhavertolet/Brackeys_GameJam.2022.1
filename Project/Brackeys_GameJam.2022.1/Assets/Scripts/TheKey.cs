using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheKey : MonoBehaviour
{
    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (gameManager.playerHasTheKey)
            Destroy(gameObject);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
