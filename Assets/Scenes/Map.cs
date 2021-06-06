using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
     GameObject mapn;
    public Text gold1;
    Animator anim;
    int count = 0;
    string mapname;
    void Start()
    {
        mapname = SceneManager.GetActiveScene().name;
        Debug.Log("level" + mainplay.Global.level);
        Debug.Log("map" + mainplay.Global.map);
        mapn = GameObject.Find("scenename");
        mapn.GetComponent<UnityEngine.UI.Text>().text= SceneManager.GetActiveScene().name;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                Vector3 mousePos = hit.point;
                //Select stage    
                clickevent(hit.collider.gameObject, mousePos);
                return;

            }
        }
        gold1.text = mainplay.Global.score.ToString();
        
        if (count == 0)
            {
           
            int present_map = mapname[3] - 48;
            if (present_map == mainplay.Global.map)
            {
                //anim to global level only
                foreach (GameObject fObj in GameObject.FindGameObjectsWithTag("land"))
                {
                    if ((int.Parse(fObj.name) <= mainplay.Global.level))
                    {
                        anim = fObj.GetComponent<Animator>();
                        anim.SetBool("isShow2", true);
                    }
                }

                count++;

            }
            else
            {
                //anim all
                foreach (GameObject fObj in GameObject.FindGameObjectsWithTag("land"))
                {
                   
                        anim = fObj.GetComponent<Animator>();
                        anim.SetBool("isShow2", true);
                   
                }
            }
                
                    
            }
            
        
    }
    public void clickevent(GameObject obj, Vector3 v)
    {

        // Destroy the gameObject after clicking on it
        if (obj.name == "home")
        {
            SceneManager.LoadScene(sceneName: "mainmap");
        }
        int present_map = mapname[3] - 48;
        if (present_map == mainplay.Global.map)
        {
            //clickable up to global map
            if (obj.tag == "land" || obj.tag == "head")
            {
                if (mainplay.Global.level >= int.Parse(obj.name))
                {
                    string mapname = SceneManager.GetActiveScene().name;

                    string namescene = mapname[3] + "play" + obj.name;
                    SceneManager.LoadScene(sceneName: namescene);
                }



            }
            else
            {
                Debug.Log("no");
            }
        }
        else
        {
            //clickable all
            if (obj.tag == "land" || obj.tag == "head")
            {
                
                    string mapname = SceneManager.GetActiveScene().name;

                    string namescene = mapname[3] + "play" + obj.name;
                    SceneManager.LoadScene(sceneName: namescene);
                



            }
            else
            {
                Debug.Log("no");
            }

        }

            

    }


}
