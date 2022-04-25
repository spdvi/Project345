using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    [HideInInspector] public float elapsedSeconds = 0;
    private float elapsedMiliseconds = 0;

    private Slider slider;
    [SerializeField] private TextMeshProUGUI countdownText;

    public float LimitDeTemps { get; set; } = 5f;


    public UnityEvent countDownEvent;


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = LimitDeTemps;

        if (countDownEvent == null)
        {
            countDownEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.enabled)
        {
            elapsedMiliseconds += Time.deltaTime;
            elapsedSeconds += Time.deltaTime;
            if (elapsedMiliseconds >= 0.01f)
            {
                slider.value = slider.maxValue - elapsedSeconds;
                elapsedMiliseconds = 0f;
                countdownText.text = (LimitDeTemps - elapsedSeconds).ToString("n2");
            }

            if (elapsedSeconds >= LimitDeTemps)
            {
                //Debug.Log("Se acabo el tiempo");
                countDownEvent.Invoke();
                this.enabled = false;
            }
        }
    }
}
