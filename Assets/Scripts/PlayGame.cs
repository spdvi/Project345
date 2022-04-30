using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Button>().onClick.AddListener(LoadGame);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
    }

}
