using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Knob_Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public int letterChange(int last, int current)
    {
        int totalChange;
        float rotationAngle;

        totalChange = current - last;

        rotationAngle = totalChange * 360f / 26f;

        Debug.Log(rotationAngle);

        transform.rotation *= Quaternion.Euler(0f, 0f, rotationAngle);

        return totalChange;
    }
}
