using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public int countTrack, numberOfMoves;
    public bool victorybool, losebool, tochLock;
    public GameObject[] slotePoss;
    public GameObject[] contSlotePos;
    public GameObject isContin;
    public ParticleSystem smoke;
    public float speed;

    //private GameObject[] contSlote = new GameObject[3];
    private GameObject[] slote = new GameObject[5];
    private GameObject[] contGre = new GameObject[3],contOrg = new GameObject[3];
    private int countGre,countOrg,sigma;
    private bool spic;
    private void Start()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        
        countTrack = GameObject.FindGameObjectsWithTag("Path").Length;
        numberOfMoves = countTrack + 5;
        sigma = -1;
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
                MoveToSlote();
                tochLock = true;
                sigma++;
                spic = true;
                for (int j = 1; j < 4; j++)
                {
                    string org = "Cont_Org_" + j.ToString();
                    string gre = "Cont_Gre_" + j.ToString();

                    if (slote[i].name == org)
                    {
                        contOrg[countOrg] = slote[i];
                        countOrg++;
                        if (countOrg == 3)
                        {
                           // MoveToShip();
                        }
                    }
                    else if(slote[i].name == gre)
                    {
                        contGre[countGre] = slote[i];
                        countGre++;

                        if (countGre == 3)
                        {
                            MoveToShip();

                        }
                    }

                }
                break;

            }

        }
       
    }
    private void MoveToSlote()
    {
        if (spic)
        {
            isContin.transform.position = Vector3.MoveTowards(isContin. transform.position, slotePoss[sigma].transform.position, speed );
            isContin.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
       
    }
    private void MoveToShip()
    {
        contGre[0].transform.position = Vector3.MoveTowards(transform.position, contSlotePos[0].transform.position,speed );
        contGre[1].transform.position = Vector3.MoveTowards(transform.position, contSlotePos[1].transform.position,  speed );
        contGre[2].transform.position = Vector3.MoveTowards(transform.position, contSlotePos[2].transform.position, speed );


    }
    private void Update()
    {
        MoveToSlote();
    }
}
