// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the Key_Down class
public class Key_Down : MonoBehaviour
{
    // Reference to the GameManager object
    public GameObject gameManager;

    // Flag to track if the key is currently pressed
    private bool isPressed = false;

    // Method to handle key press
    public void HandleKeyPress()
    {
        // Check if the key is not already pressed
        if (!isPressed)
        {
            // Extract the key letter from the object's name
            string keyLetter = gameObject.name.Replace("_Key", "");
            Debug.Log("Key Pressed: " + keyLetter);

            // Convert the key letter to a character and get its ASCII value
            char character = keyLetter[0];
            int asciiValue = (int)character;
            Debug.Log("Ascii Value = " + asciiValue);

            // Call the message method in the GameManager with the ASCII value
            gameManager.GetComponent<Game_Manager>().message(asciiValue);

            // Trigger the key press animation
            StartCoroutine(KeyPressAnimation());

            // Set the flag to indicate that the key is pressed
            isPressed = true;
        }
    }

    // Coroutine for the key press animation
    private IEnumerator KeyPressAnimation()
    {
        // Animation duration
        float duration = 0.1f;

        // Store the original and target positions for the animation
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition - new Vector3(0, 0.1f, 0);

        // First phase of the animation: move down
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Second phase of the animation: move back up
        timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the key is back at its original position
        transform.position = originalPosition;

        // Reset the flag to indicate that the key is no longer pressed
        isPressed = false;
    }
}
