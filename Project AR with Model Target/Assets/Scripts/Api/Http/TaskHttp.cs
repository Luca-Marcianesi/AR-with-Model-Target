using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TakHttp : MonoBehaviour
{
    public TMP_Text textTask;


    public int internalId;

    string urlsetState = "http://localhost:8000/getTaskById?id=";
    // string urlsetState = "http://193.205.129.120:63395/getTaskById?id=";

    List<ResponseTask> list = null;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getMetaData());
    }

    IEnumerator getMetaData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlsetState + internalId))
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
            }
            else
            {
                string json = request.downloadHandler.text;
                list = JsonConvert.DeserializeObject<List<ResponseTask>>(json);

            }
            if (list != null)
            {
                foreach (ResponseTask task in list)
                {
                    if(textTask != null) textTask.text += task.content + "\n";
                    
                }

            }
            else
            {
                if (textTask != null)  textTask.text = "Task not found";
            }
        }
    }
}
