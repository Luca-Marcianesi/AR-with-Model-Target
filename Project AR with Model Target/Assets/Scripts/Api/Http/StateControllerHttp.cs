using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;


public class StateController : MonoBehaviour
{
    //ui element
    public TMP_Text textStateModel;

    //internal id of the current model selected
    public int internalId;

    //local variable the control the ui elements
    bool _localState = false;


    string _urlsetState = "http://193.205.129.120:63395/changeState?id=";
    //string _urlsetState = "http://127.0.0.1:8000/changeState?id=";


    private void Start()
    {
        ChangeText();
    }

    //get the click on an object
    private void OnMouseDown()
    {
        ToggleState();
    }

    //change the text on the ui element
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

    //controll all the logic 
    private void ToggleState()
    { 
        {
            _localState = !_localState;

            ChangeText();
     
            StartCoroutine(setState_couroutine(_localState));
        }

    }

    // start the http connection with the api and send the value in stateToSet
    IEnumerator setState_couroutine(bool stateToSet = false)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_urlsetState + internalId+ "&state=" + stateToSet))
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                textStateModel.text = "errore";
            }
            
            
        }
    }

}
