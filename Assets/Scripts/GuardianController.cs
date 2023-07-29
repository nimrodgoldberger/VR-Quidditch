//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public enum eGuardianState { Idle = 0, Defend = 1, GoBack = 2 }



//public class GuardianController : MonoBehaviour
//{
//    [SerializeField]
//    private float defenceRadius;
//    [SerializeField]
//    private GameObject[] targetsToDefend;
//    [SerializeField] 
//    private GameObject[] oponents;

//    private GameObject targetToTackle;
//    private Vector3 direction;
//    public float speed;
//    private bool isMoving = false;
//    private Coroutine movementCoroutine;

//    private Vector3 startingPosition;
//    private eGuardianState eState;


//    //For rotation
//    [SerializeField] private float RotationSpeed = 3f;
//    private Coroutine LookCoroutine;

//    // Start is called before the first frame update
//    void Start()
//    {
//        startingPosition = transform.position;
//        eState = eGuardianState.Idle;
//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        if(!isMoving)
//        {
//            if(eState == eGuardianState.Idle)
//            {
//                initializeMovement();
//            }
//            else
//            {
//                tackleTarget();
//            }
//        }
        
//    }

//    private void initializeMovement()
//    {
//        bool foundEnemy = searchForCloseOponents();

//        if(!foundEnemy && transform.position != startingPosition)
//        {
//            eState = eGuardianState.GoBack;
//        }

//        if(eState != eGuardianState.Idle)
//        {
//            tackleTarget();
//        }
//    }

//    private bool searchForCloseOponents()
//    {
//        bool found = false;
//        foreach(GameObject target in targetsToDefend)
//        {
//            foreach(GameObject oponent in oponents)
//            {
//                if(IsOponentClose(target, oponent))
//                {
//                    eState = eGuardianState.Defend;
//                    targetToTackle = oponent;
//                    found = true;
//                    break;
//                }
//            }
//            if(found)
//            {
//                break;
//            }
//        }

//        return found;
//    }

//    private bool IsOponentClose(GameObject target, GameObject oponent)
//    {
//        if(Vector3.Distance(oponent.transform.position, target.transform.position) < defenceRadius)
//        {
//            return true;
//        }

//        return false;
//    }

//    private void tackleTarget()
//    {
//        float distance = eState == eGuardianState.Defend ? Vector3.Distance(transform.position, targetToTackle.transform.position) : Vector3.Distance(transform.position, startingPosition);

//        if(distance <= speed * Time.deltaTime)
//        {
//            // Target is within range, set the GameObject's position to the target position
//            transform.position = eState == eGuardianState.Defend ? targetToTackle.transform.position : startingPosition;
//        }
//        else
//        {
//            // Start moving towards the target
//            StartRotating();
//            StartCoroutine(MoveToTarget());
//        }

//        movementCoroutine = StartCoroutine(MoveToTarget());
//    }

//    private IEnumerator MoveAndRotateToTarget()
//    {
//        Vector3 initialPosition = transform.position;
//        Vector3 targetPosition = searchForCloseOponents() ? targetToTackle.transform.position : startingPosition;
//        Quaternion initialRotation = transform.rotation;
//        Quaternion lookRotation = Quaternion.LookRotation(targetToTackle.transform.position - transform.position);
//        float elapsedTime = 0f;
        
//        while(elapsedTime < 1f)
//        {
//            // Calculate the new position using Mathf.Lerp
//            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
//            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, elapsedTime);

//            // Update the elapsed time based on the speed and rotationSpeed average increase
//            // Maybe needs adjustments
//            elapsedTime +=  ((Time.deltaTime * speed)+(Time.deltaTime * RotationSpeed))/2 ;

//            yield return null; // Wait for the next frame
//        }
//    }


//    private System.Collections.IEnumerator MoveToTarget()
//    {
//        isMoving = true;

//        Vector3 initialPosition = transform.position;

//        Vector3 targetPosition = searchForCloseOponents() ? targetToTackle.transform.position : startingPosition;

//        float elapsedTime = 0f;

//        while(elapsedTime < 1f)
//        {
//            // Calculate the new position using Mathf.Lerp
//            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);

//            // Update the elapsed time based on the speed
//            elapsedTime += Time.deltaTime * speed;

//            yield return null; // Wait for the next frame
//        }
//        eState = eGuardianState.Idle;
//        isMoving = false;
//    }

//    private void StartRotating()
//    {
//        if(LookCoroutine != null)
//        {
//            StopCoroutine(LookCoroutine);
//        }

//        LookCoroutine = StartCoroutine(LookAt());
//    }

//    private IEnumerator LookAt()
//    {
//        Quaternion lookRotation = Quaternion.LookRotation(targetToTackle.transform.position - transform.position);

//        float time = 0;

//        Quaternion initialRotation = transform.rotation;
//        while(time < 1)
//        {
//            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, time);

//            time += Time.deltaTime * RotationSpeed;

//            yield return null;
//        }
//    }
//}
