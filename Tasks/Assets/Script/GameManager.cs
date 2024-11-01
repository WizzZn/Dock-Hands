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
    public GameObject greBoat,orgBoat;

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
        MoveToShip();
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
                sigma = i;
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
                          /*  for (int l = 0; l < slote.Length ; l++)
                            {
                                for (int k = 0; k < contOrg.Length ; k++)
                                {
                                    if (contOrg[k].name == slote[l].name)
                                    {
                                        slote[l] = null;
                                        spic = false;
                                    }
                                    break;
                                }

                            }*/
                            


                          
                        }
                    }
                    else if(slote[i].name == gre)
                    {
                        contGre[countGre] = slote[i];
                        countGre++;

                        if (countGre == 3)
                        {
                            

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

        BoatMove.boatMoveInstance.starter = true;

    }
    private void Update()
    {
        MoveToSlote();
    }
   
  
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Green"))
        {
            Debug.Log("hit");
            BoatMove.boatMoveInstance.starter = false;

            if (countGre == 3)
            {
                contGre[0].SetActive(false);
                contGre[1].SetActive(false);
                contGre[2].SetActive(false);
                greBoat.transform.GetChild(0).gameObject.SetActive(true);
                BoatMove.boatMoveInstance.starter = true;
            }
        }
        if (other.gameObject.CompareTag("Orange"))
        {
            Debug.Log("hit");
            BoatMove.boatMoveInstance.starter = false;

            if (countOrg == 3)
            {
                contOrg[0].SetActive(false);
                contOrg[1].SetActive(false);
                contOrg[2].SetActive(false);
                orgBoat.transform.GetChild(0).gameObject.SetActive(true);
                BoatMove.boatMoveInstance.starter = true;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Green"))
        {
            orgBoat.AddComponent<BoatMove>();

        }
    }
}
