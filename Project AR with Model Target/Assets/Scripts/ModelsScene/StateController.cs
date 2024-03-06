using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class StateController : MonoBehaviour
{
    public TMP_Text _textMeshProState;

    public int _internalId;

    bool localState = false;

    string urlgetState = "http://localhost:8000/getState?id=";
    string urlsetState = "http://localhost:8000/changeState?id=";

    ResponseState state = null;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(getState_couroutine());
    }

    void aggiornaState(string state)
    {
        StartCoroutine(setState_couroutine(state));
        StartCoroutine(getState_couroutine());


    }

    IEnumerator setState_couroutine(string stateToSet)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlsetState + _internalId+ "?state=" + stateToSet))
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
            }
        }
    }




        IEnumerator getState_couroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlgetState + _internalId))
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
            }
            else
            {
                string json = request.downloadHandler.text;
                state = JsonConvert.DeserializeObject<ResponseState>(json);

            }
        }

        if (state != null)
        {

            _textMeshProState.text = "State: "  + state.state;
           

        }
        else
        {
            _textMeshProState.text = "State non Found";
        }
    }
}
