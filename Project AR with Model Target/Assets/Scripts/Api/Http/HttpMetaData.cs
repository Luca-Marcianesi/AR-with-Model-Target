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

    string urlsetState = "http://localhost:8000/machinery?id=";

    List<ResponseMachinery> list = null;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getMetaData());
    }

    IEnumerator getMetaData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlsetState + internalId ))
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
            }
            else
            {
                string json = request.downloadHandler.text;
                list = JsonConvert.DeserializeObject<List<ResponseMachinery>>(json);

            }
            if (list != null)
            {
                ResponseMachinery machinery = list[0];

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
