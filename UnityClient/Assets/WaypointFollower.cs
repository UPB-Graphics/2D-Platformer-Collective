using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(waypoints[currentWWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWWaypointIndex++;
            if (currentWWaypointIndex >= waypoints.Length)
            {
                currentWWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
