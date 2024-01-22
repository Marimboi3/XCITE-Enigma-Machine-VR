using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Knob_Rotation : MonoBehaviour
{
    // References to the knob object, rotation speed, and the cipher mechanism script
    public Transform knob;
    public float rotationSpeed = 5f;
    public Cipher_Mechanism cipherMechanism;
    public SwitchMech switchMech;

    // Variables to manage rotation state and user selections
    private bool isRotating = false;
    private bool leftSwitch = false;
    private char knobSelectedLetter;
    private int knobASCII;
    private char baseSelectedLetter;
    private int baseASCII;
    private int knobOffset = 0;
    private float currentRotation = 0;


    // Variables for making text glow
    public GameObject OuterText;
    public GameObject InnerText;

    // Update is called once per frame
    private void Update()
    {
        // Check for a mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                // Get the clicked object
                GameObject clickedObject = hit.collider.gameObject;

                // Check if the knob is clicked
                if (clickedObject.CompareTag("MainKnob"))
                {
                    //set flag
                    InnerText.GetComponent<TextGlow>().innerTextSelected = true;
                    // Retrieve the selected letter from the knob
                    knobSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                    Debug.Log("Actual Letter Chosen: " + knobSelectedLetter);

                    // Calculate ASCII value with knob offset
                    knobASCII = (int)knobSelectedLetter + knobOffset;

                    // Wrap around if ASCII value exceeds bounds
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
                // Check if the base is clicked
                else if (clickedObject.CompareTag("MainBase"))
                {
                    //set flag
                    OuterText.GetComponent<TextGlow>().outerTextSelected = true;

                    // Retrieve the selected letter from the base
                    baseSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                    Debug.Log("Selected Base Letter: " + baseSelectedLetter);

                    // Calculate ASCII value for the base letter
                    baseASCII = (int)baseSelectedLetter;
                    Debug.Log("Letter ASCII: " + baseASCII);
                }
                // Check if the vowel switch is clicked
                else if (clickedObject.CompareTag("Left"))
                {
                    if (leftSwitch)
                    {
                        leftSwitch = false;
                        
                        cipherMechanism.LeftSwitch(leftSwitch);
                        switchMech.LeftSwitch(leftSwitch);
                    }
                    else
                    {
                        leftSwitch = true;
                        
                        cipherMechanism.LeftSwitch(leftSwitch);
                        switchMech.LeftSwitch(leftSwitch);
                    }
                }
            }

            // Check if the knob is not rotating and both letters are selected
            if (!isRotating && knobSelectedLetter != '\0' && baseSelectedLetter != '\0')
            {
                // Calculate the offset between knob and base letters
                int localChange = (int)knobSelectedLetter - baseASCII;
                Debug.Log("Message Offset: " + localChange);

                // Send the offset to the Cipher_Mechanism script
                cipherMechanism.ReceiveInteger(localChange);

                // Start knob rotation
                isRotating = true;
                RotateToSelectedLetters();

                // Reset selected letters
                knobSelectedLetter = '\0';
                baseSelectedLetter = '\0';
            }
        }
    }

    // Rotate the knob to align with the selected letters
    private void RotateToSelectedLetters()
    {
        // Calculate the index of knob and base letters in the alphabet
        int knobIndex = knobASCII - 'A';
        int baseIndex = baseASCII - 'A';

        // Calculate the angle per letter in the alphabet
        float anglePerLetter = 360f / 26;

        // Calculate the target rotation angle for the knob
        float targetRotation = currentRotation + (baseIndex - knobIndex) * anglePerLetter;

        // Calculate the knob offset based on the target rotation
        knobOffset = Mathf.RoundToInt(targetRotation / anglePerLetter);

        // Create a quaternion for the target rotation
        Quaternion targetQuaternion = Quaternion.Euler(10f, 0f, targetRotation);

        // Start the coroutine to smoothly rotate the knob
        StartCoroutine(RotateKnob(targetQuaternion));
    }

    // Coroutine to smoothly rotate the knob
    private IEnumerator RotateKnob(Quaternion targetRotation)
    {
        // Continue rotating until the angle difference is small
        while (Quaternion.Angle(knob.localRotation, targetRotation) > 0.1f)
        {
            // Interpolate the knob rotation towards the target rotation
            knob.localRotation = Quaternion.Slerp(knob.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // Knob rotation is complete
        isRotating = false;

        // Update the current rotation angle
        currentRotation = knob.localRotation.eulerAngles.z;
    }
}
