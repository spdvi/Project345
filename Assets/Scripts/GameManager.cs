using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button playGameButton;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("playerName") && PlayerPrefs.HasKey("token"))
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
