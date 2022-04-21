using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLlumMagatzem : MonoBehaviour
{
    [SerializeField] private Light pointLight1;
    [SerializeField] private Light pointLight2;
    private Transform lightSwitch;
    

    private void Start()
    {
        // lightSwitch = this.gameObject.transform.Find("Switch");
        lightSwitch = transform.Find("Switch");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pointLight1.enabled)
            {
                // switch is up. Move it down
                lightSwitch.Rotate(Vector3.right, 60f, Space.Self); // Space.Self is default
            }
            else
            {
                lightSwitch.Rotate(Vector3.right, -60f, Space.Self);
            }
            pointLight1.enabled = !pointLight1.enabled;
            pointLight2.enabled = !pointLight2.enabled;

        }
    }

}
