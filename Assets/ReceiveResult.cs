using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveResult : MonoBehaviour
{

    float scale = 0.01f;

    public GameObject monumentContainer;

    // Use this for initialization
    void Start()
    {
        //GameObject.Find("Text").GetComponent<Text>().text = "You need to be connected to Internet";
    }

    void onActivityResult(string recognizedText)
    {
        char[] delimiterChars = { '~' };
        string[] result = recognizedText.Split(delimiterChars);

        //You can get the number of results with result.Length
        //And access a particular result with result[i] where i is an int
        //I have just assigned the best result to UI text
        GameObject.Find("Text").GetComponent<Text>().text = result[0];

        if (result[0].Equals("scale up"))
        {
            monumentContainer.transform.localScale += new Vector3(scale, scale, scale);
        }
        else if (result[0].Equals("scale down"))
        {
            monumentContainer.transform.localScale += new Vector3(-scale, -scale, -scale);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
