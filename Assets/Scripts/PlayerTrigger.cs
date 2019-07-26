using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    // Have a reference to the PlayerMovement component
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    /// <summary>
    /// Check the colliders on Top, Right, Bottom and Left from the player
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WallTag>())
        {
            if(gameObject.name == "TopTrigger")
            {
                playerMovement.canGoTop = false;
            }
            else if (gameObject.name == "RightTrigger")
            {
                playerMovement.canGoRight = false;
            }
            else if (gameObject.name == "BottomTrigger")
            {
                playerMovement.canGoBottom = false;
            }
            else if (gameObject.name == "LeftTrigger")
            {
                playerMovement.canGoLeft = false;
            }
        }
    }

    /// <summary>
    /// Check the colliders on Top, Right, Bottom and Left from the player
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<WallTag>())
        {
            if (gameObject.name == "TopTrigger")
            {
                playerMovement.canGoTop = true;
            }
            else if (gameObject.name == "RightTrigger")
            {
                playerMovement.canGoRight = true;
            }
            else if (gameObject.name == "BottomTrigger")
            {
                playerMovement.canGoBottom = true;
            }
            else if (gameObject.name == "LeftTrigger")
            {
                playerMovement.canGoLeft = true;
            }
        }
    }
}
