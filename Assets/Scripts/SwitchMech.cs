using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMech : MonoBehaviour
{
    public bool vowelSwitch = false;
    public bool switchOn = false;

    // Update is called once per frame
    void Update()
    {
        if (switchOn && vowelSwitch)
        {
            transform.Rotate(Vector3.right, -90f);
            vowelSwitch = false;
            Debug.Log("I turned On!!!");
        }
        else if (!switchOn && vowelSwitch)
        {
            transform.Rotate(Vector3.right, 90f);
            vowelSwitch = false;
            Debug.Log("I turned Off ._. ");
        }
    }

    public void LeftSwitch(bool state)
    {
        switchOn = state;
        vowelSwitch = true;
    }

    public void RightSwitch(bool state)
    {
        Debug.Log("Switched right!");
        switchOn = state;
        vowelSwitch = true;
    }
}
