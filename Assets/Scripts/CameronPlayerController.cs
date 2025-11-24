using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameronPlayerController : MonoBehaviour
{

    public int lives;
    private float speed;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject shieldPrefab;
    public AudioSource powerUpSound;
    


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        powerUpSound = GameObject.Find("GameManager").GetComponent<AudioSource>();
        lives = 3;
        speed = 5.0f;
        gameManager.ChangeLivesText(lives);
        shieldPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        //if you have a shield first, lose the shield first and have no life decrease
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    //
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            //shield activate
            shieldPrefab.SetActive(true);

            //depending on how we implement text and sounds, this may not be used:
            //gameManager.ManagePowerupText(3);
            powerUpSound.Play();
        }
    }
    //
    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        //if ever below the screen size, set position back to screensize
        if (transform.position.y <= -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenSize, 0);
        }
        //if above the screensize, set position back to screensize
        if (transform.position.y > verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, verticalScreenSize, 0);
        }

        //if above half the screen, set vertical position back to half the screen
        if (transform.position.y > (verticalScreenSize/2))
        {
            transform.position = new Vector3(transform.position.x, verticalScreenSize/2, 0);

        }


    }
}
