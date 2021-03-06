using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEina : MonoBehaviour
{
    [SerializeField] private List<GameObject> einesPrefabs;
    private int magatzemLength = 15, magatzemWidth = 10, magatzemHeight = 7;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InstantiateEina();
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
}
