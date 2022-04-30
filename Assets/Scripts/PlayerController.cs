using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField] private float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //playerRb = this.gameObject.GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        playerRb.MovePosition(transform.position + (transform.forward * vInput + transform.right * hInput) * moveSpeed * Time.fixedDeltaTime);
    }

}
