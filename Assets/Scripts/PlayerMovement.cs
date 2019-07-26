using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region Public variables

    // To know if there are wall 
    public bool canGoLeft = true;
    public bool canGoRight = true;
    public bool canGoTop = true;
    public bool canGoBottom = true;

    #endregion

    #region Unity callbacks

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }

    #endregion

    #region Player functions

    /// <summary>
    /// Move the player with the arrows
    /// </summary>
    private void MoveThePlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && canGoLeft)
        {
            transform.position -= Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canGoRight)
        {
            transform.position += Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canGoTop)
        {
            transform.position += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && canGoBottom)
        {
            transform.position -= Vector3.forward;
        }
    }

    #endregion

}
