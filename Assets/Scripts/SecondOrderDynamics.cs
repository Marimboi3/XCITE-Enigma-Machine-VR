using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondOrderDynamics : MonoBehaviour
{
    private Vector3 xp;
    private Vector3 y, yd;
    private float k1, k2, k3;
    private float T_crit;

    //private float PI = 3.14f;

    public SecondOrderDynamics(float f, float z, float r, Vector3 x0)
    {
        k1 = z / (Mathf.PI * f);
        k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);
        T_crit = 0.8f * (Mathf.Sqrt(4 * k2 + k1 * k1) - k1);

        xp = x0;
        y = x0;
        yd = Vector3.zero;
    }

    public Vector3 Update(float T, Vector3 x, Vector3 xd = null)
    {
        if (xd == null)
        {
            xd = (x - xp) / T;
            xp = x;
        }
        int iterations = (int)Mathf.Ceil(T / T_crit);
        T = T / iterations;
        for (int i = 0; i < iterations; i++)
        {
            y = y + T * yd;
            yd = yd + T * (x + k3 * xd - y - k1 * yd) / k2;
        }
        return y;
    }
}
