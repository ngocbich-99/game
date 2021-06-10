using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainplay : MonoBehaviour
{
    public const string LAYER_NAME = "upper";
    public GameObject light1;
    public GameObject wrong;
    public GameObject clue1;
    public GameObject darkpic;
    public GameObject box,restart,exit,ok,sta1,sta2,sta3,coin;
    public GameObject status,gold;
    public GameObject timedisplay;
    public Text tx_score;
    GameObject sn;
    int clue_price = 20,kt1=1;
    string cluename = "";
    int total_map_check;
    string scene_present ;
    int present_map ;
    int present_level;
    
    //audio 
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip backgroundSound;
    [SerializeField]
    [Range(0,1)]
    float soundVolume = 0.7f ;

    [SerializeField] 
    [Range(0.0f, 1.0f)] 
    private float volumeBg = 1f;
    
    //game status
    private GameStatus gameStatus = GameStatus.stopPlay;

    // Start is called before the first frame update
    void Start()
    {
        //playing game
        gameStatus = GameStatus.Playing;
        
        sn = GameObject.Find("scenename");
        scene_present = SceneManager.GetActiveScene().name;
        sn.GetComponent<UnityEngine.UI.Text>().text = scene_present;
        present_map = scene_present[0] - 48;
        present_level = scene_present[5] - 48;
        total_map_check = 0;
        clue1.SetActive(false);
        // light1.SetActive (false);
        wrong.SetActive(false);
        darkpic.SetActive(false);
        Global.check = 0;
        Global.time = 60;
        box.SetActive(false);
        status.SetActive(false);
        gold.SetActive(false);
        
        AudioSource.PlayClipAtPoint(backgroundSound,gameObject.transform.position, volumeBg);
    }
    
    // Update is called once per frame
    void Update()
    {
        timedisplay.GetComponent<UnityEngine.UI.Text>().text  = Global.time.ToString("F0") + " s";
            tx_score.text = Global.score.ToString();
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 mousePos = hit.point;
                    //Select stage    
                    clickevent(hit.collider.gameObject,mousePos);
                    return;
                }
            }

            if (gameStatus == GameStatus.Playing)
            {
                if (!GameObject.FindGameObjectWithTag("find"))
                {
                    //complete level
                    endLevel(1);
                }
                else
                {
                    if (Global.check == 1)
                    {
                        //incomplete
                        endLevel(0);
                    }
                }
            }
            
            // Debug.Log(darkpic.activeSelf + "update");
    }
    
   public void endLevel(int kt)
    {
        //Debug.Log(Global.check);
        darkpic.SetActive(true);
        box.SetActive(true);
        status.SetActive(true);
        tx_score.text = "";
        timedisplay.SetActive(false);
        //win game
        if (kt == 1 && kt1==1)
        {
            AudioSource.PlayClipAtPoint(winSound,gameObject.transform.position, soundVolume);
            //status win
            gameStatus = GameStatus.stopPlay;
            kt1++;
            gold.SetActive(true);
            status.GetComponent<UnityEngine.UI.Text>().text = "YOU WIN";
            sta1.SetActive(true);
            sta2.SetActive(true);
            sta3.SetActive(true);
            ok.SetActive(true);
            gold.GetComponent<UnityEngine.UI.Text>().text = "+50";
            coin.SetActive(true);
            Debug.Log("re_le = " + present_level);
            Debug.Log("pre_map =" + present_map);
            Global.score += 50;
            PlayerPrefs.SetInt("score", Global.score);
            PlayerPrefs.Save();
            if (present_map == Global.map)
            {
                if (present_level == Global.level)
                {
                    if (Global.level < 3)
                    {
                        Global.level = Global.level+ 1;
                        //save
                        PlayerPrefs.SetInt("level", Global.level);
                        PlayerPrefs.Save();
                        //REDIRECT TO PRESENT_MAP
                    }
                    else
                    {
                        if(Global.map==3)
                        {
                            //REDIRECT TO PRESENT_MAP
                        }
                        if (Global.map < 3)
                        {
                            // REMEMBER WHEN USER CLICK OK, APP HAS TO LOAD THE NEW MAP WITH LEVEL 1
                            //REDIRECT TO TOTAL MAP
                            total_map_check = 1;
                            Global.map = Global.map + 1;
                            Global.level = 1;
                            PlayerPrefs.SetInt("level", Global.level);
                            PlayerPrefs.SetInt("map", Global.map);
                            PlayerPrefs.Save();
                        }

                    }
                }
                else
                {
                    if (present_level < Global.level)
                    {
                        //REDIRECT TO PRESENT_MAP
                    }
                }
            }
            else
            {
                // add the score only
            }
            // Global.levelup = 1;
        }
        if (kt == 0)
        {
            //lose game
            AudioSource.PlayClipAtPoint(loseSound, transform.transform.position, soundVolume);
            //game status lose
            gameStatus = GameStatus.stopPlay;
            Debug.Log("lose");
            status.GetComponent<UnityEngine.UI.Text>().text = "GAME OVER";
            restart.SetActive(true);
            exit.SetActive(true);
            //REDIRECT TP PRESENT_MAP
        }
        
    } 
    public void clickevent(GameObject obj, Vector3 v)
    {
        
        // Destroy the gameObject after clicking on it
        if (obj.tag == "find")
        {
            string clickname = obj.name;
            if (cluename == clickname)
            {
                clue1.SetActive(false);
                cluename = "";
            }
            AudioSource.PlayClipAtPoint(correctSound, obj.transform.position ,soundVolume);
            obj.transform.position = new Vector3(0, 0);
            // light1.SetActive(true);
            GameObject lightPrefabs = Instantiate(light1, Vector3.zero, Quaternion.identity);
            Destroy(lightPrefabs, 1f);
            
            // StartCoroutine(RemoveAfterSeconds(1, light1));
            StartCoroutine(RemoveAfterSecondsfind(1, obj));
        }
        else
        {   if(obj.tag == "ui")
            {
                if (obj.name == "getclue" && clue1.activeSelf == false)
                {
                    foreach (GameObject fObj in GameObject.FindGameObjectsWithTag("find"))
                    {
                        if((Global.score - clue_price)>=0)
                        {
                       
                            Global.score = Global.score - clue_price;
                            PlayerPrefs.SetInt("score", Global.score);
                            PlayerPrefs.Save();
                            clue_price += 10;
                            //Debug.Log(fObj.name);
                            cluename = fObj.name;
                            clue1.transform.position = fObj.transform.position;
                            clue1.SetActive(true);
                            return;
                        }
                        
                    }

                }
                if (obj.name == "ok" )
                {
                    if (total_map_check == 1)
                    {
                        SceneManager.LoadScene(sceneName:"mainmap");
                    }
                    else
                    {
                        //load present map
                        string ss = "map" + present_map;
                        SceneManager.LoadScene(sceneName:ss);
                    }
                   

                }
                if (obj.name == "exit")
                {
                    string ss = "map" + present_map;
                    SceneManager.LoadScene(sceneName: ss);

                }
                if (obj.name == "restart")
                {
                    Animator ani=restart.GetComponent<Animator>();
                    ani.keepAnimatorControllerStateOnDisable=true;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);


                }
                if (obj.name == "home")
                {
                    SceneManager.LoadScene(sceneName: "mainmap");

                }

            }
            else
            {
                //click sai object can tim active wrong
                StartCoroutine(RemoveAfterSeconds(0.3f, wrong));
                AudioSource.PlayClipAtPoint(wrongSound, v, soundVolume);
                wrong.SetActive(true);
                wrong.transform.position = v; //v la gi
                Global.time = Global.time - 5;
                //Debug.Log(wrong.transform.position);

            }

        }

    }

    public static class Global
    {
        public static int score = 50;
        public static float time= 60;
        public static int check = 0;
        public static int level = 1;
        public static int levelup = 1;
        public static int map = 1;
        public static string playerName = "";
    }
    IEnumerator RemoveAfterSeconds(float seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
    IEnumerator RemoveAfterSecondsfind(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        string objname = obj.name;
        Destroy(obj);
       
        foreach (GameObject tick in GameObject.FindGameObjectsWithTag("check"))
        {
           
            if(tick.name== objname)
            {
                tick.GetComponent<SpriteRenderer>().sortingLayerName = LAYER_NAME;
                GameObject g = GameObject.Find("inner" + tick.name);
                g.SetActive(false);


            }
        }
            
      
    }

}
