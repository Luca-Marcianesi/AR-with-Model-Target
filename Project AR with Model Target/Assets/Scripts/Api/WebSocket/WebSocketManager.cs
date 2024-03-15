using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class WebSocketManager : MonoBehaviour
{

    //create the object for the web socket connection
    WebSocket _ws = new WebSocket("ws://193.205.129.120:63425");

    //the Ui object that display the changes
    public TMP_Text textToViewModel;
    public TMP_Text textToViewUi;
    public TMP_Text textButton;

    //empy object for the response of the connection
    ResponseWebSocket _newdata = null;

    //varieble the save locally the state of the connection
    bool _isConnected = false;

    // Start is called before the first frame update
    void Start()
    {



        //attach a listener to the web socket
        _ws.OnMessage += (sender, e) =>
        {
            //on new message save the new data (after a json parser)
            _newdata =  JsonUtility.FromJson<ResponseWebSocket>(e.Data);
        };

    }

    // Update is called once per frame
    void Update()
    {
        //update the ui elements
        if (_newdata != null)
        {
            if(textToViewModel != null)
            {
                textToViewModel.text = _newdata.temperature + " °C";
            }
            textToViewUi.text = _newdata.temperature + " °C";


        }
        
    }

    public void startConnection()
    {
        _ws.Connect();

    }

    public void Close()
    {

        _ws.Close();
    }

    public void ToggleConnectio()
    {

        if(_isConnected) {
            textButton.text = "Get Live Temperature";
            _ws.Close();
            _isConnected = false;
        }
        else { 
            _ws.Connect();
            textButton.text = "Stop Connection";
            _isConnected = true;
        }
    }
}


