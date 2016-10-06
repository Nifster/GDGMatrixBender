using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public float speed;
    public float bulletSpeed;
    public float bulletDelay;

    public GameObject bullet;
    public GameObject muzzle;

    public GameObject net;

    public int bulletCount;

    public Vector2 lookDirection;

    public Slider healthBarSlider;
    public Slider energyBarSlider;
    private bool tired = false; 

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = (mousePos - (Vector2)transform.position).normalized;
        transform.right = lookDirection;
        //energyBarSlider.value += (float)0.1;

        if (energyBarSlider.value >= 10)
        {
            tired = false;
        }
        if(energyBarSlider.value<=0)
        {
            tired = true;
        }
        if(healthBarSlider.value<=0)
        {
            Destroy(this.gameObject);
        }

    }

    void FixedUpdate(){
        Vector3 velocity = Vector3.zero;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (Input.GetKey(KeyCode.W))
        {
            velocity += Vector3.up * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += Vector3.down * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector3.right * speed;
        }
      
        rb.velocity = velocity;
        

        if (Input.GetMouseButton(0))
        {
            if (!tired)
                {   
                    energyBarSlider.value -= (float)0.3;
                    //Activate reflection
                    if(net.activeSelf==false)
                    // Drop all bullets stored
                    bulletCount = 0;
                    net.SetActive(true);
                }
            else
            {
                net.SetActive(false);
                bulletCount = 0;
                

            }
                
        }

        if (!Input.GetMouseButton(0))
        {   //Deactivate reflection
            energyBarSlider.value += (float)0.2;
            
            net.SetActive(false);
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (energyBarSlider.value<=10)
            {
                tired = true;
            }
                net.SetActive(false);
            //Shoot bullets one at a time after delay, think of it as him reflecting one bullet at a time
            StartCoroutine(reflectBullets());
        }

    }
    IEnumerator reflectBullets()
    {
        while(bulletCount>0) {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.rotation = transform.rotation;
            newBullet.transform.position = muzzle.transform.position;
            newBullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * (lookDirection * speed);
            bulletCount--;
            yield return new WaitForSeconds(bulletDelay);
        }
    }

  
}

