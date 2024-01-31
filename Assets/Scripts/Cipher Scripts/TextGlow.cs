using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TextGlow : MonoBehaviour
{   
    //Variables
    public List<GameObject> Alphabet; //List of letters GameObjects present on knobs
    //Material objects to change letter appearance
    public Material TextMaterial;
    public Material GlowMaterial;
    public Camera BloomCamera; //Camera object to access bloom effect settings
    public bool OuterText; //flag to check which Text this script is being called on
    private float timer = 2.0f; //period of glow effect 
    private bool outerGlow = true; //flag to alternate between glowing and regular text
    //flags to check whether a letter has been clicked on and should glow
    public bool outerTextSelected = false;
    public bool innerTextSelected = false;
    //Bloom object to hold reference to bloom effect being used on the camera
    private Bloom bloomEffect;

    void Start()
    {
        ResetMaterials();

        var postProcessVol = BloomCamera.GetComponent<PostProcessVolume>();
        if (postProcessVol != null)
        {
            bloomEffect = postProcessVol.profile.GetSetting<Bloom>();
        }
    }

    void Update()
    {
        //update timer with passing time
        timer += Time.deltaTime;

        //if 2 seconds have elapsed, toggle which knob is glowing and reset timer
        //timer initialized to 2 so this will be called right away, making outer text glow as soon as it starts
        if (timer > 2.0f)
        {
            ToggleGlow();
            timer = 0.0f;
        }
    }

    /*
    Void function to toggle the glow effect between the outer and the inner text of the knobs. 
    This function is called every 2 seconds, and it alternates the glowing by checking the outerGlow flag, which is flipped 
    each time function is called.  
     */
    void ToggleGlow()
    {
        foreach (GameObject letter in Alphabet)
        {
            Renderer letterRenderer = letter.GetComponent<Renderer>();

            /*
            If the outerGlow flag is set, it means the outer knob should glow. It checks the OuterText flag (which is only ever set to true for the outer knob),
            and if true, then it also checks that no letter on the outer text has been clicked (through the outerTextSelected flag), and only then 
            will it change the material of all the letters to the glowing material and launch the Bloom Effect coroutine to gradually increase
            and decrease the glow intensity, to create a pulsating effect. If the OuterText flag is not set, then this is being called by the inner text, and in that
            case just set material of all letters to the regular text material, as long as no letters have been clicked yet.
            */
            if (outerGlow)
            {
                if (OuterText)
                {
                    if (!outerTextSelected)
                    {
                        letterRenderer.material = GlowMaterial;
                        StartBloomEffect();
                    }             
                }
                else
                {
                    if (!innerTextSelected)
                    {
                        letterRenderer.material = TextMaterial;
                    }
                }
            }
            /*
            If the outerGlow flag is not set, then it means the inner knob should glow. The exact same behaviour as above repeats, 
            but in this case, we switch the setting of the glow material and the call of the Bloom Effect coroutine so that the inner knob
            (the one with the OuterText flag NOT set) can glow if no letters have been clicked yet.
            */
            else
            {
                if (OuterText)
                {
                    if (!outerTextSelected)
                    {
                        letterRenderer.material = TextMaterial;
                    }
                }
                else
                {
                    if (!innerTextSelected)
                    {
                        letterRenderer.material = GlowMaterial;
                        StartBloomEffect();                        
                    }     
                }
            }
        }
        outerGlow = !outerGlow;
    }

    //Function to reset the material of all letters to the regular text material
    void ResetMaterials()
    {
        foreach (GameObject letter in Alphabet)
        {
            letter.GetComponent<Renderer>().material = TextMaterial;
        }
    }


    //Function that is accessed by MainKnobRotation script to make a single letter glow when clicked
    public void GlowLetter(GameObject letter)
    {
        ResetMaterials();
        letter.GetComponent<Renderer>().material = GlowMaterial;
    }

   
    //Function to call coroutine that animates the glowing to make it appear like it is pulsating
    public void StartBloomEffect()
    {
        if (bloomEffect != null)
        {
            //stop any active coroutines to avoid multiple at same time
            StopAllCoroutines();
            //start coroutine that will alternate intensity from 0 to 2 over a 2 second time frame
            StartCoroutine(AnimateBloomIntensity(0.0f, 2.0f, 2.0f));
        }
    }

    //Coroutine that changes intensity value of bloom effect dynamically to simulate a pulsating glow animation
    private IEnumerator AnimateBloomIntensity(float startIntensity, float endIntensity, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            float curvedT = Mathf.Sin(t * Mathf.PI); // Sin curve for smooth easing in and out
            bloomEffect.intensity.value = Mathf.Lerp(startIntensity, endIntensity, curvedT);
            currentTime += Time.deltaTime;
            yield return null; // Wait until next frame
        }
        bloomEffect.intensity.value = startIntensity; // Reset to start intensity
    }

}
