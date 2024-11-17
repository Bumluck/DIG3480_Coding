using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    // variables
    //1. access level: public or private
    //2. type: int(e.g., 2, 4, 123, etc.) float (e.g, 2.5, 6.78, etc.)
    //3. name: (1) start w/ lowercase (2) if it is multiplle words, then the other words start wih uppercase and written together
    //4. optional: give it an initial value

    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private float horizontalScreenLimit;
    private float verticalScreenLimit;
    
    private int lives;
    private int shooting;

    /*NullReferenceException including UnityEditor.Graphs.Edge.WakeUp will be in the console for some reason
     it is probably because of the hasShield boolean not being assigned to an object in Unity Editor but the shield
    still works so I wouldn't worry about it*/
    private bool hasShield;

    public GameManager gameManager;

    public GameObject explosion;
    public GameObject Bullet;
    public GameObject thruster;
    public GameObject shield;
    
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        horizontalScreenLimit = 11.5f;
        verticalScreenLimit = 7.5f;
        lives = 3;
        UpdateLivesText();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shooting = 1;
        hasShield = false;
    }

    void Update()
    {
        Moving();
        Shooting();
    }

    // Update is called once per frame
    void Moving()
    {
        // Player can only move horizontally, Starting Y position set in GameManager
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);


        //if the player moves off camera
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            //On the X plane, move them to the other side of the screen
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (shooting)
            {
                case 1:
                    //normal shot
                        Instantiate(Bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    //Double shot
                        Instantiate(Bullet, transform.position + new Vector3(-.5f, 1, 0), Quaternion.identity);
                        Instantiate(Bullet, transform.position + new Vector3(.5f, 1, 0), Quaternion.identity);
                    break;
                case 3:
                    //Triple shot
                        Instantiate(Bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                        Instantiate(Bullet, transform.position + new Vector3(-.5f, 1, 0), Quaternion.Euler(0, 0, 30));
                        Instantiate(Bullet, transform.position + new Vector3(.5f, 1, 0), Quaternion.Euler(0, 0, -30));
                    break;
            }
        }
        
    }

    void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives;
    }

    public void LoseALife()
    {
        if (hasShield == false)
        {
            //lives = lives -1;
            //lives -= 1;
            lives--;
            UpdateLivesText();
        } else if (hasShield == true)
        {
            //If Player has shield, then they cannot lose a life.
        }


        if (lives == 0)
        {
            gameManager.GameOver();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }

    IEnumerator SpeedPowerDown()
    {
        //Return speed to normal state
        yield return new WaitForSeconds(3f);
        speed = 6f;
        thruster.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    IEnumerator ShootingPowerDown()
    {
        //Return shooting to normal state
        yield return new WaitForSeconds(4f);
        shooting = 1;
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    IEnumerator ShieldPowerDown()
    {
        //Turn off shield
        yield return new WaitForSeconds(4f);
        shield.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
        hasShield = false;
        gameManager.PlayPowerDown();
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Powerup")
        {
            gameManager.PlayPowerUp();
            int powerupType = Random.Range(1, 5);
            switch(powerupType)
            {
                case 1:
                    //speed powerup
                    speed = 9f;
                    gameManager.UpdatePowerupText("Speed Boost!");
                    thruster.gameObject.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    //double shot
                    gameManager.UpdatePowerupText("Double Shot!");
                    shooting = 2;
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 3:
                    //triple shot
                    gameManager.UpdatePowerupText("Triple Shot!");
                    shooting = 3;
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 4:
                    //shield
                    gameManager.UpdatePowerupText("Shields!");
                    shield.gameObject.SetActive(true);
                    hasShield = true;
                    StartCoroutine(ShieldPowerDown());
                    break;


            }
        }
        Destroy(whatIHit.gameObject);
    }

}
