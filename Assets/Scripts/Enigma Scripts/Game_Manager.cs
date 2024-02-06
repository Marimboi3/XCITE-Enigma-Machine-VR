// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Define the Game_Manager class
public class Game_Manager : MonoBehaviour
{
    // Variables to keep track of rotor positions, letter input, word, encryption, and word index
    List<int> rotorTurn = new List<int>() { 0, 0, 0 };
    private int letterNum;
    string word = null;
    string encryption = null;
    int wordIndex = 0;

    // References to game objects in the scene
    public GameObject mechanism;
    public GameObject rotor1;
    public GameObject rotor2;
    public GameObject rotor3;
    public GameObject Lights;

    // Method to handle incoming messages (letters)
    public void message(int text)
    {
        // Increment the word index and append the received letter to the word
        wordIndex++;
        word = word + (char)text;

        // Log the current word
        Debug.Log("Word: " + word);

        // Input the letter into the mechanism and get the corresponding number
        letterNum = mechanism.GetComponent<Mechanism>().letterInput(text);

        // Trigger the light-up effect for the letter
        Lights.GetComponent<Light_Up>().GlowLetter((char)letterNum);

        // Update the encrypted message with the transformed letter
        encryption = encryption + (char)letterNum;

        // Log the encrypted message
        Debug.Log("Encrypted message is: " + encryption);

        // Rotate the first rotor and update its position
        rotorTurn[0] += 1;
        rotor1.GetComponent<Rotate1>().rotate();

        // Check if the first rotor completed a full rotation and rotate the second rotor
        if (rotorTurn[0] > 25)
        {
            rotorTurn[0] -= 26;
            rotorTurn[1] += 1;
            rotor2.GetComponent<Rotate1>().rotate();
        }

        // Check if the second rotor completed a full rotation and rotate the third rotor
        if (rotorTurn[1] > 25)
        {
            rotorTurn[1] -= 26;
            rotorTurn[2] += 1;
            rotor3.GetComponent<Rotate1>().rotate();
        }

        // Check if the third rotor completed a full rotation
        if (rotorTurn[2] > 25)
        {
            rotorTurn[2] -= 26;
        }
    }

    // Method to retrieve the current rotor positions
    public List<int> getRotor()
    {
        return rotorTurn;
    }
}
