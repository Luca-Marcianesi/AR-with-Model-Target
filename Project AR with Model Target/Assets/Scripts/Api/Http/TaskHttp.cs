using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TakHttp : MonoBehaviour
{
    public TMP_Text textTask;

    //internal id of the current model selected
    public int internalId;

    //string _urlsetState = "http://localhost:8000/getTaskById?id=";
    string _urlsetState = "http://193.205.129.120:63395/getTaskById?id=";

    //list to memorize the result of the api call
    List<ResponseTask> _list = null;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getMetaData());
    }


    //method that start the api connection and strore the result
    IEnumerator getMetaData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_urlsetState + internalId))
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
                _list = JsonConvert.DeserializeObject<List<ResponseTask>>(json);

            }
            if (_list != null)
            {
                //insert all the task content in the ui element
                foreach (ResponseTask task in _list)
                {
                    if(textTask != null) textTask.text += task.content + "\n\n";
                    
                }

            }
            else
            {
                if (textTask != null)  textTask.text = "Task not found";
            }
        }
    }
}
