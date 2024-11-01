using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Bullets will move upward
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 10f);

        //Once off Screen bullet will be destroyed
        if (transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }
    }
}
