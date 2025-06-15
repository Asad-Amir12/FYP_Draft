using UnityEngine;

public class GroundCheckCollider : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
           // playerMovement.SetGrounded(true);
            Debug.Log("Grounded: " );
        }
    }

}
