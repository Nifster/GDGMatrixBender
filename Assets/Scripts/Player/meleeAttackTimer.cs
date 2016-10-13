using UnityEngine;
using System.Collections;

public class meleeAttackTimer : MonoBehaviour {

	private float timer = 0.3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < 0.3f)
			Destroy(this.gameObject);
		else
			timer -= Time.deltaTime;
	}
}
