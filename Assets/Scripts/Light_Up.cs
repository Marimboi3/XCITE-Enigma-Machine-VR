using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Up : MonoBehaviour
{
    //Material objects to change letter appearance
    public Material DefaultMaterial;
    public Material GlowMaterial;
    public List<GameObject> Alphabet; //List of letters GameObjects present on machine
    private float timer = 0.0f; //period of glow effect 
    private bool glow = true;


    // Start is called before the first frame update
    void Start()
    {
        ResetMaterials();
    }

    // Update is called once per frame
    void Update()
    {
        //update timer with passing time
        timer += Time.deltaTime;

        //if 2 seconds have elapsed, toggle which knob is glowing and reset timer
        //timer initialized to 2 so this will be called right away, making outer text glow as soon as it starts
        if (timer > 2.0f)
        {
            if (glow)
            {
                GlowMaterials();
                timer = 0.0f;
                glow = !glow;
            }
            else
            {
                ResetMaterials();
                timer = 0.0f;
                glow = !glow;
            }
        }
    }


    void ResetMaterials()
    {
        foreach (GameObject letter in Alphabet)
        {
            letter.GetComponent<Renderer>().material = DefaultMaterial;
        }
    }

    void GlowMaterials()
    {
        foreach (GameObject letter in Alphabet)
        {
            letter.GetComponent<Renderer>().material = GlowMaterial;
        }
    }
}