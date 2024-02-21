using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side_Knob_Rotation : MonoBehaviour
{
    // Public variables accessible from Unity Editor
    public Transform knob;
    public float rotationSpeed = 5f;
    public Cipher_Mechanism cipherMechanism;
    public SwitchMech switchMech;

    // Private variables for internal use
    private bool isRotating = false;
    private bool rightSwitch = false;
    private int knobSelectedLetter;
    private int knobASCII;
    private char baseSelectedLetter;
    private int baseASCII;
    private int knobOffset = 0;
    private float currentRotation = 0;

    // Variables for making text glow
    public GameObject SideKnob;

    // Update is called once per frame
    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if the ray hits any game object
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Check if the clicked object is a side knob
                if (clickedObject.CompareTag("SideKnob") && rightSwitch)
                {
                    //set flag
                    SideKnob.GetComponent<SideKnobGlow>().innerTextSelected = true;
                    //glow individual letter
                    SideKnob.GetComponent<SideKnobGlow>().GlowLetter(clickedObject, false);

                    knobSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                    //Debug.Log("Knob Position Chosen: " + (int)knobSelectedLetter);

                    //Debug.Log("Knob Offset Again: " + knobOffset);

                    // Calculate ASCII value for the knob
                    knobASCII = (int)knobSelectedLetter + knobOffset - 48;

                    // Ensure knobASCII is within a specific range
                    if (knobASCII < 1)
                    {
                        knobASCII += 5;
                    }
                    else if (knobASCII > 5)
                    {
                        knobASCII -= 5;
                    }

                    // Debug.Log("Offset Knob Letter: " + (char)knobASCII);
                    //Debug.Log("Letter Chosen ASCII: " + knobASCII);
                }
                // Check if the clicked object is a side base
                else if (clickedObject.CompareTag("SideBase") && rightSwitch)
                {
                    //set flag
                    SideKnob.GetComponent<SideKnobGlow>().outerTextSelected = true;
                    //glow individual letter
                    SideKnob.GetComponent<SideKnobGlow>().GlowLetter(clickedObject, true);

                    baseSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                    //Debug.Log("Base Position Selected: " + (int)baseSelectedLetter);

                    // Calculate ASCII value for the base
                    baseASCII = (int)baseSelectedLetter - 48;
                    //Debug.Log("Position Number: " + baseASCII);
                }
                // Check if the clicked object is the "Right" switch
                else if (clickedObject.CompareTag("Right"))
                {
                    // Toggle the state of the right switch
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

        // Check conditions for knob rotation
        if (!isRotating && knobSelectedLetter != '\0' && baseSelectedLetter != '\0' && rightSwitch)
        {
            // Start rotating the knob to the selected letters
            isRotating = true;
            RotateToSelectedLetters();

            // Reset selected letters
            knobSelectedLetter = '\0';
            baseSelectedLetter = '\0';
        }
    }

    // Rotate the knob to the selected letters
    private void RotateToSelectedLetters()
    {
        // Set the angle per letter (assuming 5 letters)
        int anglePerLetter = 72;

        // Calculate the target rotation
        float rawTargetRotation = currentRotation + (baseASCII - knobASCII) * anglePerLetter;
        // Make sure it is divisible by 72
        float targetRotation = Mathf.Round(rawTargetRotation / 72) * 72;

        if (targetRotation < 0)
        {
            targetRotation += 360;
        }
        else if (targetRotation > 360)
        {
            targetRotation -= 360;
        }

        //Debug.Log("Target Rotation: " + targetRotation);

        // Calculate the knob offset based on the target rotation
        knobOffset = ((int)targetRotation) / anglePerLetter;
        //Debug.Log("Knob Offset: " + knobOffset);

        // Ensure knob offset is positive
        if (knobOffset < 0)
        {
            knobOffset += 5;
        }
        else if (knobOffset > 5)
        {
            knobOffset -= 5;
        }

        // Prepare consonant offset to be sent to cipher mechanism
        int consOffset = 5 - knobOffset;

        // If variable equals 5, then bring it down to 0
        if (consOffset >= 5)
        {
            consOffset -= 5;
        }

        // Inform the cipher mechanism about the local change
        cipherMechanism.ReceiveCons(consOffset);

        //Debug.Log("Int sent to Cipher Mech: " + consOffset);

        // Ensure target rotation is within a specific range
        targetRotation %= 360;

        // Convert the target rotation to a quaternion for smooth rotation
        Quaternion targetQuaternion = Quaternion.Euler(10f, 0, targetRotation);

        // Start coroutine to gradually rotate the knob
        StartCoroutine(RotateKnob(targetQuaternion));
    }

    // Coroutine to smoothly rotate the knob
    private IEnumerator RotateKnob(Quaternion targetRotation)
    {
        while (Quaternion.Angle(knob.localRotation, targetRotation) > 0.1f)
        {
            knob.localRotation = Quaternion.Slerp(knob.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // End rotation and update current rotation
        isRotating = false;
        currentRotation = knob.localRotation.eulerAngles.z;
        //Debug.Log("Current Z Rotation: " + currentRotation);
    }
}
