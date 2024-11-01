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
    private float speed;
    private int lives;

    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
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
        transform.Translate(new Vector3(horizontalInput, 0, 0) * Time.deltaTime * speed);

        //if the player moves off camera
        if (transform.position.x > 11.5f || transform.position.x <= -11.5f)
        {
            //On the X plane, move them to the other side of the screen
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);

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


}
