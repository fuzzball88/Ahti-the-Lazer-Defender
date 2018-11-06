using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    //public float speed = 5f;
    public float damage = 1f;
    
    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        //Other way to move the projectile
        //transform.Translate(Vector3.up * speed * Time.deltaTime);
	}

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy") { 
        Debug.Log("Enemy hit");
        }
    }
    */
}
