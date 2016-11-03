using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float activeRange;

    [SerializeField]
    private float speed;

    bool active = false;

    public float LOSdistance;

	private bool isReversed = false;
	private float reverseTimer = 0.0f;
	private float reverseCooldown = 0.0f;

    public GameObject bullet;
    public GameObject muzzle;
    public float bulletSpeed;
    public float bulletDelay;
    public float health;

    public Vector2 lookDirection;

    private GameObject playerObj;
    private Rigidbody2D rigidbody;
    float frameDelay;


    // Use this for initialization
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        rigidbody = this.GetComponent<Rigidbody2D>();
        float frameDelay = 30 * bulletDelay;
    }

    // Update is called once per frame
    void Update()
    {

        //Get distance to player
        Vector2 playerDist = (playerObj.transform.position - transform.position);
        //Debug.Log(playerDist.magnitude);

		if (Input.GetKey(KeyCode.I) && reverseCooldown <= 0.0f) {
			isReversed = true;
			reverseTimer = 2.0f;
			reverseCooldown = 5.0f;
		}

		if (reverseTimer > 0.0f) {
			reverseTimer -= Time.deltaTime;
		} else {
			isReversed = false;
		}

		if (reverseCooldown > 0.0f) {
			reverseCooldown -= Time.deltaTime;
		}

        Vector2 velocity = Vector2.zero;
        if(!active)
        {
            if (checkLOS())
                active = true;
        }
        if (active)
        {
            transform.right = playerDist.normalized;
            lookDirection = playerObj.transform.position;
            //Debug.Log("Player DETECTED");
            //follow player
            velocity += playerDist.normalized * speed;
            if (checkLOS() && frameDelay == 0)
            {
                frameDelay += 30 * bulletDelay;
                shootBullet();

            }
            else
            {
                if (frameDelay > 0)
                    frameDelay--;
                
            }
        }
        rigidbody.velocity = velocity;
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("coll");
        if (other.gameObject.tag == "Bullet")
        {
            this.health -= 20;
            if(health<=0)
            Destroy(this.gameObject);
        }
    }

    bool checkLOS()
    {
        Vector3 start = muzzle.transform.position;
        Vector3 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - start).normalized;


        //draw the ray in the editor
        Debug.DrawRay(start, direction * LOSdistance, Color.red);

        //do the ray test
        RaycastHit2D sightTest = Physics2D.Raycast(start, direction, LOSdistance);
        if (sightTest.collider != null)
        {
            Debug.Log("Rigidbody collider is: " + sightTest.collider);
            if (sightTest.collider.gameObject.tag.Equals("Player"))
            {
                return true;
                Debug.Log("Rigidbody collider is: " + sightTest.collider);
            }
        }
        return false;
    }

    void shootBullet()
    {
        //if(speed == 0)
            //speed = 1;
                GameObject newBullet = Instantiate(bullet);
                newBullet.transform.rotation = muzzle.transform.rotation;
                newBullet.transform.position = muzzle.transform.position;
//        newBullet.GetComponent<Rigidbody2D>().velocity = (playerObj.transform.position - this.transform.position).normalized * (bulletSpeed + speed);
		if (isReversed) {
			newBullet.GetComponent<Rigidbody2D>().velocity = -(playerObj.transform.position - this.transform.position).normalized * (bulletSpeed + speed);
		} else {
			newBullet.GetComponent<Rigidbody2D>().velocity = (playerObj.transform.position - this.transform.position).normalized * (bulletSpeed + speed);
		}
        //speed = 0;
    }
}
