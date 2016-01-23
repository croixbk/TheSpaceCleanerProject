using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10F;

    Rigidbody rb;
    Vector3 movimento;
    float x;
    float z;

    bool canJump = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
        {
            movimento = new Vector3(x, 0F, z);
            transform.Translate(movimento * velocidade * Time.deltaTime);
        }

        // Fins de teste
        if (!canJump)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, .5F))
                if (hit.collider.gameObject.name == "Plane")
                    canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.AddForce(transform.up * 9F, ForceMode.Impulse);
        }
    }
}