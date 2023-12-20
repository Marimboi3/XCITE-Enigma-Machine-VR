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
        int letterNum;

        //this types hello
        List<int> inputs = new List<int> {65, 65, 90, 90};

        for (int i = 0; i < inputs.Count; i++)
        {
            Debug.Log("typing input{" + i + "}: " + (char)inputs[i]);
            letterNum = mechanism.GetComponent<Mechanism>().letterInput(inputs[i]);
            Debug.Log("this returns: " + (char)letterNum);
        }

        //User chooses to plug in S to O on the plug board

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
