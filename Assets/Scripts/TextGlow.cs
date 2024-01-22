using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGlow : MonoBehaviour
{
    public List<GameObject> Alphabet;
    public Material TextMaterial;
    public Material GlowMaterial;
    public bool OuterText;

    private float timer = 2.0f;
    private bool glow = true;

    public bool outerTextSelected = false;
    public bool innerTextSelected = false;

    void Start()
    {
        ResetMaterials();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2.0f)
        {
            ToggleGlow();
            timer = 0.0f;
        }
    }

    void ToggleGlow()
    {
        foreach (GameObject letter in Alphabet)
        {
            Renderer letterRenderer = letter.GetComponent<Renderer>();

            if (glow)
            {
                if (OuterText)
                {
                    letterRenderer.material = outerTextSelected ? TextMaterial : GlowMaterial;
                }
                else
                {
                    if (!innerTextSelected)
                    {
                        letterRenderer.material = TextMaterial;
                    }
                }
            }
            else
            {
                if (OuterText)
                {
                    letterRenderer.material = TextMaterial;
                }
                else
                {
                    letterRenderer.material = innerTextSelected ? TextMaterial : GlowMaterial;
                }
            }
        }
        glow = !glow;
    }

    void ResetMaterials()
    {
        foreach (GameObject letter in Alphabet)
        {
            letter.GetComponent<Renderer>().material = TextMaterial;
        }
    }

    public void GlowLetter(GameObject letter)
    {
        letter.GetComponent<Renderer>().material = GlowMaterial;
    }
}
