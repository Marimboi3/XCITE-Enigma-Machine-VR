using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Open_Top_Box : MonoBehaviour
{
    private float xAngle = 130;
    private float speed = 50;

    public bool open = false;

    private float totalRot = 0;

    private void Start()
    {
        Debug.Log("quarteron: " + gameObject.transform.rotation);
        Quaternion rotation = new Quaternion(0, 0, 180, 0);
        this.gameObject.transform.rotation = rotation;
    }
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
