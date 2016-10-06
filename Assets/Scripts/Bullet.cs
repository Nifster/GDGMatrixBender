using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {

        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called when some other object enters your hitbox. (note: isTrigger must be on!)
    void OnTriggerEnter2D(Collider2D other)
    {

        // First we need to check whether the thing it's colliding with is the enemy.
        // There are a few methods to do this. I'll be using Method 3.

        // Method 1: Check object tag (I'm not using this)
        //if (other.tag != "whateveryounamedyourtag") Destroy(other.gameObject);

        // Method 2: Check whether object has the script component "Enemy" attached. (I'm not using this)
        //var enemyComponent = other.GetComponent<Enemy>();
        //if(enemyComponent == null) Destroy(other.gameObject);

        // Method 3: Check object name (I'm using this)
        if (other.name.ToLower() == "enemy(clone)")
        {
            //kill the enemy
            Destroy(other.gameObject);

            //add score
            if (gameManager != null)
            {
                gameManager.EnemyKilled();
            }

        }

        if (other.tag.ToLower()=="enemy")
        {
            //kill the enemy
            Destroy(other.gameObject);

            //add score
            if (gameManager != null)
            {
                gameManager.EnemyKilled();
            }

        }

        if (other.name.ToLower() == "net")
        {
            //Add 1 bullet to count
            other.gameObject.GetComponentInParent<Controller>().bulletCount += 1;      
        }

        Destroy(this.gameObject);

        if (other.name.ToLower() == "player")
        {
        
            other.gameObject.GetComponentInParent<Controller>().healthBarSlider.value -= (float)20;
        }

        Destroy(this.gameObject);


    }
}
