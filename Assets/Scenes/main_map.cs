using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_map : MonoBehaviour
{
    public Text map;
    public Text lv;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        map.text = mainplay.Global.map.ToString();
        lv.text = mainplay.Global.level.ToString();
        score.text = mainplay.Global.score.ToString();
        Debug.Log(PlayerPrefs.GetString("name", null));
        Debug.Log(PlayerPrefs.GetInt("map"));
        Debug.Log(PlayerPrefs.GetInt("level"));
        Debug.Log(PlayerPrefs.GetInt("score"));
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

    }
    public void clickevent(GameObject obj, Vector3 v)
    {
        if (obj.tag == "detailmap")
        {
            if (mainplay.Global.map >= int.Parse(obj.name))
            {

                string namemap ="map" + obj.name;
                SceneManager.LoadScene(sceneName: namemap);
            }



        }
        else
        {


            Debug.Log("no");



        }
    }
}
