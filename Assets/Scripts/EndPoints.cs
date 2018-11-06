using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoints : MonoBehaviour {
    public Text endPointsText;
    public GameManager gameManager;

	// Use this for initialization
	void Start () {
        endPointsText = gameObject.GetComponent<Text>();
        endPointsText.text = ScoreKeeper.score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
