using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static System.Net.WebRequestMethods;

public class OptionsScript : MonoBehaviour
{
    string _urlListRoom = "http://193.205.129.120:63395/listroom";
    //string _urlListRoom = "http://127.0.0.1:8000/listroom";

    
    string _urlListMachinery = "http://193.205.129.120:63395/machineryByRoom?id=";
    //string _urlListMachinery = "http://127.0.0.1:8000/machineryByRoom?id=";


    //ui elements
    public TMP_Dropdown dropdownRooms;
    public TMP_Dropdown dropdownMachinery;

    //list to memorize the api response
    List<ResponseListRoom> _listRooms = null;
    List<ResponseMachinery> _listMachinery = null;



    void Awake()
    {
        StartCoroutine(getRooms_Coroutine());
    }


    IEnumerator getRooms_Coroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_urlListRoom))
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
                _listRooms = JsonConvert.DeserializeObject<List<ResponseListRoom>>(json);

            }
        }

        //set the rooms on the dropdown menu
        if (_listRooms != null)
        {
            
            dropdownRooms.options.Clear();
            dropdownRooms.options.Add(new TMP_Dropdown.OptionData("Select a Room"));
            foreach (ResponseListRoom room in _listRooms)
            {
                dropdownRooms.options.Add(new TMP_Dropdown.OptionData(room.roomName));
            }
            dropdownRooms.value = 0;
            //add listener to know when the user make a choice
            dropdownRooms.onValueChanged.AddListener(ChangeMachinery);

        }
    }

    private void ChangeMachinery(int arg0)
    {
        //search for the correct room and call the method to get the relative machinery
        foreach (var room in _listRooms)
        {
            if(room.roomName == dropdownRooms.options[arg0].text) StartCoroutine(getMachinery_Coroutine(room.roomId));
            
        }
        
    }


    //method to get the machinery
    IEnumerator getMachinery_Coroutine(string room)
    {
        string urlToCall = _urlListMachinery + room;
        using (UnityWebRequest request = UnityWebRequest.Get(urlToCall))
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("errore");
            }
            else
            {
                string json = request.downloadHandler.text;
                //use jsonconverter because the result is not an object but a list
                _listMachinery = JsonConvert.DeserializeObject<List<ResponseMachinery>>(json);
            }
        }

        //set the machenery on the dropdown menu
        if (_listMachinery != null)
        {
            dropdownMachinery.options.Clear();
            dropdownMachinery.options.Add(new TMP_Dropdown.OptionData("Select Machinery"));
            foreach (ResponseMachinery machinery in _listMachinery)
            {
                dropdownMachinery.options.Add(new TMP_Dropdown.OptionData(machinery.name));
            }
            dropdownMachinery.value = 0;
            //add listener to know when the user make a choice
            dropdownMachinery.onValueChanged.AddListener(goToMachineryScene);

        }
    }


    //method that controll the chande scene on the machinery dropdown
    private void goToMachineryScene(int arg0)
    {
       int _internalId = _listMachinery[arg0 - 1].internal_id;


        switch (_internalId)
        {
            case 1:
                SceneManager.LoadScene("GarbasperoniScene");

                    break;
            case 2:
               
                SceneManager.LoadScene("ApplicapuntaliScene"); 
                break;
            case 3:
                
                SceneManager.LoadScene("GarbapunteScene");
                break;
                    

            default: Debug.Log(_internalId);
                break;
        }
    }
}
