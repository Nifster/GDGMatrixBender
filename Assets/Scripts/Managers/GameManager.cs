using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
	public static GameManager instance {get; private set;}

    public int playerLives=3;

    public GameObject player;
    public UnityEngine.UI.Text DeadPlayerText;
    public UnityEngine.UI.Text GameOverText;
    public UnityEngine.UI.Text LevelClearText;
    public UnityEngine.UI.Text lives; 

	public GameObject prefabToSpawn = null;
	private Vector2 spawn_point_1 = new Vector2(4.22f,-7.49f);
	private Vector2 spawn_point_2 = new Vector2(13.77f,-11.2f);
	private Vector2 spawn_point_3 = new Vector2(4.22f,-15.78f);
	private Vector2 spawn_point_4 = new Vector2(-5.08f,-11.2f);
	private float nextSpawnTime;
	private float spawnCooldown = 3f;
	private int spawnCount = 1;
	private int spawnLimit = 15;

	void Awake() {
		instance = this;
	}

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

	public void EnemyKilled()
	{
		spawnCount--;
	}

	private void SpawnPrefab() {
		int num = Random.Range(1, 4);
		float spawnX;
		float spawnY;
		if (num <= 1) {
			spawnX = spawn_point_1.x;
			spawnY = spawn_point_1.y;
		} else if (num <= 2 && num > 1) {
			spawnX = spawn_point_2.x;
			spawnY = spawn_point_2.y;
		} else if (num <= 3 && num > 2) {
			spawnX = spawn_point_3.x;
			spawnY = spawn_point_3.y;
		} else {
			spawnX = spawn_point_4.x;
			spawnY = spawn_point_4.y;
		}

		Instantiate(prefabToSpawn, new Vector3(spawnX, spawnY, 0), prefabToSpawn.transform.rotation);
		spawnCount++;
	}

	void Update() {
		// Controls the spawning of collectibles
		if (prefabToSpawn != null && Time.time > nextSpawnTime) {
			nextSpawnTime += spawnCooldown;

			if (spawnCount < spawnLimit)
				SpawnPrefab();
		}
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
