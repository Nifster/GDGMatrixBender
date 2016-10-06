using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float activeRange;

    [SerializeField]
    private float speed;

    private GameObject playerObj;
    private Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Get distance to player
        Vector2 playerDist = (playerObj.transform.position - transform.position);
        //Debug.Log(playerDist.magnitude);

        Vector2 velocity = Vector2.zero;
        if (playerDist.magnitude < activeRange)
        {
            //Debug.Log("Player DETECTED");
            //follow player
            velocity += playerDist.normalized * speed;
        }
        rigidbody.velocity = velocity;
        transform.right = playerDist.normalized;
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
