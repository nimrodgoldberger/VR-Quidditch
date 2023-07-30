using UnityEngine;

public class QuaffleLogic : MonoBehaviour
{
    public GameObject holder;
    public GameObject[] team1Players;
    public GameObject[] team2Players;
    public GameObject team1VR;
    public GameObject team2VR; // TODO TESTING     Dan
    public float takeDistance = 2f;
    public float takeTime = 0.5f;

    private float takeTimer = 0f;

    private void FixedUpdate()
    {
        // Check if the ball is currently being held
        if(holder != null)
        {
            takeTimer = 0f;
            return;
        }

        // Check if any player on team 1 is holding the ball
        foreach(GameObject player in team1Players)
        {
            if(player.GetComponent<PlayerLogic>().IsHoldingBall())
            {
                holder = player;
                takeTimer = 0f;
                return;
            }
        }

        // Check if any player on team 2 is holding the ball
        foreach(GameObject player in team2Players)
        {
            if(player.GetComponent<PlayerLogic>().IsHoldingBall())
            {
                holder = player;
                takeTimer = 0f;
                return;
            }
        }

        // If nobody is holding the ball, reset the take timer
        takeTimer = 0f;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if(holder == null && other.CompareTag("Player"))
    //    {
    //        // Check if the potential holder is within range of the ball
    //        if(Vector3.Distance(transform.position, other.transform.position) < takeDistance)
    //        {
    //            // If the potential holder is from a different team,
    //            // set a delay of 0.5 seconds before taking the ball.
    //            if(other.GetComponent<PlayerLogic>().GetTeam() != GetComponent<PlayerLogic>().GetTeam())
    //            {
    //                // Increment the take timer if the player is close enough
    //                takeTimer += Time.deltaTime;
    //                if(takeTimer >= takeTime)
    //                {
    //                    // Set the holder to the potential holder
    //                    holder = other.gameObject;
    //                    takeTimer = 0f;
    //                }
    //            }
    //            else
    //            {
    //                // If the potential holder is from the same team, take the ball immediately.
    //                holder = other.gameObject;
    //                takeTimer = 0f;
    //            }
    //        }
    //        else
    //        {
    //            // Reset the take timer if the potential holder is out of range
    //            takeTimer = 0f;
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject == holder)
    //    {
    //        holder = null;
    //        takeTimer = 0f;
    //    }
    //}

    public GameObject GetHolder()
    {
        return holder;
    }

    public bool CanBeTaken(GameObject potentialHolder)
    {
        // Check if the potential holder is within range of the ball
        return Vector3.Distance(transform.position, potentialHolder.transform.position) < takeDistance;
    }

    public void TakeFrom(GameObject previousHolder, GameObject newHolder)
    {
        // Check if the previous holder is still holding the ball
        if(holder == previousHolder)
        {
            // Check if the new holder is within range of the ball
            if(CanBeTaken(newHolder))
            {
                holder = newHolder;

                transform.SetParent(holder.transform);
                transform.position = new Vector3(1f, 0f, 0f);
                //____________________________________________________________________________________________
                //                     ADD IMPLEMENTATION OF CONNECTING TO THE HOLDER
                //____________________________________________________________________________________________
            }
        }
    }




    //TODO Erase of fix     METHODS FOR TESTING
    public void VRTakesBall()
    {
        holder = team1VR;
    }

    public void VRLetsBallGo()
    {
        holder = null;
    }
}
