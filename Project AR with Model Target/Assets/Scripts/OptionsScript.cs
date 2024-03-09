using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static System.Net.WebRequestMethods;

public class OptionsScript : MonoBehaviour
{
    string urlListRoom = "http://127.0.0.1:8000/listroom";

    public TMP_Dropdown dropdownRooms;
    public TMP_Dropdown dropdownMachinery;

    List<ResponseListRoom> listRooms = null;
    List<ResponseMachinery> listMachinery = null;



    void Awake()
    {
        StartCoroutine(getRooms_Coroutine());
    }


    IEnumerator getRooms_Coroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlListRoom))
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
            }
            else
            {
                string json = request.downloadHandler.text;
                listRooms = JsonConvert.DeserializeObject<List<ResponseListRoom>>(json);

            }
        }

        if (listRooms != null)
        {
            dropdownRooms.options.Clear();
            dropdownRooms.options.Add(new TMP_Dropdown.OptionData("Select a Room"));
            foreach (ResponseListRoom room in listRooms)
            {
                dropdownRooms.options.Add(new TMP_Dropdown.OptionData(room.roomName));
            }
            dropdownRooms.value = 0;
            dropdownRooms.onValueChanged.AddListener(ChangeMachinery);

        }
    }

    private void ChangeMachinery(int arg0)
    {

        foreach (var room in listRooms)
        {
            if(room.roomName == dropdownRooms.options[arg0].text) StartCoroutine(getMachinery_Coroutine(room.roomId));
            
        }
        
    }



    IEnumerator getMachinery_Coroutine(string room)
    {
        string urlToCall = "http://127.0.0.1:8000/machineryByRoom?id=" + room;
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
                listMachinery = JsonConvert.DeserializeObject<List<ResponseMachinery>>(json);

            }
        }

        if (listMachinery != null)
        {
            dropdownMachinery.options.Clear();
            dropdownMachinery.options.Add(new TMP_Dropdown.OptionData("Select Machinery"));
            foreach (ResponseMachinery machinery in listMachinery)
            {
                dropdownMachinery.options.Add(new TMP_Dropdown.OptionData(machinery.name));
            }
            dropdownMachinery.value = 0;
            dropdownMachinery.onValueChanged.AddListener(goToMachineryScene);

        }
    }


    private void goToMachineryScene(int arg0)
    {
       int _internalId = listMachinery[arg0 - 1].internal_id;


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
