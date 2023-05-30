using UnityEngine;

public class BludgerLogic : MonoBehaviour
{

    public enum State
    {
        Patrol = 1,
        Attack = 2,
        Chase = 3,
        Reset = 4
    }

    public Transform[] targets;
    private Transform target;
    private float elapsedTime = 0f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float defaultHeight = 5f;
    [SerializeField] float ovalWidth = 10f;
    [SerializeField] float ovalHeight = 5f;
    public State state;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip howlSound;
    private AudioSource audioSource;
    private Rigidbody rb;
    private Vector3 centerPosition;

    private float angle = 0f;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        centerPosition = new Vector3(0f, defaultHeight, 0f);
        state = State.Patrol;
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Random.Range(0, 200) == 0 && state == State.Patrol)
        {
            state = State.Attack;
        }

        if (state == State.Attack)
        {
            state = State.Chase;
            elapsedTime = 0f;
            target = GetClosestTarget();
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * movementSpeed;
        }
        else if (state == State.Patrol)
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
        else if (state == State.Chase)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * movementSpeed;
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 20f)
                state = State.Patrol;
        }
    }

    private Transform GetClosestTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // The object has collided with a player
            Debug.Log("Object collided with a player!");
            // Perform any additional actions you want here
            audioSource.PlayOneShot(hitSound);
            // Stop the object's movement
            rb.velocity = Vector3.zero;
            // Optionally, you can disable the script or destroy the object
            // to prevent further movement or collisions
            // gameObject.SetActive(false);
            // Destroy(gameObject);
        }
        state = State.Patrol;
    }
}
