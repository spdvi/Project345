using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiHelper : MonoBehaviour
{
    public static IEnumerator StartIntent(int idNivell, IntentResultsDto intent)
    {
        string token;

        if (!PlayerPrefs.HasKey("token"))
        {
            throw new Exception("No token found.");
        }
        else
        {
            token = PlayerPrefs.GetString("token");
            UnityWebRequest httpClient = new UnityWebRequest(Constants.Server + "api/Intent/CreateIntent", "POST");
            //www.SetRequestHeader("Content-Type", "application/json");
            httpClient.SetRequestHeader("Accept", "application/json");
            httpClient.SetRequestHeader("Authorization", "bearer " + token);

            string jsonData = idNivell.ToString();
            byte[] dataToSend = Encoding.UTF8.GetBytes(jsonData);
            httpClient.uploadHandler = new UploadHandlerRaw(dataToSend);
            httpClient.downloadHandler = new DownloadHandlerBuffer();

            httpClient.SetRequestHeader("Content-Type", "application/json");

            yield return httpClient.SendWebRequest();

            if (httpClient.result != UnityWebRequest.Result.Success)
            {
                throw new Exception(httpClient.error);
            }
            else
            {
                string jsonResponse = httpClient.downloadHandler.text;
                intent.IdIntent = JsonConvert.DeserializeObject<int>(jsonResponse);
            }

            httpClient.Dispose();
        }

    }

    internal static IEnumerator UpdateIntent(IntentResultsDto intent)
    {
        string token;

        if (!PlayerPrefs.HasKey("token"))
        {
            throw new Exception("No token found.");
        }
        else
        {
            token = PlayerPrefs.GetString("token");
            UnityWebRequest httpClient = new UnityWebRequest(Constants.Server + "api/Intent/UpdateIntent", "PUT");
            //www.SetRequestHeader("Content-Type", "application/json");
            httpClient.SetRequestHeader("Accept", "application/json");
            httpClient.SetRequestHeader("Authorization", "bearer " + token);

            string jsonData = JsonConvert.SerializeObject(intent);
            byte[] dataToSend = Encoding.UTF8.GetBytes(jsonData);
            httpClient.uploadHandler = new UploadHandlerRaw(dataToSend);
            httpClient.downloadHandler = new DownloadHandlerBuffer();

            httpClient.SetRequestHeader("Content-Type", "application/json");

            yield return httpClient.SendWebRequest();

            if (httpClient.result != UnityWebRequest.Result.Success)
            {
                throw new Exception(httpClient.error);
            }
            else
            {
                string jsonResponse = httpClient.downloadHandler.text;
                //intent.IdIntent = JsonConvert.DeserializeObject<int>(jsonResponse);
            }

            httpClient.Dispose();
        }
    }
}
