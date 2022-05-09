using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeViewMode : MonoBehaviour
{
    [SerializeField] private Image wheel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            wheel.fillAmount += 0.02f;
            if (wheel.fillAmount >= 1f)
            {
                //Debug.Log("Change mode");
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            wheel.fillAmount = 0f;
        }
    }
}
