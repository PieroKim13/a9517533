using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class OpeningCameraMove : MonoBehaviour
{
    public Waypoint waypoints;

    Transform target;

    Vector3 moveDir = Vector3.zero;

    float moveSpeed = 0.56f;

    int i = 0;

    private void Start()
    {
        target = waypoints.CurrentWaypoint;
    }

    private void Update()
    {
        CameraMoving();
    }

    private void CameraMoving()
    {
        switch (i)
        {
            case 0:
                if (transform.position.z < (target.position.z + 1.0f))
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
                else
                {
                    target = waypoints.GetNextWaypoint();
                    i++;
                    transform.position = target.position;
                }
                break;
            case 1:
                if (transform.position.z > (target.position.z - 1.0f))
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                else
                {
                    target = waypoints.GetNextWaypoint();
                    i++;
                    transform.position = target.position;
                }
                break;
            case 2:
                transform.rotation = Quaternion.Euler(0, -180, 0);
                if (transform.position.x < (target.position.x + 1.0f))
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                else
                {
                    target = waypoints.GetNextWaypoint();
                    i++;
                    transform.position = target.position;
                }
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                i++;
                break;
        }

    }
}
