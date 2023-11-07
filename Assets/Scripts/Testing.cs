using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject plug;
    public GameObject mechanism;

    // Start is called before the first frame update
    void Start()
    {
        mechanism = GameObject.Find("Enigma Mechanism");

        int rotorNum;

        //this types hello
        List<int> inputs = new List<int> { 72, 69, 76, 76, 79 };

        for (int i = 0; i < inputs.Count; i++)
        {
            rotorNum = mechanism.GetComponent<Mechanism>().rotor(inputs[i]);
            Debug.Log("typing input{" + i + "}: " + inputs[i]);
            Debug.Log("this returns: " + rotorNum);
            
        }

        //User chooses to plug in S to O on the plug board

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
