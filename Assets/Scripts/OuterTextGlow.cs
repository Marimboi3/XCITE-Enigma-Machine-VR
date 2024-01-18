using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterTextGlow : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Alphabet;
    public Material TextMaterial;
    public Material GlowMaterial;
    private float timer = 0.0f;
    private float waitTime = 2.0f;
    private bool glow = true;
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
                foreach (GameObject letter in Alphabet)
                {
                    letter.GetComponent<Renderer>().material = GlowMaterial;
                }
                glow = false;
            }
            else
            {
                foreach (GameObject letter in Alphabet)
                {
                    letter.GetComponent<Renderer>().material = TextMaterial;
                    glow = true;
                }
            }
            timer = timer - waitTime;
        }
    }
}
