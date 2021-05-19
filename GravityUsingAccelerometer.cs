using UnityEngine;

public class GravityUsingAccelerometer : MonoBehaviour
{
    [SerializeField] float multiplier = 20;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Input.acceleration * multiplier);
    }
}
