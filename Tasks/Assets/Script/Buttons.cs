using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour

{
    public GameObject pusePannel;
    public GameObject settingsPannel;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Puse()
    {
        if (pusePannel.activeSelf == true)
        {
            pusePannel.SetActive(false);
           
        }
        else
        {
            pusePannel.SetActive(true);
        }
    }
    public  void Settings()
    {
        if(settingsPannel.activeSelf == true)
        {
            settingsPannel.SetActive(false);
        }
        else
        {
            settingsPannel.SetActive(true);
        }
    }
    public void Levels()
    {

    }
    public void MainMenu()
    {

    }
   
}
