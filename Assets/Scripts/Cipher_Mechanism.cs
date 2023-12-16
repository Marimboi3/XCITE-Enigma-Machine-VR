using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cipher_Mechanism : MonoBehaviour
{
    public GameObject mainKnob;

    private int changeAmount;
    private int zRotation;

    private string testMessage = "ALEXIS";
    private string newMessage;
    private int testAchar = (int)'A';
    private int testEchar = (int)'T';

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(testAchar);
        Debug.Log(testEchar);

        //pick letter change

        //rotate the knob
        changeAmount = mainKnob.GetComponent<Main_Knob_Rotation>().letterChange(testAchar, testEchar);

        //change the message
        messageOutput(testMessage, changeAmount);
    }

    public string messageOutput(string message, int change)
    {
        int[] asciiArray = message.Select(c => (int)c).ToArray();

        for(int i = 0; i < asciiArray.Length; i++)
        {
            asciiArray[i] += change;

            if (asciiArray[i] > 90)
            {
                asciiArray[i] -= 26;
            }

            newMessage += (char)asciiArray[i];

            Debug.Log((char)asciiArray[i]);
        }

        Debug.Log(newMessage);

        return message;
    }
}
