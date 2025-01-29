using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    [Header("Game God Settings")]
    public int countTrack, numberOfMoves;
    public bool victorybool, losebool, tochLock,grebool,orgbool;//for lock the values 
    public float speed;
    public GameObject[] slotePoss, boatList;
    public GameObject isContin;
    public ParticleSystem [] smoke;
    public TextMeshProUGUI chanceText;
    public TextMeshProUGUI TrackText;
    public GameObject winPannel,losePannel;
    

    private GameObject[] slote = new GameObject[5];
    private GameObject[] contGre = new GameObject[5],contOrg = new GameObject[5];
    private int countGre,countOrg,sloteIntex, trackComplete,boatIntex;
    private bool spic,boatbool;
          

    private void Awake()
    {
        winPannel.SetActive(false);
        losePannel.SetActive(false);
    }
    private void Start()
    {
        foreach (var particle in smoke)
        {
            if (particle != null)
            {
                particle.Stop();

            }
            else
            {
                Debug.Log("Particle is null");
            }
        }

        boatIntex = 0;
        boatList[boatIntex].GetComponent<BoatMove>().starter = true;
        //#region GameManagerInstance
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }

        countTrack = GameObject.FindGameObjectsWithTag("Path").Length;
        numberOfMoves = countTrack + 3;
        
        
    }
    private void Update()
    {
        if (GameManager.gameManagerInstance == null)
        {
            Debug.LogError("GameManager instance is null!");
        }
       
        chanceText.text = numberOfMoves.ToString();
        TrackText.text = countTrack + "/" + trackComplete;
        MoveToSlote();
        Victory();
        Lose();
        BoatMovement();
        
    }
    public int TrackComplete
    {
        get
        {
            return trackComplete;
        }
        set
        {
            //Debug.Log($"TrackComplete changed from {trackComplete} to {value}");
            trackComplete = value;
        }
    }
    public void Victory()
    {
        if (countTrack == trackComplete && !losebool)
        {
            victorybool = true;
            winPannel.SetActive(true);
            Debug.Log("U Win");

        }
    }
    public void Lose()
    {
        if (losebool && !victorybool)
        {
            losePannel.SetActive(true);
            Debug.Log("U Losss");
        }
    }
    public void Sloting()
    {
        for (int i = 0; i < slote.Length; i++)
        {
            if (slote[i] == null)
            {   
                slote[i] = isContin;
                TrackComplete++;
                MoveToSlote();
                tochLock = true;
                sloteIntex = i;
                spic = true;
                for (int j = 1; j < 4; j++)
                {
                    string org = "Cont_Org_" + j.ToString();
                    string gre = "Cont_Gre_" + j.ToString();

                    if (slote[i].name == org && orgbool)
                    {
                        contOrg[i] = slote[i];
                        countOrg++;

                    }
                    else if(slote[i].name == gre && grebool)
                    {
                        contGre[i] = slote[i];
                        countGre++;
                    }
                }
                break;
            }
           

        }

    }
    public void TochControll()// for checking Num of Move;
    {
        Debug.Log($"TouchControll Checking  : {numberOfMoves} - Moves Left ");

        if (numberOfMoves <= 0 && !victorybool)
        {
            tochLock = false;
            Debug.Log($"Touch Locked for number of Move is Null");
            losebool = true;
            Lose();
        }
    }
    private void MoveToSlote()
    {
        if (spic)
        {
            isContin.transform.position = Vector3.MoveTowards(isContin. transform.position, slotePoss[sloteIntex].transform.position, speed );
            isContin.transform.localEulerAngles = new Vector3(0, 180, 0);
            if (isContin.transform.position == slotePoss[sloteIntex].transform.position)
            {
                spic = false;
                smoke[sloteIntex].Play();

                int h = 0;
                for (int k = 0; k < slote.Length; k++)
                {
                    if (slote[k] != null)
                    {
                        h++;
                        if (h == slote.Length)
                        {
                            losebool = true;
                            Lose();
                        }
                    }
                }
                h = 0;
                TochControll();
               
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Green") && boatIntex< boatList.Length && other.gameObject.name == boatList[boatIntex]?.name)
        {

            boatList[boatIntex].GetComponent<BoatMove>().starter = false;


            if (countGre == 3 && !spic)//!spic Use for Delaying Work
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
                boatList[boatIntex]?.transform.GetChild(0).gameObject.SetActive(true); //for containers;
                boatList[boatIntex].GetComponent<BoatMove>().starter = true;
                boatList[boatIntex].GetComponent<BoatMove>().speed = 4f;
                boatIntex++;
                boatbool = true;
            }
        }
        if (other.gameObject.CompareTag("Orange") && boatIntex < boatList.Length && other.gameObject.name == boatList[boatIntex]?.name)
        {

            boatList[boatIntex].GetComponent<BoatMove>().starter = false;

            if (countOrg == 3 && !spic)//!spic Use for Delaying Worke
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
                boatList[boatIntex]?.transform.GetChild(0).gameObject.SetActive(true);  //for containers;
                boatList[boatIntex].GetComponent<BoatMove>().starter = true;
                boatList[boatIntex].GetComponent<BoatMove>().speed = 4f;
                boatIntex++;
                boatbool = true;
            }
        }
        if (other.gameObject.CompareTag("Red") && boatIntex < boatList.Length && other.gameObject.name == boatList[boatIntex]?.name)
        {

            boatList[boatIntex].GetComponent<BoatMove>().starter = false;

            if (countOrg == 3 && !spic)//!spic Use for Delaying Worke
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
                boatList[boatIntex]?.transform.GetChild(0).gameObject.SetActive(true);  //for containers;
                boatList[boatIntex].GetComponent<BoatMove>().starter = true;
                boatList[boatIntex].GetComponent<BoatMove>().speed = 4f;
                boatIntex++;
                boatbool = true;
            }
        }

        if (boatIntex >= boatList.Length)
        {
            Debug.Log("All boats have been used.");
        }
    }
   
    private void BoatMovement()
    {
        if (boatbool)
        {
            if (boatIntex >= 0 && boatIntex < boatList.Length)
            {
                boatList[boatIntex].GetComponent<BoatMove>().starter = true;
                boatList[boatIntex].GetComponent<BoatMove>().speed = 5f;
                boatbool = false;
            }
           
        }
    }
    
}
