using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        int score = PlayerPrefs.GetInt("score");
        int level = PlayerPrefs.GetInt("level");
        int map = PlayerPrefs.GetInt("map");
        string str = PlayerPrefs.GetString("name", null);
        if (string.IsNullOrEmpty(str) == true) 
        {
            //load get name scene
            PlayerPrefs.SetInt("score",50);
            PlayerPrefs.SetInt("level",1);
            PlayerPrefs.SetInt("map", 1);
  
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneName: "newuser");
        }
        else
        {
            mainplay.Global.playerName = str;
            mainplay.Global.level = level;
            mainplay.Global.map = map;
            mainplay.Global.score = score;
            //load mainmap scene
            SceneManager.LoadScene(sceneName: "mainmap");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
