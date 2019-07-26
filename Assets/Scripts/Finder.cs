using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Finder : MonoBehaviour
{
    public Transform DestinationPoint;

    // Start is called before the first frame update
    void Start()
    {
        DestinationPoint = GameObject.FindObjectOfType<Exit>().transform;
        StartCoroutine(Co_FindDestination());
    }

    /// <summary>
    /// Give the destination to the agent
    /// </summary>
    IEnumerator Co_FindDestination()
    {
        yield return new WaitForSeconds(.1f);
        transform.GetComponent<NavMeshAgent>().destination = DestinationPoint.position;
    }
}
