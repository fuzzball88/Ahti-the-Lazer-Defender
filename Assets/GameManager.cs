using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    static GameManager instance = null;
    public ScoreKeeper scoreKeeper;

    public int score;
    

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            print("duplicate game manager destroyed");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel == 0)
        {
            score = 0;
        }
	}
}
