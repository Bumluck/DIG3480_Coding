using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject coin;
    public GameObject cloud;
    
    private int score;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the Player at position (0, -3, 0) and store the new instance in the "playerInstance" variable
        GameObject playerInstance = Instantiate(player, new Vector3(0, -3, 0), Quaternion.identity);
        
        //Gets Player Component and sets Player.cs "livesText" equal to GameManager.cs "livesText"
        playerInstance.GetComponent<Player>().livesText = livesText;

        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("CreateCoin", 1f, 7f);
        CreateSky();
        
        score = 0;
        scoreText.text = "Score: " + score;
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

    void CreateCoin()
    {
        Instantiate(coin, new Vector3(-11f, Random.Range(-4f, 4f), 0), Quaternion.identity);
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }

    public void EarnScore(int howMuch)
    {
        score = score + howMuch;
        scoreText.text = "Score: " + score;
    }


}
