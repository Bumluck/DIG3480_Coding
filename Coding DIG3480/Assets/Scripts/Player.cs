using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject explosion;
    public GameObject Bullet;

    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        horizontalScreenLimit = 11.5f;
        verticalScreenLimit = 7.5f;
        lives = 3;
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
        // Pressing space spawns a bullet which moves upwards along Y
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    public void LoseALife()
    {
        //lives = lives -1;
        //lives -= 1;
        lives--;
        if (lives ==0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
