using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cipher_Mechanism : MonoBehaviour
{
    // Reference to the main knob object
    public GameObject mainKnob;

    // Flag indicating if the knob has been rotated
    private bool knobRotated = false;

    // Amount of change received from the knob rotation
    private int changeAmount;

    // Initial test message
    private string testMessage = "ALEXIS";

    // Variables to store the old and new messages after the rotation
    private string newMessage;
    private string oldMessage;

    // Update is called once per frame
    void Update()
    {
        // Check if the knob has been rotated
        if (knobRotated)
        {
            // Reset the flag
            knobRotated = false;

            // Call the messageOutput function to apply the Caesar cipher
            messageOutput(testMessage, changeAmount);
        }
    }

    // Function to apply the Caesar cipher to a given message
    public string messageOutput(string message, int change)
    {
        // Reset the old and new message strings
        newMessage = "";
        oldMessage = "";

        // Convert the message to an array of ASCII values
        int[] asciiArray = message.Select(c => (int)c).ToArray();

        // Iterate through each character in the ASCII array
        for (int i = 0; i < asciiArray.Length; i++)
        {
            // Apply the Caesar cipher by adding the change amount
            asciiArray[i] += change;

            // Wrap around if the ASCII value exceeds bounds
            if (asciiArray[i] > 90)
            {
                asciiArray[i] -= 26;
            }
            else if (asciiArray[i] < 65)
            {
                asciiArray[i] += 26;
            }

            // Build the old message string with ASCII values
            oldMessage += asciiArray[i] + " ";

            // Build the new message string with characters
            newMessage += (char)asciiArray[i];
        }

        // Log the old and new messages to the console
        Debug.Log("Old Message (ASCII): " + oldMessage);
        Debug.Log("New Message: " + newMessage);

        // Return the original message (unused in the current implementation)
        return message;
    }

    // Function to receive the change amount from the knob rotation
    public void ReceiveInteger(int receivedInteger)
    {
        // Set the change amount received from the knob
        changeAmount = receivedInteger;

        // Set the flag to indicate that the knob has been rotated
        knobRotated = true;
    }
}
