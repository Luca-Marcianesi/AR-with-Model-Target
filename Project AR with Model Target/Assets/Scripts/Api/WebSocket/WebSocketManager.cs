using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class WebSocketManager : MonoBehaviour
{
    WebSocket ws = new WebSocket("ws://193.205.129.120:63425");
    [SerializeField] private TMP_Text textToViewModel;
    [SerializeField] private TMP_Text textToViewUi;
    [SerializeField] private TMP_Text textButton;
    ResponseWebSocket newdata = null;

    // Start is called before the first frame update
    void Start()
    {
        
       


        ws.OnMessage += (sender, e) =>
        {
            
            newdata =  JsonUtility.FromJson<ResponseWebSocket>(e.Data);
        };

    }

    // Update is called once per frame
    void Update()
    {
        if (newdata != null)
        {
            if(textToViewModel != null)
            {
                textToViewModel.text = newdata.temperature + " °C";
            }
            textToViewUi.text = newdata.temperature + " °C";


        }
        
    }

    public void startConnection()
    {
        ws.Connect();

    }

    public void Close()
    {

        ws.Close();
    }

    public void ToggleConnectio()
    {

        if(ws.IsAlive) {
            textButton.text = "Get Live Temperature";
            ws.Close();
        }
        else { 
            ws.Connect();
            textButton.text = "Stop Connection";
        }
    }
}


