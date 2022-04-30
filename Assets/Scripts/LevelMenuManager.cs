using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMenuManager : MonoBehaviour
{
    public GameObject welcomePanel;
    public TextMeshProUGUI welcomeText;
    public GameObject loginPanel;
    public GameObject registerPanel;
    public Button playgameButton;
    public TMP_InputField txtEmailLogin;
    [SerializeField] private Button logoutButton;
    public Button gotoRegisterPanelButton;
    public Button gotoLoginPanelButton;


    // Start is called before the first frame update
    void Start()
    {
        gotoRegisterPanelButton.onClick.AddListener(OnGoToRegisterPanelButtonClicked);
        gotoLoginPanelButton.onClick.AddListener(OnGoToLoginPanelButtonClicked);

        logoutButton.onClick.AddListener(OnLogoutButtonClicked);

        if (PlayerPrefs.HasKey("playerName") && PlayerPrefs.HasKey("token"))
        {
            welcomePanel.SetActive(true);
            welcomeText.text = "Welcome back " + PlayerPrefs.GetString("playerName");
            loginPanel.SetActive(false);
            registerPanel.SetActive(false);
            playgameButton.interactable = true;

            // Talk to api to start new session
            StartCoroutine(SessionHelper.StartSession(PlayerPrefs.GetString("token")));
        }
        else
        {
            welcomePanel.SetActive(false);
            //welcomeText.text = "Welcome back " + PlayerPrefs.GetString("playerName");
            loginPanel.SetActive(true);
            registerPanel.SetActive(false);
            playgameButton.interactable = false;
        }
    }

    public void UserLoggedIn(string username)
    {
        welcomePanel.SetActive(true);
        welcomeText.text = "Welcome back " + username;
        loginPanel.SetActive(false);
        playgameButton.interactable = true;

        // Talk to api to start new session
        StartCoroutine(SessionHelper.StartSession(PlayerPrefs.GetString("token")));

    }

    public void UserRegistered(string email)
    {
        loginPanel.SetActive(true);
        txtEmailLogin.text = email;
        registerPanel.SetActive(false);
    }

    public void OnGoToRegisterPanelButtonClicked()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void OnGoToLoginPanelButtonClicked()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    public void OnLogoutButtonClicked()
    {
        //Call api to close session.
        StartCoroutine(SessionHelper.EndSession(PlayerPrefs.GetString("token")));

        PlayerPrefs.DeleteKey("playerName");
        PlayerPrefs.DeleteKey("token");
        PlayerPrefs.DeleteKey("playerId");
        PlayerPrefs.Save();  // Necessary???

        welcomePanel.SetActive(false);
        loginPanel.SetActive(true);
        playgameButton.interactable = false;
    }

    private void OnApplicationQuit()
    {
        if (PlayerPrefs.HasKey("token"))
        {
            StartCoroutine(SessionHelper.EndSession(PlayerPrefs.GetString("token")));
        }

    }

}
