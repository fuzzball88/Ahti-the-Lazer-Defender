using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose_collider : MonoBehaviour {

    private LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger");
        levelManager.LoadLevel("Lose");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collision");
    }
}
