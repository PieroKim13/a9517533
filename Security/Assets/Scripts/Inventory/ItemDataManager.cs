using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCode
{
    Flashlight = 0,
    Radios
}

public enum ItemType
{
    None = 0,
    Equip,
    Consume
}

public class ItemDataManager : MonoBehaviour
{
    /// <summary>
    /// ������ ������ �����ϴ� �迭
    /// </summary>
    public ItemData[] itemDatas = null;

    /// <summary>
    /// ������ ������ ��
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public ItemData this[ItemCode code] => itemDatas[(int)code];

    public int length => itemDatas.Length;
}
