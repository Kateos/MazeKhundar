using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Private variables

    // Reference to the camera component
    private Camera myCamera;

    #endregion

    #region Unity callbacks

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        SetCamera();
    }

    #endregion

    #region Camera functions

    /// <summary>
    /// Set up the camera to see the maze correctly
    /// </summary>
    void SetCamera()
    {
        if(ManagerGame.instance.Score > 0)
        {
            switch (ManagerGame.instance.Score % 4)
            {
                case 0:
                    transform.position = new Vector3(0.5f, 10.0f, 0.0f);
                    break;
                case 1:
                    transform.position = new Vector3(0.0f, 10.0f, 0.0f);
                    break;
                case 2:
                    transform.position = new Vector3(0.5f, 10.0f, -0.5f);
                    break;
                case 3:
                    transform.position = new Vector3(0.0f, 10.0f, -0.5f);
                    break;
            }
        }

        if(ManagerGame.instance.Score == 1)
        {
            myCamera.orthographicSize = 5;
        }
        else
        {
            myCamera.orthographicSize = 4 + ManagerGame.instance.Score;
        }
    }

    #endregion
}
