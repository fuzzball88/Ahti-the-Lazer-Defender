using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
    private Text scoreText;
    public static int score = 0;

    //static ScoreKeeper instance = null;
    static GameManager gameManager;
    /*
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
    */

    // Use this for initialization
    void Start () {
       ScoreKeeper.Reset();
       scoreText = GetComponent<Text>();
       scoreText.text = "Score: " + score;
       //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score;
    }

    public static void AddPoints(int points)
    {
        score += points;
        
    }

    public static void Reset()
    {
        score = 0; 
    }
}
