using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Open_Top_Box : MonoBehaviour
{
    private float xAngle = 130;
    private float speed = 50;

    private bool open = false;

    private float totalRot = 0;
/*
    private void Start()
    {

        int num = 65 + 25;
        Debug.Log("charcter should be A: " + (char)num);
        Debug.Log("charcter should be Z: " + (char)num);
    }*/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            open = !open;
        }
        if (open)
        {
            if (totalRot <= xAngle)
            {
                transform.Rotate(speed * Time.deltaTime, 0, 0);
                totalRot += speed * Time.deltaTime;
            }
        }

        else if (!open)
        {
            if (totalRot >= 0)
            {
                transform.Rotate(-speed * Time.deltaTime, 0, 0);
                totalRot -= speed * Time.deltaTime;
            }
        }
    }
}
