using UnityEngine;
using System.Collections;

public class meleeAttack : MonoBehaviour {

	public GameObject meleeAttackPrefab;

	private double coolDown = -0.1f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Mouse1)) {
			if (coolDown < 0.0) {
				coolDown = 2.0f;
				//Instantiate melee attack prefab
				Debug.Log("Haha");
			}
		}

		if (coolDown > 0.0f) {
			Debug.Log("Hehe");
			coolDown -= Time.deltaTime;
		}
	}

}
