using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    List<int> rotorTurn = new List<int>() { 0, 0, 0 };
    private int letterNum;
    string word = null;
    string encryption = null;
    int wordIndex = 0;
    string alpha;
    //public Text encryption = null;

    public GameObject mechanism;
    public GameObject rotor1;
    public GameObject rotor2;
    public GameObject rotor3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void message(int text)
    {
        wordIndex++;
        word = word + (char)text;
        //encryption.text = word;
        //Debug.Log("Encryption: " + encryption.text);
        Debug.Log("Word: " + word);
        /*}

        // Update is called once per frame
        void Update()
        {
            int input = 65;*/
        //Wait for the user to input a letter

        //Input letter into the mechanism
        letterNum = mechanism.GetComponent<Mechanism>().letterInput(text);

        encryption = encryption + (char)letterNum;

        Debug.Log("Encrypted message is: " + encryption);


        rotorTurn[0] += 1;
        rotor1.GetComponent<Rotate1>().rotate();

        if (rotorTurn[0] > 25)
        {
            rotorTurn[0] -= 26;
            rotorTurn[1] += 1;
            rotor2.GetComponent<Rotate1>().rotate();
        }

        if (rotorTurn[1] > 25)
        {
            rotorTurn[1] -= 26;
            rotorTurn[2] += 1;
            rotor3.GetComponent<Rotate1>().rotate();
        }

        if (rotorTurn[2] > 25)
        {
            rotorTurn[2] -= 26;
        }

        //Output the letter into the light panel


    }

    public List<int> getRotor()
    {
        return rotorTurn;
    }
}
