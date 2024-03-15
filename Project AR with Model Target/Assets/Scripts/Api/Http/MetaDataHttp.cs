using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class HttpMetaData : MonoBehaviour
{
    public TMP_Text textTitle;
    public TMP_Text textInfo;
    public TMP_Text textMachineInfo;

    public int internalId;

    string _urlsetState = "http://193.205.129.120:63395/machinery?id=";
    //string _urlsetState = "http://127.0.0.1:8000/machinery?id=";


    //list to memorize the result of the api call
    List<ResponseMachinery> _list = null;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getMetaData());
    }

    //method that get the metadata from an api call and put on the ui element
    IEnumerator getMetaData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_urlsetState + internalId ))
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
            }
            else
            {
                string json = request.downloadHandler.text;
                //use jsonconverter because the result is not an object but a list
                _list = JsonConvert.DeserializeObject<List<ResponseMachinery>>(json);

            }

            //change the ui elements
            if (_list != null)
            {
                //the result is always a list with only one machinery
                ResponseMachinery machinery = _list[0];

                textTitle.text = machinery.name;
                textMachineInfo.text = "Brand: " + machinery.brand + "\n" +
                    "Serial Number: " + machinery.serial_number + "\n" + "Machine Type: " + machinery.machine_type + "\n" +
                    "Deploy: " + machinery.deployed_at;

                textInfo.text = machinery.description;

            }
            else
            {
                textTitle.text = "Data not found";
            }
        }
    }


}
