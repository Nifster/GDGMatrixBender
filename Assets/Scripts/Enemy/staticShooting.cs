using UnityEngine;
using System.Collections;

public class staticShooting : MonoBehaviour {
    
    public float speed;
    public float bulletSpeed;
    public float bulletDelay;

    public GameObject bullet;
    public GameObject muzzle;


    public Vector2 lookDirection;
    // Use this for initialization
    void Start () {
        StartCoroutine(shootBullets());
    }
	
	// Update is called once per frame
	void Update () {

        

    }

    IEnumerator shootBullets()
    {
        while (1 == 1)
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.rotation = transform.rotation;
            newBullet.transform.position = muzzle.transform.position;
            newBullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * (lookDirection * speed);
            yield return new WaitForSeconds(bulletDelay);
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
}
