using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
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
        for (int i = 0; i < input.Length; i++)
        {
            //input letter from the keyboard
            letter = input[i];

            //check letter from plug board to switch letter
            letter = plugBoard(letter);

            //go through rotors
            letter = rotor(letter);

            letter = rotor(letter);

            letter = rotor(letter);

            letter = reflector(letter);

            letter = rotor(letter);

            letter = rotor(letter);

            letter = rotor(letter);

            //check plug board again
            letter = plugBoard(letter);

            //light up the final letter
        }
    }

    //If a connection is done on the plugboard, switch letters
    public char plugBoard(int input)
    {

        return ' ';
    }


    // ARRAY [INT] ARRAY = {05 06 07 ...}
    // ARRAY [INT] ARRAY2 = {73 88 85 ...}

    //FUNCTION(INT NUM) { 

    // ARRAY[NUM]
    // ARRAY2[NUM] 

    //}

    private int reflector(int input)
    {
        switch(letter)
        {
            //A to I
            case 65:
                input = 73;
                return input;
            //B to X
            case 66:
                input = 88;
                return input;
            //C to U
            case 67:
                input = 85;
                return input;
            //D to H
            case 68:
                input = 72;
                return input;
            //E to F
            case 69:
                input = 70;
                return input;
            //F to E
            case 70:
                input = 69;
                return input;
            //G to Z
            case 71:
                input = 90;
                return input;
            //H to D
            case 72:
                input = 68;
                return input;
            //I to A
            case 73:
                input = 65;
                return input;
            //J to O
            case 74:
                input = 79;
                return input;
            //K to M
            case 75:
                input = 77;
                return input;
            //L to T
            case 76:
                input = 84;
                return input;
            //M to K
            case 77:
                input = 75;
                return input;
            //N to Q
            case 78:
                input = 81;
                return input;
            //O to J
            case 79:
                input = 74;
                return input;
            //P to W
            case 80:
                input = 87;
                return input;
            //Q to N
            case 81:
                input = 78;
                return input;
            //R to S
            case 82:
                input = 83;
                return input;
            //S to R
            case 83:
                input = 82;
                return input;
            //T to L
            case 84:
                input = 76;
                return input;
            //U to C
            case 85:
                input = 67;
                return input;
            //V to Y
            case 86:
                input = 89;
                return input;
            //W to P
            case 87:
                input = 80;
                return input;
            //X to B
            case 88:
                input = 66;
                return input;
            //Y to V
            case 89:
                input = 86;
                return input;
            //Z to G
            case 90:
                input = 71;
                return input;
            default:
                break;
        }
        return input;
    }

    //Does one letter switch through the rotor


    //reminder put back to private after testing.
    public int rotor(int input)
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
                input = 84;
                return input;
            //L to V
            case 76:
                input = 86;
                return input;
            //M to Z
            case 77:
                input = 90;
                return input;
            //N to X
            case 78:
                input = 88;
                return input;
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
            case 84:
                input = 89;
                return input;
            //U to I
            case 85:
                input = 73;
                return input;
            //V to C
            case 86:
                input = 67;
                return input;
            //W to M
            case 87:
                input = 77;
                return input;
            //X to O
            case 88:
                input = 79;
                return input;
            //Y to W
            case 89:
                input = 87;
                return input;
            //Z to U
            case 90:
                input = 85;
                return input;
            default:
                break;
        }
        return ' ';
    }
}
