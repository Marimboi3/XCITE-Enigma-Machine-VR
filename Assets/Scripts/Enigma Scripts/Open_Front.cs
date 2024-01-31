using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Open_Front : MonoBehaviour
{
    private float xAngle = 90;
    private float speed = 50;

    public bool open = false;

    private float totalRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 0, 0, Space.World);
    }

    // Update is called once per frame
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