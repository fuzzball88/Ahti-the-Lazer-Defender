using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour {
    public Text text; 
	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Breakable brics: " + brick.breackableCount;
	}
}
