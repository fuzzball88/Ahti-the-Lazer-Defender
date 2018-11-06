using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float playerSpeed = 5f;
    public float health = 5f;
    public float yMin = -4.2f;
    public float yMax = -2f;
    public float padding = 0.5f;

    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float fireRate = 0.4f;

    private float xMin;
    private float xMax;

    private ScoreKeeper scoreKeeper;
    //private LevelManager levelmanager;

    public AudioClip shoot;
    public AudioClip die;
    public AudioClip hit;
    public GameObject bigExplosion;
    public GameObject smallExplosion;

    // Use this for initialization
    void Start () {
        //Distance between object(player) and main camera
        float distance = transform.position.z - Camera.main.transform.position.z;
        //Lets you act on main camera
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftMost.x + padding;
        xMax = rightMost.x - padding;

        scoreKeeper = Text.FindObjectOfType<ScoreKeeper>();
        //levelmanager = LevelManager.FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckInput();
        RestrictPosition();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            Instantiate(smallExplosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(hit, transform.position);
            missile.Hit();
            health -= missile.GetDamage();
            if(health <= 0)
            {
                Die();
            }
            //Debug.Log("Hit by projectile");
            
        }
    }
    void Die()
    {
        Instantiate(bigExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(die, transform.position);
        LevelManager levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelmanager.LoadLevel("Lose");
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("FireProjectile", 0.000001f, fireRate);
           
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("FireProjectile");
        }

        if (Input.GetKey(KeyCode.UpArrow)){
            //Predefined Vector3
            transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);
            //Other way. Constructin new Vector3
            //transform.position += new Vector3(-playerSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(Vector3.down * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        
            

    }

    void RestrictPosition()
    {
        //Creates new clamped float
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
        //Updates players position with new one
        transform.position = new Vector3(newX,newY,transform.position.z);
    }

    void FireProjectile()
    {
        GameObject beam = Instantiate(projectile, (transform.position+new Vector3(0,0.5f,0)), Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(shoot, transform.position);
    }
}
