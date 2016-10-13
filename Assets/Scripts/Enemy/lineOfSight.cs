using UnityEngine;
using System.Collections;

public class lineOfSight : MonoBehaviour {

    public float speed;
    public float bulletSpeed;
    public float bulletDelay;

    public float LOSdistance;

    public GameObject bullet;
    public GameObject muzzle;


    public Vector2 lookDirection;
    // Use this for initialization




    void Start()
    {
        StartCoroutine(shootBullets());
    }

    // Update is called once per frame
    void Update()
    {



    }

    IEnumerator shootBullets()
    {
        while (1 == 1)
        {
            if (checkLOS() == true)
            {
                GameObject newBullet = Instantiate(bullet);
                newBullet.transform.rotation = transform.rotation;
                newBullet.transform.position = muzzle.transform.position;
                newBullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * (lookDirection * speed);
                yield return new WaitForSeconds(bulletDelay);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("coll");
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }

    bool checkLOS()
    {
        bool hitPlayer = false;
        Vector3 start = this.gameObject.transform.position;
        Vector3 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        

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
}
