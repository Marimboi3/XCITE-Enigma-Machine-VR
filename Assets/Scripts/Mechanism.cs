using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    public string input;

    private int letter;

    // Start is called before the first frame update
    void Start()
    {
        int num = 65;
        Debug.Log("charcter should be A: " + (char)num);
        Debug.Log("charcter should be Z: " + (char)(num + 25));
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < input.Length; i++)
        {
            letter = input[i];

            //input letter from the keyboard
            letter = plugBoard(letter);

            //check letter from plug board to switch letter
            letter = rotor(letter);
            //go through rotors

            //check plug board again

            //light up the final letter
        }
    }

    //If a connection is done on the plugboard, switch letters
    public char plugBoard(int input)
    {

        return ' ';
    }

    //Does one letter switch through the rotor
    private int rotor(int input)
    {

        switch (input)
        {
            //A to S
            case 65:
                input = 83;
                return input;
            //B to Q
            case 66:
                input = 81;
                return input;
            //C to D
            case 67:
                input = 68;
                return input;
            //D to F
            case 68:
                input = 70;
                return input;
            //E to H
            case 69:
                input = 72;
                return input;
            //F to J
            case 70:
                input = 74;
                return input;
            //G to L
            case 71:
                input = 76;
                return input;
            //H to N
            case 72:
                input = 78;
                return input;
            //I to E
            case 73:
                input = 69;
                return input;
            //J to R
            case 74:
                input = 82;
                return input;
            //K to T
            case 75:
                return 'T';
            //L to V
            case 76:
                return 'V';
            //M to Z
            case 77:
                return 'Z';
            //N to X
            case 78:
                return 'X';
            //O to B
            case 79:
                input = 66;
                return input;
            //P to P
            case 80:
                input = 80;
                return input;
            //Q to A
            case 81:
                input = 65;
                return input;
            //R to G
            case 82:
                input = 71;
                return input;
            //S to K
            case 83:
                input = 75;
                return input;
            //T to Y
            case 'T':
                return 'Y';
            //U to I
            case 'U':
                input = 73;
                return input;
            //V to C
            case 'V':
                input = 67;
                return input;
            //W to M
            case 'W':
                input = 77;
                return input;
            //X to O
            case 'X':
                input = 79;
                return input;
            //Y to W
            case 'Y':
                return 'W';
            //Z to U
            case 'Z':
                return 'U';
            default:
                break;
        }
        return ' ';
    }
}
