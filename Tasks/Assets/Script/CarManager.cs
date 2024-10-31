using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CarManager : PathFollower
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    public GameObject isCont;
   
   protected override void Start()
   {
        base.Start();
        AddComponent();
       
    }
   
    
    private void AddComponent()
    {
        if (!GetComponent<Rigidbody>())
        {
            gameObject.AddComponent<Rigidbody>().isKinematic = true;

        }
        if (!GetComponent<BoxCollider>())
        {
            gameObject.AddComponent<BoxCollider>().size = new Vector3(2f, 2f, 4f);
            GetComponent<BoxCollider>().center = new Vector3(0, 1f, 0);
            GetComponent<BoxCollider>().isTrigger = true;
        }
        
        gameObject.tag = "Car";
        endOfPathInstruction = EndOfPathInstruction.Stop;

        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {


            reverse = true;
            moveCar = true;

            GameManager.gameManagerInstance.tochLock = true;


            GameManager.gameManagerInstance.losebool = true;
            GameManager.gameManagerInstance.Lose();
            Debug.Log("Car crashed!!");

            _rigidbody.isKinematic = _collider.isTrigger = false;
            other.GetComponent<Rigidbody>().AddForceAtPosition(other.transform.position * 50, other.transform.position);

            Invoke("TimeDelay", 2f);

        }


        if (other.CompareTag("Continer"))
        {
            var paths = GameObject.FindGameObjectsWithTag("Path");
            var roads = GameObject.FindGameObjectsWithTag("Road");
            var conts = GameObject.FindGameObjectsWithTag("Continer");
            
            string contName = "Cont_" + gameObject.name;
            string pathName = "Path_" + gameObject.name;
            string roadName = "Road_" + gameObject.name;
           // GameManager.gameManagerInstance.sigma++;
            for (int i = 0; paths.Length > i; i++)
            {
                if (paths[i].name == pathName)
                {
                    paths[i].SetActive(false);
                    gameObject.SetActive(false);
                    roads[i].SetActive(false);
                   // Debug.Log("Sucess");
                    break;
                }
            }
            for (int i = 0; conts.Length > i; i++)
            {
                if(conts[i].name == contName)
                {
                    GameManager.gameManagerInstance.isContin = conts[i];
                    GameManager.gameManagerInstance.Sloting();

                    break;
                }

            }
        }
       
    }
    private void TimeDelay()
    {
        _rigidbody.isKinematic = _collider.isTrigger = true;

    }

}
