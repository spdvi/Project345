using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineObject : MonoBehaviour
{
    public Transform sphere1; // 1 2 3
    public Transform sphere2;  // 6 6 10

    private float rayDistance = 4f;
    [HideInInspector] public Transform outlinedObject = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 rayOrigin = sphere1.position;
        //Vector3 rayDirection = sphere2.position - sphere1.position;   // 5 4 7   magnitude = 10
        //float distance = Vector3.Distance(sphere2.position, sphere1.position);
        //Ray ray1 = new Ray(rayOrigin, rayDirection);
        //Debug.DrawRay(ray1.origin, ray1.direction * distance, Color.cyan);


        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0f));
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        
        if(Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, LayerMask.GetMask("Outlineable")))
        {
            //Debug.Log(hitInfo.transform.gameObject.name);
            if (!hitInfo.transform.GetComponent<Outline>().enabled)
            {
                outlinedObject = hitInfo.transform;
                hitInfo.transform.GetComponent<Outline>().enabled = true;                
            }
            
        }
        else
        {
            ClearOutlinedComponent();
        }
    }

    public void ClearOutlinedComponent()
    {
        if (outlinedObject != null)
        {
            outlinedObject.GetComponent<Outline>().enabled = false;
            outlinedObject = null;
        }
    }
}
