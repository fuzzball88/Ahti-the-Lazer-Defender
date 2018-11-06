using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float speed = 0.2f;
    public bool autoPlay = false;

    private Ball ball;

	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
        if (autoPlay == false) { 
            MoveWithMouse();
            }
        else
        {
            AutoPlay();
        }
    }

    void MoveWithMouse()
    {
        Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);
        float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        paddlePos.x = Mathf.Clamp(mousePosInBlocks, 2.3f, 13.7f);
        this.transform.position = paddlePos;
    }

    void AutoPlay()
    {
        Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);
        Vector3 ballPos = ball.transform.position;
        paddlePos.x = Mathf.Clamp(ballPos.x, 1.8f, 14.2f);
        this.transform.position = paddlePos;

    }




}
