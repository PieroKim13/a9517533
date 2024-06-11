using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Objec/Item Data/ Item Data", order = 1)]

public class ItemData : ScriptableObject
{
    [Header("������ �⺻ ����")]
    public ItemCode code;

    /// <summary>
    /// ������ �̸�
    /// </summary>
    public string itemName = "";
    
    /// <summary>
    /// ������ ��
    /// </summary>
    public GameObject modelPrebaf;

    /// <summary>
    /// ������ ������
    /// </summary>
    public Sprite itemIcon;
    
    /// <summary>
    /// ������ ����
    /// </summary>
    public uint price = 0;
    
    /// <summary>
    /// ������ ũ��(ī��Ʈ)
    /// </summary>
    public uint maxStackCount = 1;
    
    /// <summary>
    /// ������ ����
    /// </summary>
    public string itemDescription = "";
}
