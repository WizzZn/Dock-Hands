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
    public GameObject isContin;
    public ParticleSystem smoke;
    public float speed;
    public GameObject greBoat,orgBoat;

    //private GameObject[] contSlote = new GameObject[3];
    private GameObject[] slote = new GameObject[5];
    private GameObject[] contGre = new GameObject[5],contOrg = new GameObject[5];
    private int countGre,countOrg,sigma;
    private bool spic;
    private void Start()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }

        countTrack = GameObject.FindGameObjectsWithTag("Path").Length;
        numberOfMoves = countTrack + 3;
        
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
                sigma = i;
                spic = true;
                for (int j = 1; j < 4; j++)
                {
                    string org = "Cont_Org_" + j.ToString();
                    string gre = "Cont_Gre_" + j.ToString();

                    if (slote[i].name == org)
                    {
                        contOrg[i] = slote[i];
                        countOrg++;
                      
                    }
                    else if(slote[i].name == gre)
                    {
                        contGre[i] = slote[i];
                        countGre++;

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
            if (isContin.transform.position == slotePoss[sigma].transform.position)
            {
                spic = false;
            }
        }
       
    }

    private void Update()
    {
        MoveToSlote();
    }
  
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Green"))
        {
            Debug.Log("hitGre");
            BoatMove.boatMoveInstance.starter = false;

            if (countGre == 3)
            {
               for(int i = 0; i < contGre.Length; i++)
               {
                    if (contGre[i] != null)
                    {
                        contGre[i].SetActive(false);
                        slote[i]  = null;
                        contGre[i] = null;
                    }
               }
              
                greBoat.transform.GetChild(0).gameObject.SetActive(true); //for containers;
                BoatMove.boatMoveInstance.starter = true;
            }
        }
        if (other.gameObject.CompareTag("Orange"))
        {
            Debug.Log("hitOrg");
            BoatMove.boatMoveInstance.starter = false;

            if (countOrg == 3)
            {
                for (int i = 0; i < contOrg.Length; i++)
                {
                    if (contOrg[i] != null)
                    {
                        contOrg[i].SetActive(false);
                        slote[i] = null;
                        contOrg[i] = null;
                    }
                }
                orgBoat.transform.GetChild(0).gameObject.SetActive(true);  //for containers;
                BoatMove.boatMoveInstance.starter = true;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Green"))
        {
            if(!GetComponent<BoatMove>())
            {
                orgBoat.GetComponent<BoatMove>().enabled = true;
                Debug.Log("hitExit");

            }

        }
    }
}
