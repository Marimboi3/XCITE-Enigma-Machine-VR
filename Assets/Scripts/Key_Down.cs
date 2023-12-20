using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Down : MonoBehaviour
{
    private float speed = 5;
    private float totTravel = 0;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        //keyPress();
    }

    // Update is called once per frame
    public void keyPress()
    {
        if (active) 
        {
            if (totTravel <= 10)
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
                totTravel += speed * Time.deltaTime;
            }
            else if (totTravel >= 0)
            {

            }
        }
    }
}
