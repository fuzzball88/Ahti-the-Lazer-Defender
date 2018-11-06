using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour {

    public AudioClip crack;
    public GameObject smoke;
    public float crackVolume = 0.6f;
    public static int breackableCount = 0;

    public int hitsTaken;
    public Sprite[] hitSprites;

    private LevelManager levelmanager;
    private bool isBreakable;
    private Ball ball;




    // Use this for initialization
    private void Awake()
    {
        breackableCount = 0;
    }

    void Start () {
        isBreakable = (this.tag == "Breakable");
        levelmanager = GameObject.FindObjectOfType<LevelManager>();
        //Keep track of breakable bricks
        if (isBreakable)
        {
            breackableCount++;
        }
        hitsTaken = 0;
        ball = GameObject.FindObjectOfType<Ball>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        AudioSource.PlayClipAtPoint(crack, transform.position);
        if (isBreakable)
        {
            HandleHits();
        }
        

    }

    void HandleHits()
    {
        hitsTaken++;
        int maxHits = hitSprites.Length + 1;
        if (hitsTaken >= maxHits)
        {
            breackableCount--;
            DestroyObject(gameObject);
            puffSmoke();
            ball.AddSpeed();
            
            
        }
        else
        {        
            LoadSprites();
        }
    }

    void puffSmoke()
    {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
        smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
   
    void SimulateWin()
    {
        levelmanager.LoadNextLevel();
    }

    void LoadSprites()
    {
        //Determine which sprite to show on hit.List begins from 0
        int spriteInxed = hitsTaken - 1;
        //Debug.Log(spriteInxed);

        //Access this current brick and change the sprite__If hitsprites has something then load the new sprite
        if (hitSprites[spriteInxed] != null) { 
            this.GetComponent<SpriteRenderer>().sprite=hitSprites[spriteInxed];
        }
        else
        {
            Debug.LogError("Brick sprite missing");
        }
    }
}
