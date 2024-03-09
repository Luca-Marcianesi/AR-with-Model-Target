using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.Analytics;

public class StateController : MonoBehaviour
{
    public TMP_Text textStateModel;

    public int internalId;

    bool _localState = false;

    string urlsetState = "http://localhost:8000/changeState?id=";

    private void Start()
    {
        ChangeText();
    }

    private void OnMouseDown()
    {
        ToggleState();
    }

    private void ChangeText()
    {
        if (_localState)
        {
            textStateModel.text = "ON";
        }
        else
        {
            textStateModel.text = "OFF";
        }
    }

    private void ToggleState()
    { 
        {
            _localState = !_localState;

            ChangeText();
     
            StartCoroutine(setState_couroutine(_localState));
        }

    }

    IEnumerator setState_couroutine(bool stateToSet = false)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlsetState + internalId+ "&state=" + stateToSet))
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                textStateModel.text = "errore";
            }
            
            
        }
    }

}
