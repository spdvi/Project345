using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Jugador")]
    [SerializeField] private GameObject player;

    [Header("Temporitzador")]
    [SerializeField] private Countdown countdown;
    [SerializeField] private float tempsLimit = 30f;

    [Header("Eines")]
    [SerializeField] public List<GameObject> einesPrefabs;
    [HideInInspector] public int einesInicialsNivell;

    [Header("Menus del nivell")]
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject levelMenuCanvas;
    [SerializeField] private GameObject levelFinishedCanvas;
    [SerializeField] private TextMeshProUGUI winLoseMessage;
    [SerializeField] private GameObject loseButtonsPanel;
    [SerializeField] private GameObject winButtonsPanel;

    [Header("PowerUps")]
    [SerializeField] private GameObject powerUpsPanel;

    private int magatzemLength = 15, magatzemWidth = 10, magatzemHeight = 7;
    private IntentResultsDto intent = new IntentResultsDto();
    private Dictionary<string,int> tools = new Dictionary<string,int>();
    private List<PowerUpInfo> powerUps = new List<PowerUpInfo>();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        einesInicialsNivell = einesPrefabs.Count;

        gameCanvas.SetActive(true);
        levelMenuCanvas.SetActive(false);
        levelFinishedCanvas.SetActive(false);
        loseButtonsPanel.SetActive(false);
        winButtonsPanel.SetActive(false);

        countdown.LimitDeTemps = tempsLimit;
        countdown.countDownEvent.AddListener(CountdownEnded);

        tools.Add("Destral", 1);
        tools.Add("Berbiqui", 3);
        tools.Add("Martell", 4);
        tools.Add("Serra", 8);

        StartCoroutine(ApiHelper.StartIntent(2, intent));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InstantiateEina();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Pause game and show Level menu canvas
            if (!levelMenuCanvas.activeInHierarchy)
            {
                PauseGame(false);
                levelMenuCanvas.SetActive(true);
            }
        }
    }

    private void InstantiateEina()
    {
        if (einesPrefabs.Count > 0)
        {
            int x = Random.Range(-magatzemWidth, magatzemWidth);
            int y = Random.Range(1, magatzemHeight);
            int z = Random.Range(-magatzemLength, magatzemLength);
            Vector3 randomPosition = new Vector3(x, y, z);

            int randomToolIndex = Random.Range(0, einesPrefabs.Count);
            if (einesPrefabs[randomToolIndex].GetComponent<Outline>() == null)
            {
                // Once a component is added to a prefab it remains in the prefab. If 
                // this is not the desired behaviour, add it to the Instance of the prefab.
                Outline outline = einesPrefabs[randomToolIndex].AddComponent<Outline>();
                outline.enabled = false;
            }

            Instantiate(einesPrefabs[randomToolIndex], randomPosition, Quaternion.identity);
            einesPrefabs.RemoveAt(randomToolIndex);
        }
        else
        {
            Debug.Log("No mes eines");
        }
    }


    public void CheckToolPlacement()
    {
        GameObject[] penjadors = GameObject.FindGameObjectsWithTag("Penjador");

        bool guanya = true;
        foreach (GameObject penjador in penjadors)
        {
            //if (penjador.transform.parent.childCount < 3)
            //{
            //    LoseGame("El penjador " + penjador.transform.name + "est� buit. Try again");
            //    return;
            //}
            string nomEina;
            if (penjador.transform.parent.childCount < 3)
            {
                nomEina = "none";
            }
            else
            {
                nomEina = penjador.transform.parent.GetChild(2).gameObject.name.Split('(')[0];
            }
            string nomTextPenjador = penjador.transform.parent.GetComponentInChildren<TextMeshPro>().text;
            if (!nomTextPenjador.Equals(nomEina))
            {
                guanya = false;
                intent.Result.Add(tools[nomTextPenjador], false);
            }
            else
            {
                intent.Result.Add(tools[nomTextPenjador], true);
            }

            //intent.Result.Add(idEina, 0);
        }
        if(guanya)
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    private void PauseGame(bool disableCountdown)
    {
        player.GetComponent<PlayerController>().enabled = false;
        Camera.main.GetComponent<MouseLook>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        countdown.enabled = !disableCountdown;
    }

    private void LoseGame()
    {
        // TODO: Call api to end intent and send intent data
        int id = intent.IdIntent;
        StartCoroutine(ApiHelper.UpdateIntent(intent));
        PauseGame(true);
        if (!levelFinishedCanvas.activeInHierarchy)
        {
            levelFinishedCanvas.SetActive(true);
            winLoseMessage.text = "Alguna eina no est� colocada correctament.";
            if (!loseButtonsPanel.activeInHierarchy)
            {
                loseButtonsPanel.SetActive(true);
            }
        }
    }

    private void WinGame()
    {
        // TODO: Call api to end intent and send intent data
        StartCoroutine(ApiHelper.UpdateIntent(intent));
        PauseGame(true);
        if (!levelFinishedCanvas.activeInHierarchy)
        {
            levelFinishedCanvas.SetActive(true);
            winLoseMessage.text = "Molt b�! Has completat la prova en " + (tempsLimit - countdown.elapsedSeconds).ToString("n2") + " segons!";
            if (!winButtonsPanel.activeInHierarchy)
            {
                winButtonsPanel.SetActive(true);
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueInLevel()
    {
        if (levelMenuCanvas.activeInHierarchy)
        {
            levelMenuCanvas.SetActive(false);
        }

        player.GetComponent<PlayerController>().enabled = true;
        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CountdownEnded()
    {
        //Debug.Log("Game over. You lose");
        CheckToolPlacement();
    }

    private void OnDestroy()
    {
        countdown.countDownEvent.RemoveListener(CountdownEnded);
    }

}
