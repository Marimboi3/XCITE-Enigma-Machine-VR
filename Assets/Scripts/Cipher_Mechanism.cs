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
    private bool vowelSwitch = false;
    private bool consSwitch = false;

    // Amount of change received from the knob rotation
    private int changeAmount;
    private int cons;

    // Initial test message
    private string testMessage = "ALEXIS ROCKS";

    // Variables to store the ascii and new messages after the rotation
    private string newMessage;
    private string asciiMessage;

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

            // Log the ascii and new messages to the console
            Debug.Log("ASCII Message: " + asciiMessage);
            Debug.Log("New Message: " + newMessage);
        }
    }

    // Function to apply the Caesar cipher to a given message
    public string messageOutput(string message, int change)
    {
        // Reset the ascii and new message strings
        newMessage = "";
        asciiMessage = "";

        // Convert the message to an array of ASCII values
        int[] asciiArray = message.Select(c => (int)c).ToArray();

        // Iterate through each character in the ASCII array
        for (int i = 0; i < asciiArray.Length; i++)
        {
            if (vowelSwitch)
            {
                asciiArray[i] = vowelChange(asciiArray[i], change);

                if (consSwitch)
                {
                    asciiArray[i] = consChange(asciiArray[i], cons);
                }

                if (asciiArray[i] >= 0 && asciiArray[i] <= 4)
                {
                    // Build the message string with ASCII values
                    asciiMessage += asciiArray[i] + " ";

                    // Build the new message string with characters
                    newMessage += asciiArray[i];
                }
                else
                {
                    // Build the message string with ASCII values
                    asciiMessage += asciiArray[i] + " ";

                    // Build the new message string with characters
                    newMessage += (char)asciiArray[i];
                }
            }
            else
            {
                if (asciiArray[i] != 32)
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
                }

                // Build the message string with ASCII values
                asciiMessage += asciiArray[i] + " ";

                // Build the new message string with characters
                newMessage += (char)asciiArray[i];
            }
            
            
        }

        // Log the ascii and new messages to the console
        Debug.Log("ASCII Message: " + asciiMessage);
        Debug.Log("New Message: " + newMessage);

        // Return the original message (unused in the current implementation)
        return message;
    }

    private int vowelChange(int value, int change)
    {
        switch (value)
        {
            case 65:
                value = 0;
                break;
            case 69:
                value = 1;
                break;
            case 73:
                value = 2;
                break;
            case 79:
                value = 3;
                break;
            case 85:
                value = 4;
                break;
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 32:
                break;
            default:
                // Apply the Caesar cipher by adding the change amount
                value += change;

                // Wrap around if the ASCII value exceeds bounds
                if (value > 90)
                {
                    value -= 26;
                }
                else if (value < 65)
                {
                    value += 26;
                }
                break;

        }
        return value;
    }

    private int consChange(int value, int cons)
    {
        switch(value)
        {
            case 0:
                break;
        }
        return value;
    }

    // Function to receive the change amount from the knob rotation
    public void ReceiveInteger(int receivedInteger)
    {
        // Set the change amount received from the knob
        changeAmount = receivedInteger;

        // Set the flag to indicate that the knob has been rotated
        knobRotated = true;
    }

    public void LeftSwitch(bool state)
    {
        vowelSwitch = state;
        knobRotated = true;
        Debug.Log(vowelSwitch);
    }

    public void RightSwitch(bool state, int change)
    {
        consSwitch = state;
        cons = change;
        knobRotated = true;
        Debug.Log(consSwitch);
    }
}
