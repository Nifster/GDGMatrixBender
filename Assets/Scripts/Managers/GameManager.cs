using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public int playerLives=3;

    public GameObject player;
    public UnityEngine.UI.Text DeadPlayerText;
    public UnityEngine.UI.Text GameOverText;
    public UnityEngine.UI.Text LevelClearText;
    public UnityEngine.UI.Text lives; 

    void Start()
    {
        playerLives = PlayerPrefs.GetInt("playerLives", 0);
        if (playerLives <= 0)
            playerLives = 3;
        if (playerLives == 1)
            lives.text = "Last Life!";
        else
            lives.text = playerLives.ToString();
        DeadPlayerText.enabled = false;
        GameOverText.enabled = false;
    }

    void FixedUpdate()
    {try
        {
            if (player != null)
            {
                
                if (Input.GetKey(KeyCode.R))
                {
                    playerLives -= 1;
                    PlayerPrefs.SetInt("playerLives", playerLives);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else
            {
                if (playerLives==1)
                {
                    GameOverText.enabled = true;
                }
                else
                {
                    DeadPlayerText.enabled = true;
                }
                
                    
                if (Input.GetKey(KeyCode.R))
                {
                    playerLives -= 1;
                    PlayerPrefs.SetInt("playerLives", playerLives);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
        catch (MissingReferenceException e)
        {
            


        }

    }
}
