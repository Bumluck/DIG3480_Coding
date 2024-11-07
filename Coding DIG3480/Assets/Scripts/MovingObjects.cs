using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{

    public int myType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myType == 1)
        {
            //Bullet movement myType = 1
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 10f);
        }
        else if (myType == 2)
        {
            //Enemy movement myType = 2
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 3f);
        }
        else if (myType == 3)
        {
            //Cloud movement myType = 3
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * Random.Range(2f, 6f));
        }
        else if (myType == 4)
        {
            //Coin Movement myType = 4
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 6f);
        } 

        if ((transform.position.y > 9f || transform.position.y <= -9f || transform.position.x > 11f || transform.position.x < -11f) && myType !=3)
        {
            Destroy(this.gameObject);
        }  else if (transform.position.y <= -9f && myType == 3)
        {
            transform.position = new Vector3(Random.Range(-10f, 10f), 9f, 0);
        }

    }
}
