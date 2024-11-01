using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Enemy has spawned");
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy will move down the screen
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 3f);
        //Enemy will be destroyed once off screen
        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }
    }
}
