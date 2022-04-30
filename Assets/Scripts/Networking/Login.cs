using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Login : MonoBehaviour
{
    private string server = "https://localhost:7215/";
    private Usuari user;

    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button loginButton;
    public LevelMenuManager levelManager;

    private void Start()
    {
        Debug.Log("Login panel game object activated");

        emailInputField.text = "mgarcia@fmail.com";  // Throws a null ref excp when LoginPanel not active in scene (when game restarts and a user is logged in).
        passwordInputField.text = "Pwd1234.";
        loginButton.onClick.AddListener(() => OnLoginButtonClicked());
        //playGameButton.onClick.AddListener(OnLoadProfileSceneButtonClicked);

        //if (PlayerPrefs.HasKey("playerName") && PlayerPrefs.HasKey("token"))
        //{
        //    Debug.Log(PlayerPrefs.GetString("playerName"));
        //    //OnLoadProfileSceneButtonClicked();
        //    loginButton.interactable = false;
        //}
    }

    public void OnLoadProfileSceneButtonClicked()
    {
        SceneManager.LoadScene("Scenes/UNetTetsProfileTools");
    }

    // Start is called before the first frame update
    public void OnLoginButtonClicked()
    {
        StartCoroutine(TryLogin());
    }

    private IEnumerator TryLogin()
    {
        if (user == null)
        {
            UnityWebRequest httpClient = new UnityWebRequest(Constants.Server + "api/Auth/login", "POST");

            UsuariDto newUser = new UsuariDto();
            newUser.Email = emailInputField.text;
            newUser.Password = passwordInputField.text;

            string jsonData = JsonUtility.ToJson(newUser);
            byte[] dataToSend = Encoding.UTF8.GetBytes(jsonData);

            httpClient.uploadHandler = new UploadHandlerRaw(dataToSend);
            httpClient.downloadHandler = new DownloadHandlerBuffer();

            httpClient.SetRequestHeader("Content-Type", "application/json");
            httpClient.SetRequestHeader("Accept", "application/json");

            yield return httpClient.SendWebRequest();

            if (httpClient.result == UnityWebRequest.Result.ConnectionError || httpClient.result == UnityWebRequest.Result.ProtocolError)
            {
                throw new Exception("Login: " + httpClient.error);
            }
            else
            {
                string jsonResponse = httpClient.downloadHandler.text;
                user = JsonConvert.DeserializeObject<Usuari>(jsonResponse);
            }
            httpClient.Dispose();
        }

        PlayerPrefs.SetString("token", user.token);
        PlayerPrefs.SetString("playerName", user.nom);
        PlayerPrefs.SetInt("playerId", user.id);
        PlayerPrefs.Save();

        levelManager.UserLoggedIn(user.nom);
    }
}
