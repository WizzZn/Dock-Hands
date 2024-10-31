using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public int countTrack, numberOfMoves;
    public bool victorybool, losebool, tochLock;
    public GameObject[] slotePoss;
    //public GameObject[] contSlotePos;
    public GameObject isContin;
   // public GameObject[] contSlote = new GameObject[2];


    private GameObject[] slote = new GameObject[5];
    private GameObject[] contGre,contOrg = new GameObject[2];
    private void Start()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        
        countTrack = GameObject.FindGameObjectsWithTag("Path").Length;
        numberOfMoves = countTrack * 5;
    }
    public void Victory()
    {
        if (countTrack == 0 && !losebool)
        {
            victorybool = true;
            Debug.Log("U Won!");
        }
    }
    public void Lose()
    {
        if (losebool || numberOfMoves == 0 && !victorybool)
        {
            Debug.Log("U Lose!!");
        }
    }
    public void Sloting()
    {
        for (int i = 0; i < slote.Length; i++)
        {
            if (slote[i] == null)
            {
                slote[i] = isContin;
                tochLock = true;
                break;
                
            }
        }
    }
}
