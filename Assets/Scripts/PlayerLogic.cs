using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLogic : MonoBehaviour
{

    public QuaffleLogic quaffleLogic;
    // Team of the player
    

    public PlayerTeam playerTeam;

    
    //__________________________________________TAKING A BALL_____________________________________________________
    // Method to attempt to take the ball from the current holder or from the ground/air
    public void StartTryTakeBall()
    {
        StartCoroutine(TryTakeBall());
    }

    // Coroutine to attempt to take the ball from the current holder or from the ground/air
    private IEnumerator TryTakeBall()
    {
        GameObject initialHolder = quaffleLogic.GetHolder();
        GameObject currentHolder = initialHolder;

        // Check if the ball is not held by anyone, take it immediately
        if(currentHolder == null)
        {
            quaffleLogic.TakeFrom(null, gameObject);
        }
        else
        {
            // Check if the player is from a different team and is close enough to the current holder
            if(quaffleLogic.CanBeTaken(gameObject) && currentHolder.GetComponent<PlayerLogic>().playerTeam != playerTeam)
            {
                // Increment a timer to ensure the player is close enough for 0.5 seconds
                float takeTimer = 0f;
                bool isCloseEnough = true;

                while(takeTimer < 0.5f && isCloseEnough && initialHolder == currentHolder)
                {
                    // Check if the player is still close enough
                    isCloseEnough = quaffleLogic.CanBeTaken(gameObject);

                    // Check if the holder of the ball changed during the Coroutine
                    currentHolder = quaffleLogic.GetHolder();

                    if(isCloseEnough && initialHolder == currentHolder)
                    {
                        // Update the timer and wait for the next frame
                        takeTimer += Time.deltaTime;
                    }
                    else
                    {
                        // If the player moved away or the holder changed, reset the timer and exit the loop
                        takeTimer = 0f;
                    }

                    // IMPORTANT: This will yield control to Unity for one frame,
                    // allowing the rest of the game to keep running.
                    yield return null;
                }

                // After 0.5 seconds and the player is still close enough and the holder is the same,
                // take the ball from the current holder
                if(isCloseEnough && initialHolder == currentHolder)
                {
                    quaffleLogic.TakeFrom(currentHolder, gameObject);
                }
            }
        }
    }


    //___________________________________________THROWING OR PASSING A BALL_______________________________________
    // Method to decide whether to throw or pass the ball
    public void ThrowOrPassBall(Transform target, float throwForce)
    {
        // Check if the player is holding the ball
        if(IsHoldingBall())
        {


            // Generate a random number between 0 and 1
            float randomValue = Random.value;

            // If the randomValue is less than or equal to 0.66, throw the ball to the target
            if(randomValue <= 0.66f)
            {
                // Implement your throw logic here, using the 'target' and 'throwForce'
                // to determine the direction and power of the throw.
                // Example:
                Rigidbody ballRigidbody = quaffleLogic.GetComponent<Rigidbody>();
                Vector3 throwDirection = (target.position - ballRigidbody.position).normalized;
                ballRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            }
            else
            {
                // If the randomValue is greater than 0.66, pass the ball to a teammate
                // Implement your pass logic here, where you find a nearby teammate to pass the ball to.
                // Example:
                GameObject[] teammates = GetTeammates();
                if(teammates.Length > 0)
                {
                    // Choose a random teammate
                    GameObject teammateToPass = teammates[Random.Range(0, teammates.Length)];
                    quaffleLogic.TakeFrom(gameObject, teammateToPass);
                }
            }
        }
    }
    // Method to get an array of teammates of the same team
    private GameObject[] GetTeammates()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> teammates = new List<GameObject>();

        foreach(GameObject player in allPlayers)
        {
            if(player.GetComponent<PlayerLogic>().playerTeam == playerTeam && player != gameObject)
            {
                teammates.Add(player);
            }
        }

        return teammates.ToArray();
    }


    //___________________________________________GENERAL METHODS__________________________________________________
    public bool IsHoldingBall()
    {
        GameObject currentHolder = quaffleLogic.GetHolder();

        // If the current holder is this player, return true (player is holding the ball)
        return currentHolder == gameObject;
    }

    public PlayerTeam GetTeam()
    {
        return playerTeam;
    }

}
