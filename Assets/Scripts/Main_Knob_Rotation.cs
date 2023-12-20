using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Knob_Rotation : MonoBehaviour
{
    public Transform knob;
    public Transform baseLetters;
    public float rotationSpeed = 5f;

    private bool isRotating = false;
    private char knobSelectedLetter;
    private int knobASCII;
    private char baseSelectedLetter;
    private int baseASCII;
    private int knobOffset = 0;
    private float currentRotation = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.CompareTag("MainKnob"))
                {
                    knobSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                    Debug.Log("Actual Letter Chosen: " + knobSelectedLetter);
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
                else if (clickedObject.CompareTag("MainBase"))
                {
                    baseSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                    Debug.Log("Selected Base Letter: " + baseSelectedLetter);
                    baseASCII = (int)baseSelectedLetter;
                    Debug.Log("Letter ASCII: " + baseASCII);
                }
            }

            if (!isRotating && knobSelectedLetter != '\0' && baseSelectedLetter != '\0')
            {
                int localChange = knobASCII - baseASCII;
                Debug.Log("Message Offset: " +  localChange);

                isRotating = true;
                RotateToSelectedLetters();

                knobSelectedLetter = '\0';
                baseSelectedLetter = '\0';
            }
        }
    }

    private void RotateToSelectedLetters()
    {
        int knobIndex = knobASCII - 'A';
        int baseIndex = baseASCII - 'A';

        Debug.Log("knob index: " + knobIndex);
        Debug.Log("base index: " + baseIndex);

        float anglePerLetter = 360f / 26;

        Debug.Log("Rotation Before: " + currentRotation);

        float targetRotation = currentRotation + (baseIndex - knobIndex) * anglePerLetter;

        knobOffset = Mathf.RoundToInt(targetRotation / anglePerLetter);

        //Debug.Log("Target Rotation: " + targetRotation);
        //Debug.Log("Positiones Moved: " + targetRotation / anglePerLetter);
        //Debug.Log("Knob Offset: " + knobOffset);

        Quaternion targetQuaternion = Quaternion.Euler(10f, 0f, targetRotation);

        StartCoroutine(RotateKnob(targetQuaternion));
    }

    private IEnumerator RotateKnob(Quaternion targetRotation)
    {
        while (Quaternion.Angle(knob.localRotation, targetRotation) > 0.1f)
        {
            //Debug.Log(Quaternion.Angle(knob.localRotation, targetRotation));
            //Debug.Log("Current Rotation: " + knob.localRotation.eulerAngles);
            knob.localRotation = Quaternion.Slerp(knob.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isRotating = false;

        currentRotation = knob.localRotation.eulerAngles.z;
        Debug.Log("Rotation After: " + currentRotation);
    }

   /* public int letterChange(int last, int current)
    {
        int totalChange;
        float rotationAngle;

        totalChange = current - last;

        rotationAngle = totalChange * 360f / 26f;

        Debug.Log(rotationAngle);

        transform.rotation *= Quaternion.Euler(0f, 0f, rotationAngle);

        return totalChange;
    }*/
}
