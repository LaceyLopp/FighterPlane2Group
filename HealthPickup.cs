using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainALife : MonoBehaviour
{

    private GameManager gameManager;

    public int gainALife = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {

        if (whatDidIHit.tag == "Player")
        {



            gameManager.GainALife(gainAlife);
            Destroy(this.gameObject);
        }


    }
}
