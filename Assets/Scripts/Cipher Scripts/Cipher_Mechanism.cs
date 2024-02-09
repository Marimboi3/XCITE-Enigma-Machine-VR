using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Cipher_Mechanism : MonoBehaviour
{
    // Reference to the main knob object
    public GameObject mainKnob;
    public Display_Text screen;

    // Flag indicating if the knob has been rotated
    private bool knobRotated = false;
    private bool vowelSwitch = false;
    private bool consSwitch = false;

    // Amount of change received from the knob rotation
    private int changeAmount;
    private int cons;

    // Initial test message
    private string testMessage = "UCR XCITE IDEA LAB";


    // Variables to store the ascii and new messages after the rotation
    public string newMessage;
    private string asciiMessage;

    private static int[] sideLetters = new int[5] { 83, 84, 76, 82, 78 };

    void Start()
    {
        screen.recieveOriginal(testMessage);
        screen.recieveEncryption(testMessage);
    }
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

            screen.recieveEncryption(newMessage);

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

            if (vowelSwitch)
            {
                asciiArray[i] = vowelChange(asciiArray[i], change);

                if (asciiArray[i] >= 0 && asciiArray[i] <= 4)
                {
                    // Build the message string with ASCII values
                    asciiMessage += asciiArray[i] + " ";

                    // Build the new message string with characters
                    newMessage += asciiArray[i];
                }
                /*else
                {
                    // Build the message string with ASCII values
                    asciiMessage += asciiArray[i] + " ";

                    // Build the new message string with characters
                    newMessage += (char)asciiArray[i];
                }*/
            }/*
            else if (consSwitch)
            {
                asciiArray[i] = consChange(asciiArray[i], cons);

                asciiMessage += asciiArray[i] + " ";

                newMessage += (char)asciiArray[i];
            }*/

            if (consSwitch)
            {
                asciiArray[i] = consChange(asciiArray[i], cons);
            }

            if (asciiArray[i] > 5)
            {
                // Build the message string with ASCII values
                asciiMessage += asciiArray[i] + " ";

                // Build the new message string with characters
                newMessage += (char)asciiArray[i];
            }

            /*else
            {

                

                if (consSwitch)
                {
                    asciiArray[i] = consChange(asciiArray[i], cons);
                }

                // Build the message string with ASCII values
                asciiMessage += asciiArray[i] + " ";

                // Build the new message string with characters
                newMessage += (char)asciiArray[i];
            }*/
            
            
        }

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
            default:/*
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
                }*/
                break;

        }
        return value;
    }

    private int consChange(int value, int cons)
    {
        int tempChange;

        switch (value)
        {
            case 83:
                value = sideLetters[cons];
                break;
            case 84:
                tempChange = cons + 1;

                if (tempChange > 4)
                {
                    tempChange -= 5;
                }

                value = sideLetters[tempChange];
                break;
            case 76:
                tempChange = cons + 2;

                if (tempChange > 4)
                {
                    tempChange -= 5;
                }

                value = sideLetters[tempChange];
                break;
            case 82:
                tempChange = cons + 3;

                if (tempChange > 4)
                {
                    tempChange -= 5;
                }

                value = sideLetters[tempChange];
                break;
            case 78:
                tempChange = cons + 4;

                if (tempChange > 4)
                {
                    tempChange -= 5;
                }

                value = sideLetters[tempChange];
                break;
            default:
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

    public void ReceiveCons(int change)
    {
        cons = change;
        knobRotated = true;
        Debug.Log("Cipher Mechanism Received: " + cons);
    }

    public void LeftSwitch(bool state)
    {
        vowelSwitch = state;
        knobRotated = true;
        Debug.Log(vowelSwitch);
    }

    public void RightSwitch(bool state)
    {
        consSwitch = state;
        knobRotated = true;
        Debug.Log(consSwitch);
    }

    public bool GetRightSwitch()
    {
        return consSwitch;
    }
}
