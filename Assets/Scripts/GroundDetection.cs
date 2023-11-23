using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    // Sets isGrounded = true if colliding with Object that has Layer "Ground"
    private void OnTriggerStay2D(Collider2D collider)
    {
        isGrounded = collider != null && (((1 << collider.gameObject.layer) & groundLayer) != 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}