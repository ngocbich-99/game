using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (mainplay.Global.time <= 0)
        {
            mainplay.Global.check = 1;
        }
        else
        {
            mainplay.Global.time -= Time.deltaTime;
        }
    }
}
