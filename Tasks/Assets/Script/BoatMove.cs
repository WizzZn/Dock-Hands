#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    public static BoatMove boatMoveInstance;

    [Header("Boat Settings")]
    public bool starter;
    public float speed;
    public float boatDistanse;
    public Vector3 boatPos;
    // Start is called before the first frame update
    void Start()
    {
        if(boatMoveInstance == null)
        {
            boatMoveInstance = this;

        }

        speed = 2f;
        //starter = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        Moving();
       
    }
    void Moving()
    {
        boatPos = new Vector3(30f, transform.position.y, transform.position.z);
        boatDistanse = Vector3.Distance(transform.position, boatPos);

        if (starter)
        {
            transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime, Space.World);
            if (boatDistanse < 0.1f) //Boat stop @ x = 30f
            {
                Debug.Log($"boat Dead : {gameObject.name}");
                gameObject.SetActive(false);
                starter = false;
            }
        }
    }
}
#endif