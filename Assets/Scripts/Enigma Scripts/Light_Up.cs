using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Up : MonoBehaviour
{
    //Material objects to change letter appearance
    public Material DefaultMaterial;
    public Material GlowMaterial;
    public List<GameObject> Alphabet; //List of letters GameObjects present on machine


    // Start is called before the first frame update
    void Start()
    {
        ResetMaterials();
    }

    //Set all letters to not glow
    void ResetMaterials()
    {
        foreach (GameObject letter in Alphabet)
        {
            letter.GetComponent<Renderer>().material = DefaultMaterial;
        }
    }


    //Called from the Game_Manager script, passes in encrypted letter.
    //This function resets all other letters to not glow and then looks for the letter passed in
    //within the Alphabet GameObject list in this script, and when found it changes material to glowing one
    public void GlowLetter(char letter)
    {
        ResetMaterials();

        foreach (GameObject item in Alphabet)
        {
            char foundLetter = item.GetComponent<LetterInfo>().letter;
            if (foundLetter == letter)
            {
                item.GetComponent<Renderer>().material = GlowMaterial;
            }
        }
    }
}