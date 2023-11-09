using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotate1 : MonoBehaviour
{
    private float angle = 360 / 26;
    //private float totalRot = 0;
    //private float speed = 80;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 0, 0, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rotate()
    {
        transform.Rotate(angle, 0, 0);
    }
}
