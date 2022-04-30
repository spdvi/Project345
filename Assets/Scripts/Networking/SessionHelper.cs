using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SessionHelper
{

    public static IEnumerator StartSession(string token)
    {
        UnityWebRequest www = UnityWebRequest.Get(Constants.Server + "api/Usuaris/startsession");
        //www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("Authorization", "bearer " + token);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(www.error);
        }

    }

    public static IEnumerator EndSession(string token)
    {
        UnityWebRequest www = UnityWebRequest.Get(Constants.Server + "api/Usuaris/endsession");
        //www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("Authorization", "bearer " + token);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(www.error);
        }

    }

}
