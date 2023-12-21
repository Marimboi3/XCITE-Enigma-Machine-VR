using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side_Knob_Rotation : MonoBehaviour
{
    public Transform knob;
    public float rotationSpeed = 5f;
    public Cipher_Mechanism cipherMechanism;

    private bool isRotating = false;
    private char knobSelectedLetter;
    private int knobASCII;
    private char baseSelectedLetter;
    private int baseASCII;
    private int knobOffset = 0;
    private float currentRotation = 0;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.CompareTag("SideKnob"))
                {
                    knobSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                    Debug.Log("Side Letter Chosen: " +  knobSelectedLetter);

                    knobASCII = (int)knobSelectedLetter + knobOffset;

                    if (knobASCII > 90)
                    {
                        knobASCII -= 26;
                    }
                    else if (knobASCII < 65)
                    {
                        knobASCII += 26;
                    }

                    Debug.Log("Offset Knob Letter: " + (char)knobASCII);
                    Debug.Log("Offset ASCII: " + knobASCII);
                }
                else if (clickedObject.CompareTag("SideBase"))
                {
                    baseSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                    Debug.Log("Selected Base Letter: " + baseSelectedLetter);

                    baseASCII = (int)baseSelectedLetter;
                    Debug.Log("Letter ASCII: " + baseASCII);
                }
            }
        }

        if(!isRotating && knobSelectedLetter != '\0' && baseSelectedLetter != '\0')
        {
            int localChange = (int)knobSelectedLetter - baseASCII;
        }
    }
}
