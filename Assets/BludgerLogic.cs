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
    private bool onTrack;
    private Transform target;
    private float chaseTimer = 0f;
    private float patrolTimer = 0f;
    private int rand;

    public State state;

    private AudioSource audioSource;
    public AudioClip howlLoop;

    private Rigidbody rb;
    private Vector3 direction, targetPosition;

    [SerializeField] 
    float posY = 10f, rotationRadius = 370f, angularSpeed = 1f, ovalWidth = 3f, movementSpeed = 50f;
    [SerializeField] private AudioClip hitSound, howlSound;
    [SerializeField]
    Transform rotationCenter;

    private float deltaY, posX, posZ, angle = 0f;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = State.Patrol;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = howlLoop;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        if (state == State.Patrol && Random.Range(0, (10000 - (int)(patrolTimer*10))) == 0)
        {
            state = State.Attack;
            onTrack = true;
        }

        if (state == State.Attack)
        {
            state = State.Chase;
            chaseTimer = 0f;
            target = GetClosestTarget();
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * movementSpeed;
        }
        else if (state == State.Patrol)
        {

            if (onTrack)
            {
                rand = Random.Range(0, 10);
                deltaY = Random.Range(0f, 20f);
                if (rand == 0) // change height at random
                {
                    if (posY < 25f)
                        posY += deltaY;
                    else if (posY > 250f)
                    {
                        posY -= deltaY;
                    }
                    else
                    {
                        if (rand >= 25)
                            posY += deltaY;
                        else
                            posY -= deltaY;
                    }
                    Debug.Log(posY.ToString());
                }
                posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius / ovalWidth;
                posZ = rotationCenter.position.z + Mathf.Sin(angle) * rotationRadius;
                targetPosition = new Vector3(posX, posY, posZ);
                angle += Time.deltaTime * movementSpeed / ovalWidth;
                if (angle > Mathf.PI * 2f)
                {
                    angle -= Mathf.PI * 2f;
                }
            }
            onTrack = (Vector3.Distance(targetPosition, transform.position) < 15f);
            direction = targetPosition - transform.position;
            direction.Normalize();
            rb.velocity = direction * movementSpeed;

            patrolTimer += Time.deltaTime;

        }
        else if (state == State.Chase)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * movementSpeed;
            chaseTimer += Time.deltaTime;
            if (chaseTimer >= 20f)
            {
                state = State.Patrol;
                onTrack = true;
                patrolTimer = 0f;
            }
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
            Debug.Log("Object collided with a player!");
            //audioSource.PlayOneShot(hitSound);
            rb.velocity = Vector3.zero;
        }
        state = State.Patrol;
        onTrack = true;
        patrolTimer = 0f;
    }
}
