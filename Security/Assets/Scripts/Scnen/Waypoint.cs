using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    /// <summary>
    /// �� ��������Ʈ�� ������ �ִ� ��������Ʈ ������
    /// </summary>
    public Transform[] waypoints;

    /// <summary>
    /// ���� �̵����� ��������Ʈ ������ �ε���
    /// </summary>
    int index = 0;

    /// <summary>
    /// ���� �̵����� ��������Ʈ ����
    /// </summary>
    public Transform CurrentWaypoint => waypoints[index];

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);   // �ڽĵ��� Ʈ�������� ã�� �����ϱ�
        }
    }

    /// <summary>
    /// ���� ��������Ʈ�� �����ְ� CurrentWaypoint �����ϴ� �Լ�
    /// </summary>
    /// <returns>���� ��������Ʈ�� Ʈ������</returns>
    public Transform GetNextWaypoint()
    {
        index++;
        index %= waypoints.Length;  // index�� waypoints.Length�� �������� index�� 0�� �ȴ�.

        return waypoints[index];
    }
}
