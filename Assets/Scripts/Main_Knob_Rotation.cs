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
    private char baseSelectedLetter;

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
                }
                else if (clickedObject.CompareTag("MainBase"))
                {
                    baseSelectedLetter = clickedObject.GetComponent<LetterInfo>().letter;
                }
            }

            if (!isRotating && knobSelectedLetter != '\0' && baseSelectedLetter != '\0')
            {
                isRotating = true;
                RotateToSelectedLetters();
            }
        }
    }

    private void RotateToSelectedLetters()
    {
        int knobIndex = knobSelectedLetter - 'A';
        int baseIndex = baseSelectedLetter - 'A';

        float anglePerLetter = 360f / 26;
        float targetRotation = knob.rotation.eulerAngles.z + (baseIndex - knobIndex) * anglePerLetter;

        Quaternion targetQuaternion = Quaternion.Euler(0f, 0f, targetRotation);

        StartCoroutine(RotateKnob(targetQuaternion));
    }

    private IEnumerator RotateKnob(Quaternion targetRotation)
    {
        while (Quaternion.Angle(knob.rotation, targetRotation) > 0.1f)
        {
            knob.rotation = Quaternion.Slerp(knob.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isRotating = false;
    }

/*    private Vector3 GetMouseWorldPosition()
    {
        Debug.Log(isRotating);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    private void RotateTowardsEndLocation()
    {
        Vector3 localEndLocation = transform.InverseTransformPoint(endLocation);
        float targetAngle = Mathf.Atan2(localEndLocation.y, localEndLocation.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
        {
            isRotating = false;
            Debug.Log("Stop Rotating!");
        }
    }*/

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
