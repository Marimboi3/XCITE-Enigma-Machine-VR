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
    private int knobSelectedLetter;
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
                    Debug.Log("Knob Position Chosen: " +  knobSelectedLetter);

                    Debug.Log("Knob Offset Again: " + knobOffset);

                    knobASCII = (int)knobSelectedLetter + knobOffset - 48;

                    if (knobASCII < 1)
                    {
                        knobASCII += 5;
                    }
                    else if (knobASCII > 5)
                    {
                        knobASCII -= 5;
                    }

                    //Debug.Log("Offset Knob Letter: " + (char)knobASCII);
                    Debug.Log("Offset ASCII: " + knobASCII);
                }
                else if (clickedObject.CompareTag("SideBase"))
                {
                    baseSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                    Debug.Log("Base Position Selected: " + (int)baseSelectedLetter);

                    baseASCII = (int)baseSelectedLetter - 48;
                    Debug.Log("Position Number: " + baseASCII);
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
            int localChange = knobASCII - baseASCII;
            Debug.Log("Message Side Offset: " +  localChange);

            cipherMechanism.ReceiveCons(localChange);

            isRotating = true;
            RotateToSelectedLetters();

            knobSelectedLetter = '\0';
            baseSelectedLetter = '\0';
        }
    }

    private void RotateToSelectedLetters()
    {
        int anglePerLetter = 360 / 5;

        int targetRotation = (int)currentRotation + (baseASCII - knobASCII) * anglePerLetter;
        Debug.Log("Target Rotation: " +  targetRotation);

        knobOffset = targetRotation / anglePerLetter;
        Debug.Log("Knob Offset: " +  knobOffset);

        Quaternion targetQuaternion = Quaternion.Euler(10f, 0, targetRotation);

        StartCoroutine(RotateKnob(targetQuaternion));
    }

    private IEnumerator RotateKnob(Quaternion targetRotation)
    {
        while (Quaternion.Angle(knob.localRotation, targetRotation) > 0.1f)
        {
            knob.localRotation = Quaternion.Slerp(knob.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isRotating = false;

        currentRotation = knob.localRotation.eulerAngles.z;
        //Debug.Log("Current Z Rotation: " +  currentRotation);
    }
}
