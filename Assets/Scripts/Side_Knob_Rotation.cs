using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side_Knob_Rotation : MonoBehaviour
{
    public Transform knob;
    public float rotationSpeed = 5f;
    public Cipher_Mechanism cipherMechanism;
    public SwitchMech switchMech;

    private bool isRotating = false;
    private bool rightSwitch = false;
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

                    knobASCII = (int)knobSelectedLetter;

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
                else if (clickedObject.CompareTag("Right"))
                {
                    if (rightSwitch)
                    {
                        rightSwitch = false;

                        cipherMechanism.RightSwitch(rightSwitch);
                        switchMech.RightSwitch(rightSwitch);
                    }
                    else
                    {
                        rightSwitch = true;

                        cipherMechanism.RightSwitch(rightSwitch);
                        switchMech.RightSwitch(rightSwitch);
                    }
                }
            }
        }

        if(!isRotating && knobSelectedLetter != '\0' && baseSelectedLetter != '\0')
        {
            int localChange = (int)knobSelectedLetter - baseASCII;
            Debug.Log("Message Side Offset: " +  localChange);


        }
    }
}
