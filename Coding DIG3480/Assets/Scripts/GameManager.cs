using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject coin;
    public GameObject cloud;
    public GameObject powerup;

    public int cloudSpeed;

    private bool isPlayerAlive;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI RestartText;
    public TextMeshProUGUI powerupText;

    private int score;


    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the Player at position (0, -3, 0) and store the new instance in the "playerInstance" variable
        GameObject playerInstance = Instantiate(player, new Vector3(0, -3, 0), Quaternion.identity);
        
        //Gets Player Component and sets Player.cs "livesText" equal to GameManager.cs "livesText"
        playerInstance.GetComponent<Player>().livesText = livesText;

        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("CreateCoin", 1f, 7f);
        StartCoroutine(CreatePowerup());
        CreateSky();
        
        score = 0;
        scoreText.text = "Score: " + score;
        cloudSpeed = 1;
        isPlayerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
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

    IEnumerator CreatePowerup()
    {
        if (isPlayerAlive == true)
        {
            Instantiate(powerup, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4f, 7f));
            StartCoroutine(CreatePowerup());
        }
        else if (isPlayerAlive == false)
        {

        }
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

    public void GameOver()
    {
        isPlayerAlive = false;
        CancelInvoke();
        GameOverText.gameObject.SetActive(true);
        RestartText.gameObject.SetActive(true);
        cloudSpeed = 0;
    }

    void Restart()
    {

        if(Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
        {
            //Restart the game
            SceneManager.LoadScene("Game");
        }
    }
    public void UpdatePowerupText(string whichPowerup)
    {
        powerupText.text = whichPowerup;
    }
}
