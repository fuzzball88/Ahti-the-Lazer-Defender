using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {
    static LevelManager instance = null;
    /*
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            print("duplicate music player destroyed");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
    */
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
    
    public void QuitRequest()
    {
        Debug.Log("Level Quit request");
        Application.Quit();
    }
    public void LoadNextLevel()
    {
        brick.breackableCount = 0;
        Application.LoadLevel(Application.loadedLevel+1);
    }



}
