using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject enemyNew;

    // Start is called before the first frame update
    void Start()
    {
        //Create and set the players Y to -3 and repeat void CreateEnemy
        Instantiate(player, transform.position = new Vector3(0, -3, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("CreateEnemyNew", 1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CreateEnemy()
    {
        //Create enemy at the top of the screen
        Instantiate(enemy, new Vector3(Random.Range(-9f, 9f), 8f, 0), Quaternion.identity);
    }

    void CreateEnemyNew()
    {
        Instantiate(enemyNew, new Vector3(Random.Range(-9f, 9f), -12f, 0), Quaternion.identity);
    }
}
