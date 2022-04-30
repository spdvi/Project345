using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Text;
using System;
using Newtonsoft.Json;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    public Button registerButton;
    [SerializeField] private LevelMenuManager levelManager;

    private void Start()
    {
        //emailInputField.text = "ggarcia@fmail.com";
        //passwordInputField.text = "Pwd1234.";
        registerButton.onClick.AddListener(() => OnRegisterButtonClick());
    }

    public void OnRegisterButtonClick()
    {
        StartCoroutine(RegisterNewUser());
    }

    private IEnumerator RegisterNewUser()
    {
        Usuari usuari = new Usuari();

        UnityWebRequest httpClient = new UnityWebRequest(Constants.Server + "api/Auth/register", "POST");

        UsuariDto newUser = new UsuariDto();
        newUser.Email = emailInputField.text;
        newUser.Password = passwordInputField.text;

        //string jsonData = JsonUtility.ToJson(newUser);
        string jsonData = JsonConvert.SerializeObject(newUser);
        byte[] dataToSend = Encoding.UTF8.GetBytes(jsonData);
        httpClient.uploadHandler = new UploadHandlerRaw(dataToSend);
        httpClient.downloadHandler = new DownloadHandlerBuffer();

        httpClient.SetRequestHeader("Content-Type", "application/json");

        yield return httpClient.SendWebRequest();

        if (httpClient.result == UnityWebRequest.Result.ConnectionError || httpClient.result == UnityWebRequest.Result.ProtocolError)
        {
            throw new Exception("OnRegisterButtonClick: Error > " + httpClient.error);
        }
        else
        {
            string jsonResponse = httpClient.downloadHandler.text;
            usuari = JsonConvert.DeserializeObject<Usuari>(jsonResponse);
        }

        httpClient.Dispose();

        // TODO: Show Ok message, wait 1 sec and go to login panel
        levelManager.UserRegistered(emailInputField.text);
    }
}
