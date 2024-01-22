using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGlow : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Alphabet;
    public Material TextMaterial;
    public Material GlowMaterial;
    public bool OuterText;
    private float timer = 2.0f;
    private float waitTime = 2.0f;
    private bool glow = true;

    public bool outerTextSelected = false;
    public bool innerTextSelected = false;
    void Start()
    {
        foreach (GameObject letter in Alphabet)
        {
            letter.GetComponent<Renderer>().material = TextMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            if (glow)
            {
                if (OuterText)
                {
                    if (!outerTextSelected)
                    {
                        foreach (GameObject letter in Alphabet)
                        {
                            letter.GetComponent<Renderer>().material = GlowMaterial;
                        }
                    }                  
                }
                else
                {
                    foreach (GameObject letter in Alphabet)
                    {
                        letter.GetComponent<Renderer>().material = TextMaterial;
                    }
                }

                glow = false;
            }
            else
            {
                if (OuterText)
                {
                    foreach (GameObject letter in Alphabet)
                    {
                        letter.GetComponent<Renderer>().material = TextMaterial;
                    }
                }
                else
                {
                    if (!innerTextSelected)
                    {
                        foreach (GameObject letter in Alphabet)
                        {
                            letter.GetComponent<Renderer>().material = GlowMaterial;
                        }
                    }
                }
                glow = true;
            }
            timer = 0.0f;
        }
    }
}

