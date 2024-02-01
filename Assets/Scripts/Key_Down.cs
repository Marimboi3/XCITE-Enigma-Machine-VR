using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Down : MonoBehaviour
{
    public int letter;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
            }
        }
    }
}
