using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEditor;
using System.Globalization;

public class Mechanism : MonoBehaviour
{
    public GameObject gameManager;

    private static int[] letters = new int[26] {65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 
                                                78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90};

    private static int[] rotorChange = new int[26] {83, 81, 68, 70, 72, 74, 76, 78, 69, 82, 84, 86, 90,
                                                    88, 66, 80, 65, 71, 75, 89, 73, 67, 77, 79, 87, 85};

    private static int[] reflectorChange = new int[26] {73, 88, 85, 72, 70, 69, 90, 68, 65, 79, 77, 84, 75,
                                                        81, 74, 87, 78, 83, 82, 76, 67, 89, 80, 66, 86, 71};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public int letterInput(int letter)
    {
        List<int> rotorTurn = gameManager.GetComponent<Game_Manager>().getRotor();

        //check letter from plug board to switch letter
        letter = plugBoard(letter);

        //go through rotors
        letter += rotorTurn[0];
        letter = rotor1(letter);
        letter -= rotorTurn[0];

        if (letter < 65)
        {
            letter += 26;
        }

        letter += rotorTurn[1];
        letter = rotor1(letter);
        letter -= rotorTurn[1];

        if (letter < 65)
        {
            letter += 26;
        }

        letter += rotorTurn[2];
        letter = rotor1(letter);
        letter -= rotorTurn[2];

        if (letter < 65)
        {
            letter += 26;
        }

        letter = reflector(letter);

        letter += rotorTurn[2];
        letter = rotor1return(letter);
        letter -= rotorTurn[2];

        if (letter < 65)
        {
            letter += 26;
        }

        letter += rotorTurn[1];
        letter = rotor1return(letter);
        letter -= rotorTurn[1];

        if (letter < 65)
        {
            letter += 26;
        }

        letter += rotorTurn[0];
        letter = rotor1return(letter);
        letter -= rotorTurn[0];

        if (letter < 65)
        {
            letter += 26;
        }

        //check plug board again
        letter = plugBoard(letter);

        return letter;
        //light up the final letter
    }


    //If a connection is done on the plugboard, switch letters
    public int plugBoard(int input)
    {

        return input;
    }

    private int reflector(int input)
    {
        int output;
        int check = ArrayUtility.IndexOf(letters, input);

        output = reflectorChange[check];
        
        return output;
    }

    //Does one letter switch through the rotor


    //reminder put back to private after testing.
    private int rotor1(int input)
    {
        if (input > 90)
        {
            input -= 26;
        }

        int output;
        int check = ArrayUtility.IndexOf(letters, input);

        output = rotorChange[check];

        return output;
    }

    private int rotor1return(int input)
    {
        if (input > 90)
        {
            input -= 26;
        }

        int output;
        int check = ArrayUtility.IndexOf(rotorChange, input);

        output = letters[check];

        return output;
    }


    /*
     *  Changes from Rotor               Changes from Reflector
     *   A     -->     S                    A     -->     I
     *   B     -->     Q                    B     -->     X
     *   C     -->     D                    C     -->     U
     *   D     -->     F                    D     -->     H
     *   E     -->     H                    E     -->     F
     *   F     -->     J                    F     -->     E
     *   G     -->     L                    G     -->     Z
     *   H     -->     N                    H     -->     D
     *   I     -->     E                    I     -->     A
     *   J     -->     R                    J     -->     O
     *   K     -->     T                    K     -->     M
     *   L     -->     V                    L     -->     T
     *   M     -->     Z                    M     -->     K
     *   N     -->     X                    N     -->     Q
     *   O     -->     B                    O     -->     J
     *   P     -->     P                    P     -->     W
     *   Q     -->     A                    Q     -->     N
     *   R     -->     G                    R     -->     S
     *   S     -->     K                    S     -->     R
     *   T     -->     Y                    T     -->     L
     *   U     -->     I                    U     -->     C
     *   V     -->     C                    V     -->     Y
     *   W     -->     M                    W     -->     P
     *   X     -->     O                    X     -->     B
     *   Y     -->     W                    Y     -->     V
     *   Z     -->     U                    Z     -->     G
    */
}
