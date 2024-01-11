using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_Text : MonoBehaviour
{
    public Text originalText;
    public Text encryptionText;

    private string original = "Hello";
    private string encryption = "World";
    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        originalText.text = original;
        encryptionText.text = encryption;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            originalText.text = original;

            encryptionText.text = encryption;
            flag = false;
        }
    }

    public void recieveOriginal(string text)
    {
        original = text;
    }

    public void recieveEncryption(string text)
    {
        encryption = text;
        flag = true;
    }
}
