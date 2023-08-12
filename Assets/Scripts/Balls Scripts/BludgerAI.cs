using UnityEngine;

public class BludgerAI : MonoBehaviour
{
    public Transform playerTransform;
    public float movementSpeed = 5f;
    public float defaultHeight = 5f;
    public float ovalWidth = 10f;
    public float ovalHeight = 5f;

    private Rigidbody rb;
    private Vector3 centerPosition;

    private float angle = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        centerPosition = new Vector3(0f, defaultHeight, 0f);
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the direction towards the player
            Vector3 direction = playerTransform.position - transform.position;
            direction.Normalize();

            // Move the object towards the player
            rb.velocity = direction * movementSpeed;
        }
        else
        {
            // Calculate the position in an oval pattern
            float x = Mathf.Cos(angle) * ovalWidth / 2f;
            float y = Mathf.Sin(angle) * ovalHeight / 2f;
            Vector3 targetPosition = centerPosition + new Vector3(x, y, 0f);

            // Calculate the direction towards the target position
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();

            // Move the object towards the target position
            rb.velocity = direction * movementSpeed;

            // Update the angle for the next frame
            angle += Time.deltaTime * movementSpeed / Mathf.Max(ovalWidth, ovalHeight);
            if (angle > Mathf.PI * 2f)
            {
                angle -= Mathf.PI * 2f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // The object has collided with the player
            //Debug.Log("Object collided with the player!");
            // Perform any additional actions you want here

            // Stop the object's movement
            rb.velocity = Vector3.zero;
            // Optionally, you can disable the script or destroy the object
            // to prevent further movement or collisions
            // gameObject.SetActive(false);
            // Destroy(gameObject);
        }
    }
}
