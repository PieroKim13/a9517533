using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    /// <summary>
    /// 이 웨이포인트가 가지고 있는 웨이포인트 지점들
    /// </summary>
    public Transform[] waypoints;

    /// <summary>
    /// 현재 이동중인 웨이포인트 지점의 인덱스
    /// </summary>
    int index = 0;

    /// <summary>
    /// 현재 이동중인 웨이포인트 지점
    /// </summary>
    public Transform CurrentWaypoint => waypoints[index];

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);   // 자식들의 트랜스폼을 찾아 저장하기
        }
    }

    /// <summary>
    /// 다음 웨이포인트를 돌려주고 CurrentWaypoint 지정하는 함수
    /// </summary>
    /// <returns>다음 웨이포인트의 트랜스폼</returns>
    public Transform GetNextWaypoint()
    {
        index++;
        index %= waypoints.Length;  // index가 waypoints.Length와 같아지면 index는 0이 된다.

        return waypoints[index];
    }
}
