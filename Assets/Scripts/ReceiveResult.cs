using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveResult : MonoBehaviour
{
    public delegate void VoiceEvent(string eventType);
    public static event VoiceEvent OnVoiceEvent;


    public const string EVENT_FULL_CASTLE = "EVENT_FULL_CASTLE";
    public const string EVENT_PARTIAL_CASTLE = "EVENT_PARTIAL_CASTLE";
    public const string EVENT_DESTROYED_CASTLE = "EVENT_DESTROYED_CASTLE";
    public const string EVENT_FULL_WIREFRAME = "EVENT_FULL_WIREFRAME";
    public const string EVENT_PARTIAL_WIREFRAME = "EVENT_PARTIAL_WIREFRAME";
    public const string EVENT_DESTROYED_WIREFRAME = "EVENT_DESTROYED_WIREFRAME";
    public const string EVENT_WIREFRAME = "EVENT_WIREFRAME";
    public const string EVENT_CASTLE = "EVENT_CASTLE";

    public const string CMD_FULL_CASTLE = "castle in 1812";
    public const string CMD_PARTIAL_CASTLE = "castle in 1700";
    public const string CMD_DESTROYED_CASTLE = "castle in 1200";
    public const string CMD_FULL_WIREFRAME = "wireframe in 1812";
    public const string CMD_PARTIAL_WIREFRAME = "wireframe in 1700";
    public const string CMD_DESTROYED_WIREFRAME = "wireframe in 1200";
    public const string CMD_WIREFRAME = "wireframe";
    public const string CMD_CASTLE = "castle";


    public const string CMD_SCALE_UP = "scale up";
    public const string CMD_SCALE_DOWN = "scale down";


    // Key = the voice command
    // Value = the event
    private Dictionary<string, string> phraseResultEvents = new Dictionary<string, string>();

    private void Start()
    {
        phraseResultEvents.Add(CMD_FULL_CASTLE, EVENT_FULL_CASTLE);
        phraseResultEvents.Add(CMD_PARTIAL_CASTLE, EVENT_PARTIAL_CASTLE);
        phraseResultEvents.Add(CMD_DESTROYED_CASTLE, EVENT_DESTROYED_CASTLE);
        phraseResultEvents.Add(CMD_FULL_WIREFRAME, EVENT_FULL_WIREFRAME);
        phraseResultEvents.Add(CMD_PARTIAL_WIREFRAME, EVENT_PARTIAL_WIREFRAME);
        phraseResultEvents.Add(CMD_DESTROYED_WIREFRAME, EVENT_DESTROYED_WIREFRAME);

        phraseResultEvents.Add(CMD_CASTLE, EVENT_CASTLE);
        phraseResultEvents.Add(CMD_WIREFRAME, EVENT_WIREFRAME);
    }


    float scale = 0.01f;
    public GameObject monumentContainer;


    void onActivityResult(string recognizedText)
    {
        Debug.LogError("recognizedText:  " + recognizedText);

        char[] delimiterChars = { '~' };
        string[] result = recognizedText.Split(delimiterChars);

        string capturedPhrase = result[0].ToLower();

        //You can get the number of results with result.Length
        //And access a particular result with result[i] where i is an int
        //I have just assigned the best result to UI text
//        GameObject.Find("SpeechRecog").GetComponent<Text>().text = capturedPhrase;

        if (OnVoiceEvent != null)
        {
            string eventString = phraseResultEvents[capturedPhrase];

            if (eventString != null && !eventString.Equals(""))
            {
                OnVoiceEvent(eventString);
                return;
            }
        }

        switch (capturedPhrase)
        {
            case CMD_SCALE_UP:
                monumentContainer.transform.localScale += new Vector3(scale, scale, scale);
                break;
            case CMD_SCALE_DOWN:
                monumentContainer.transform.localScale += new Vector3(-scale, -scale, -scale);
                break;
        }
    }

}
