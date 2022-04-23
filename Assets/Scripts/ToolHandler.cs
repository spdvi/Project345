using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    [SerializeField] private OutlineObject outlinerComponent;
    [SerializeField] private LevelManager levelManager;

    private int einesColocades = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.childCount == 0)
            {
                if (outlinerComponent.outlinedObject != null && outlinerComponent.outlinedObject.CompareTag("Eina"))
                {
                    GrabTool(outlinerComponent.outlinedObject);
                }
            }
            else if (transform.childCount == 1)
            {
                if (outlinerComponent.outlinedObject == null)
                {
                    DetachEina(transform.GetChild(0));
                }
                else if (outlinerComponent.outlinedObject.CompareTag("Penjador"))
                {
                    PenjarEina(outlinerComponent.outlinedObject);
                }
            }
        }
    }

    private void PenjarEina(Transform cilindrePenjador)
    {
        if (cilindrePenjador.childCount > 1)
        {
            // Error: ja hi ha una eina penjada. També podem comprovar en OutlineObject si ja hi ha una eina i no outlinearlo.
            DetachEina(transform.GetChild(0));
        }
        else
        {
            //Transform eina = this.transform.GetChild(0);
            //eina.GetComponent<Rigidbody>().isKinematic = true;
            //eina.GetComponent<Collider>().enabled = false;
            //eina.parent = penjador;
            //eina.localPosition = Vector3.zero;
            //eina.localRotation = Quaternion.identity * Quaternion.Euler(0,0,-90);
            ////eina.localScale = new Vector3(1f, 1f, 1f);
            //eina.GetComponent<Outline>().enabled = false;

            Transform eina = this.transform.GetChild(0);
            string einaAInstanciar = eina.name.Split("(")[0];
            Destroy(this.transform.GetChild(0).gameObject);
            GameObject einaPrefab = Resources.Load<GameObject>(@"Eines/" + einaAInstanciar);
            GameObject novaEina = Instantiate<GameObject>(einaPrefab, cilindrePenjador.parent);
            novaEina.transform.localPosition = Vector3.zero;
            novaEina.GetComponent<Rigidbody>().isKinematic = true;
            novaEina.GetComponent<Collider>().enabled = true;
            novaEina.layer = 0;  // Ja no es pot interactuar amb la eina. Una vegada penjada no es pot despenjar.
            cilindrePenjador.gameObject.layer = 0;  // Si hi ha un objecte penjat, deixa de respondre als raycasts
            outlinerComponent.ClearOutlinedComponent();

            einesColocades++;

            // Comprovar si totes les eines s'han posat
            if (einesColocades == levelManager.einesInicialsNivell)
            {
                // Not a good system. May be eines in the floor.
                levelManager.CheckToolPlacement();
            }
        }
    }

    private void DetachEina(Transform eina)
    {
        eina.parent = null;
        eina.GetComponent<Rigidbody>().isKinematic = false;
        eina.GetComponent<Rigidbody>().AddForce(eina.forward * 2, ForceMode.Impulse);
        eina.GetComponent<Collider>().enabled = true;
    }

    private void GrabTool(Transform eina)
    {
        eina.GetComponent<Rigidbody>().isKinematic = true;
        eina.GetComponent<Collider>().enabled = false;
        eina.parent = this.transform;
        eina.localPosition = Vector3.zero;
        eina.localRotation = Quaternion.identity;
        eina.localScale = new Vector3(1f, 1f, 1f);
        outlinerComponent.ClearOutlinedComponent();
    }
}
