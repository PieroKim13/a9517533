using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Objec/Item Data/ Item Data", order = 1)]

public class ItemData : ScriptableObject
{
    [Header("아이템 기본 정보")]
    public ItemCode code;

    /// <summary>
    /// 아이템 이름
    /// </summary>
    public string itemName = "";
    
    /// <summary>
    /// 아이템 모델
    /// </summary>
    public GameObject modelPrebaf;

    /// <summary>
    /// 아이템 아이콘
    /// </summary>
    public Sprite itemIcon;
    
    /// <summary>
    /// 아이템 가격
    /// </summary>
    public uint price = 0;
    
    /// <summary>
    /// 아이템 크기(카운트)
    /// </summary>
    public uint maxStackCount = 1;
    
    /// <summary>
    /// 아이템 설명
    /// </summary>
    public string itemDescription = "";
}
