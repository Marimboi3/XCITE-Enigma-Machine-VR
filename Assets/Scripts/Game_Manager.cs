using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public int rotor1Turn = 0;
    public int rotor2Turn = 0;
    public int rotor3Turn = 0;
    private int letterNum;
    string word = null;
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

        Debug.Log((char)letterNum);

        rotor1Turn += 1;
        rotor1.GetComponent<Rotate1>().rotate();

        if (rotor1Turn > 25)
        {
            rotor1Turn -= 26;
            rotor2Turn += 1;
            rotor2.GetComponent<Rotate1>().rotate();
        }

        if (rotor2Turn > 25)
        {
            rotor2Turn -= 26;
            rotor3Turn += 1;
            rotor3.GetComponent<Rotate1>().rotate();
        }

        if (rotor3Turn > 25)
        {
            rotor3Turn -= 26;
        }

        //Output the letter into the light panel


    }
}
