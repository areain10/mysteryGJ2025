using UnityEngine;

public class SmoothMouseFollow : MonoBehaviour
{
    public float smoothTime = 0.1f; // Adjust for desired smoothness
    public float moveSpeed = 5f; // Adjust for desired speed

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Smoothly move towards mouse position
        transform.position = Vector3.SmoothDamp(transform.position, mousePosition, ref velocity, smoothTime);
    }
}