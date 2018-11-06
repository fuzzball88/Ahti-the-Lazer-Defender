using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {

    public float health = 3f;
    public float projectileSpeed = 5f;
    public float shotsPerSecond = 0.5f;

    public int scoreValue = 150;

    public GameObject enemyLaser;
    private ScoreKeeper scoreKeeper;

    public AudioClip shoot;
    public AudioClip die;
    public AudioClip hit;
    public GameObject bigExplosion;
    public GameObject smallExplosion;



    // Use this for initialization
    void Start()
    {
        //highScore = Text.FindObjectOfType<Score>();
        //Lower option finds Score object dynamically
        //scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
	// Update is called once per frame
	void Update () {
        FireProjectile();
	}

    void FireProjectile()
    {
        //counts the firerate
        float probability = Time.deltaTime * shotsPerSecond;

        if (Random.value < probability) { 
        GameObject laser = Instantiate(enemyLaser, (transform.position + new Vector3(0, -0.5f, 0)), Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(shoot, transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            AudioSource.PlayClipAtPoint(hit, transform.position);
            Instantiate(smallExplosion, transform.position, Quaternion.identity);
            missile.Hit();
            health -= missile.GetDamage();
            if(health <= 0)
            {
                Destroy(gameObject);
                //highScore.AddPoints();
                
                ScoreKeeper.AddPoints(scoreValue);
                AudioSource.PlayClipAtPoint(die, transform.position);
                Instantiate(bigExplosion, transform.position, Quaternion.identity);
            }
            //Debug.Log("Hit by projectile");
            
        }
    }

    public void AddDifficulty()
    {
        if(this.projectileSpeed > 10f)
        {
            scoreValue *= 2;
        }
        else { 
        projectileSpeed *= 1.5f;
        }
    }
}
