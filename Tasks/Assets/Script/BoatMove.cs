using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    public static BoatMove boatMoveInstance;
    public bool starter;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        boatMoveInstance = this;
        starter = true;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }
    void Moving()
    {
        if (starter)
        {
             transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime, Space.World);
        }
    }
}
