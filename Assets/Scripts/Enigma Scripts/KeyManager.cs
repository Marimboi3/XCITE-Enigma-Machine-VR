// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the KeyManager class
public class KeyManager : MonoBehaviour
{
    // Flag to track if a key has been pressed
    private bool keyPressed = false;

    // Update is called once per frame
    private void Update()
    {
        // Check if the left mouse button is pressed and a key has not been pressed
        if (Input.GetMouseButtonDown(0) && !keyPressed)
        {
            // Cast a ray from the camera to the mouse position
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if the ray hits any game object
            if (Physics.Raycast(ray, out hit))
            {
                // Retrieve the Key_Down script from the hit object
                Key_Down keyScript = hit.collider.gameObject.GetComponent<Key_Down>();

                // Check if the Key_Down script is attached to the object
                if (keyScript != null)
                {
                    // Trigger the key press functionality in the Key_Down script
                    keyScript.HandleKeyPress();

                    // Set the flag to indicate that a key has been pressed
                    keyPressed = true;
                }
            }
        }
        // Check if the left mouse button is released
        else if (Input.GetMouseButtonUp(0))
        {
            // Reset the flag when the mouse button is released
            keyPressed = false;
        }
    }
}
