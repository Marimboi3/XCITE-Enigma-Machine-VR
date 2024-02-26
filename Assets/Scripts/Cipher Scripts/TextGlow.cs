using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TextGlow : MonoBehaviour
{
    //Variables
    public List<GameObject> OuterAlphabetMain; //List of letters GameObjects present on main knob
    public List<GameObject> InnerAlphabetMain;

    public List<GameObject> OuterAlphabetSide; //List of letters GameObjects present on side knob
    public List<GameObject> InnerAlphabetSide;
    //Material objects to change letter appearance
    public Material TextMaterial;
    public Material GlowMaterial;
    public Camera BloomCamera; //Camera object to access bloom effect settings
    private float timer = 3.0f; //period of glow effect 
    private bool outerGlow = true; //flag to alternate between glowing inner and outer text

    //flags to check whether a letter has been clicked on in respective knobs
    public bool outerTextSelectedMain = false;
    public bool innerTextSelectedMain = false;

    public bool outerTextSelectedSide = false;
    public bool innerTextSelectedSide = false;

    //variable to knocw when switch was turned on to start glowing side knob
    public bool glowSwitch = false;
    //To access the value of the switch
    public Cipher_Mechanism cipherMechanism;
    //Bloom object to hold reference to bloom effect being used on the camera
    private Bloom bloomEffect;

    void Start()
    {
        //turn everything off
        ResetMaterials(OuterAlphabetMain);
        ResetMaterials(InnerAlphabetMain);
        ResetMaterials(OuterAlphabetSide);
        ResetMaterials(InnerAlphabetSide);

        //Get bloom effect from post process volume
        var postProcessVol = BloomCamera.GetComponent<PostProcessVolume>();
        if (postProcessVol != null)
        {
            bloomEffect = postProcessVol.profile.GetSetting<Bloom>();
        }
    }

    void Update()
    {
        //check if switch has been flipped 
        glowSwitch = cipherMechanism.GetRightSwitch();

        //update timer with passing time
        timer += Time.deltaTime;

        //if 3 seconds have elapsed, toggle which knob is glowing and reset timer
        //timer initialized to 3 so this will be called right away, making outer text glow as soon as it starts
        if (timer > 3.0f)
        {
            ToggleGlow();
            timer = 0.0f;
        }
    }

    /*
      Void function to toggle the glow effect between the outer and the inner text of the knobs. 
      This function is called every 3 seconds, and it alternates the glowing by checking the outerGlow flag, which is flipped 
      each time function is called.  
   */
    void ToggleGlow()
    {

        /*
        If the outerGlow flag is set, it means the outer knobs should glow. It checks that no letter on the outer text has been clicked (through the outerTextSelectedMain flag), and then it checks
        if  an inner letter has been selected, if not thet it turns off the inner alphabet, and then 
        it calls the GlowLetterList function to change the material of all the letters to the glowing material,
        then the Bloom Effect coroutine is called to gradually increase and decrease the glow intensity, to create a pulsating effect.

        If the side switch has been flipped on, then repeat the same process for the side knobs
        */
        if (outerGlow)
        {
            if (!outerTextSelectedMain)
            {
                if(!innerTextSelectedMain)
                {
                    ResetMaterials(InnerAlphabetMain);
                }
                GlowLetterList(OuterAlphabetMain);
            }

            if (glowSwitch)
            {
                if (!outerTextSelectedSide)
                {
                    if (!innerTextSelectedSide)
                    {
                        ResetMaterials(InnerAlphabetSide);
                    }
                    GlowLetterList(OuterAlphabetSide);
                }
            }
            StartBloomEffect();

        }
        /*
        If the outerGlow flag is not set, then it means the inner knob should glow. The exact same behaviour as above repeats, 
        but in this case, we switch the setting of the glow material so that the inner knob can glow if no letters have been clicked yet.
        */
        else
        {
            if (!innerTextSelectedMain)
            {
                if (!outerTextSelectedMain)
                {
                    ResetMaterials(OuterAlphabetMain);
                }
                
                GlowLetterList(InnerAlphabetMain);
            }

            if (glowSwitch)
            {
                if (!innerTextSelectedSide)
                {
                    if (!outerTextSelectedSide)
                    {
                        ResetMaterials(OuterAlphabetSide);
                    }
                    GlowLetterList(InnerAlphabetSide);
                }
            }
            StartBloomEffect();
        }
        outerGlow = !outerGlow;
    }

    //Function to reset the material of all letters to the regular text material
    void ResetMaterials(List<GameObject> Alphabet)
    {
        foreach (GameObject letter in Alphabet)
        {
            letter.GetComponent<Renderer>().material = TextMaterial;
        }
    }

    //Function that is accessed by MainKnobRotation and SideKnobRotation scripts to make a single letter glow when clicked
    public void GlowLetter(GameObject letter, bool outerText, bool main)
    {
        if (outerText && main)
        {
            ResetMaterials(OuterAlphabetMain);
        }
        else if (outerText && !main)
        {
            ResetMaterials(OuterAlphabetSide);
        }
        else if (!outerText && main)
        {
            ResetMaterials(InnerAlphabetMain);
        }
        else if (!outerText && !main)
        {
            ResetMaterials(InnerAlphabetSide);
        }
        letter.GetComponent<Renderer>().material = GlowMaterial;
    }


    //Function to call coroutine that animates the glowing to make it appear like it is pulsating
    public void StartBloomEffect()
    {
        if (bloomEffect != null)
        {
            //stop any active coroutines to avoid multiple at same time
            StopAllCoroutines();
            //start coroutine that will alternate intensity from 0 to 2 over a 3 second time frame
            StartCoroutine(AnimateBloomIntensity(0.0f, 2.0f, 3.0f));
        }
    }

    //Function to change the material of a list to the glow material
    public void GlowLetterList(List<GameObject> Alphabet)
    {
        foreach (GameObject letter in Alphabet)
        {
            Renderer letterRenderer = letter.GetComponent<Renderer>();
            letterRenderer.material = GlowMaterial;
        }
    }

    //Coroutine that changes intensity value of bloom effect dynamically to simulate a pulsating glow animation
    private IEnumerator AnimateBloomIntensity(float startIntensity, float endIntensity, float duration)
    {
        bloomEffect.intensity.value = startIntensity;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            float curvedT = Mathf.Sin(t * Mathf.PI); // Sin curve for smooth easing in and out
            bloomEffect.intensity.value = Mathf.Lerp(startIntensity, endIntensity, curvedT);
            currentTime += Time.deltaTime;
            yield return null; // Wait until next frame
        }
    }

}
