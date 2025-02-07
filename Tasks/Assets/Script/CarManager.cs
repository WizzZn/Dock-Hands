#if UNITY_EDITOR

using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CarManager : PathFollower
{
    public static CarManager carManagerInstence;
    private Rigidbody _rigidbody;
    private Collider _collider;
    public GameObject isCont;
  
   
   protected override void Start()
   {
        base.Start();
        AddComponent();
       if (carManagerInstence == null)
       {
            carManagerInstence = this; 
       }
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
            Debug.Log($"Car crashed with Another Car : {other.gameObject}");
            _rigidbody.isKinematic = _collider.isTrigger = false;
            other.GetComponent<Rigidbody>().AddForceAtPosition(other.transform.position * 50, other.transform.position);
            Invoke("TimeDelay", 2f);
            GameManager.gameManagerInstance.TochControll();
        }


        if (other.CompareTag("Continer"))// Reach the Continner;
        {
            var paths = GameObject.FindGameObjectsWithTag("Path");
            var roads = GameObject.FindGameObjectsWithTag("Road");
            var conts = GameObject.FindGameObjectsWithTag("Continer");
            string contName = "Cont_" + gameObject.name;
            string pathName = "Path_" + gameObject.name;
            string roadName = "Road_" + gameObject.name;
            for (int i = 0; paths.Length > i; i++)
            {

                if (paths[i].name == pathName)
                {

                    paths[i].SetActive(false);
                    gameObject.SetActive(false);
                    roads[i].SetActive(false);
                    break;
                }
            }
            for (int i = 0; conts.Length > i; i++)
            {

                if (conts[i].name == contName)
                {

                    GameManager.gameManagerInstance.isContin = conts[i];
                    GameManager.gameManagerInstance.Sloting();
                    break;
                }
            }

        }
        if (other.gameObject.CompareTag("HitContiner"))
        {
            reverse = true;
            moveCar = true;
            GameManager.gameManagerInstance.tochLock = true;
            Debug.Log($"Car crashed with Continer : {other.gameObject}");
            GameManager.gameManagerInstance.TochControll();

        }
    }
    private void TimeDelay()
    {
        _rigidbody.isKinematic = _collider.isTrigger = true;
    }
}
#endif