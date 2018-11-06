using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour {

    private Paddle paddle;

    public Rigidbody2D rigidbody2D;
    private bool hasGameStarted = false;

    //Relation of the paddle and ball
    private Vector3 paddleToBallVector;
    AudioSource audio;
    private GameObject instructionPanel;

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        //Finds the Paddle instant from GameObjects and links it to paddle.
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
        //print(paddleToBallVector);
        rigidbody2D = GetComponent<Rigidbody2D>();
        instructionPanel = GameObject.FindGameObjectWithTag("Instruction");
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasGameStarted)
        {
            //Lock ball relative to paddle
            this.transform.position = paddle.transform.position + paddleToBallVector;
        
            //Wait for mouse press to launch
            if (Input.GetMouseButtonDown(0))
            {
                instructionPanel.SetActive(false);
                hasGameStarted = true;
                print("fire clicked");
                rigidbody2D.velocity = new Vector2(2f, 10f);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Random force
        Vector2 tweak = new Vector2(Random.Range(0.2f, 0.6f), Random.Range(0.2f, 0.6f));
        
        if (hasGameStarted) { 
        audio.Play();
            //Add random force to the ball
            rigidbody2D.velocity += tweak;
        }
    }

    public void AddSpeed()
    {
        rigidbody2D.velocity += new Vector2(0.5f,0.5f);
    }
}
