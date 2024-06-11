using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSlot : MonoBehaviour
{
    /// <summary>
    /// �κ��丮������ �ε���
    /// </summary>
    uint slotIndex;

    /// <summary>
    /// �κ��丮������ �ε����� Ȯ���ϱ� ���� ������Ƽ
    /// </summary>
    public uint Index => slotIndex;

    /// <summary>
    /// �� ���Կ� ����ִ� �������� ����
    /// </summary>
    ItemData slotItemData = null;

    /// <summary>
    /// �� ���Կ� ����ִ� �������� ������ Ȯ���ϱ� ���� ������Ƽ(����� private)
    /// </summary>
    public ItemData ItemData
    {
        get => slotItemData;
        private set
        {
            //������ ����� ��
            if(slotItemData != value)
            {
                //���� �۾� ó��
                slotItemData = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    /// <summary>
    /// ���Կ� ����ִ� �������� ����, ����, ��� ���ΰ� ����Ǿ��ٰ� �˸��� ��������Ʈ
    /// </summary>
    public Action onSlotItemChange;

    /// <summary>
    /// ���Կ� �������� �ִ��� ������ Ȯ���ϴ� ������Ƽ(true�� ����ְ�, false�� �������� ����ִ�.)
    /// </summary>
    public bool IsEmpty => slotItemData == null;

    /// <summary>
    /// �� ���Կ� ����ִ� ������ ����
    /// </summary>
    uint itemCount = 0;

    /// <summary>
    /// ������ ������ Ȯ���ϱ� ���� ������Ƽ(set�� private)
    /// </summary>
    public uint ItemCount
    {
        get => itemCount;
        private set
        {
            //������ ������ ���� ��
            if(itemCount != value)
            {
                itemCount = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    // <summary>
    /// �� ������ �������� ���Ǿ����� ����
    /// </summary>
    bool isEquipped = false;

    /// <summary>
    /// �� ������ ��񿩺θ� Ȯ���ϱ� ���� ������Ƽ
    /// </summary>
    public bool IsEquipped
    {
        get => isEquipped;
        set
        {
            IsEquipped = value;
            onSlotItemChange?.Invoke();
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="index">�� ������ �ε���(�κ��丮���� ���° ��������)</param>
    public InvenSlot(uint index)
    {
        slotIndex = index;
        ItemCount = 0;
        IsEquipped = false;
    }

    /// <summary>
    /// �� ���Կ� �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="data">������ ������ ����</param>
    /// <param name="count">������ ������ ����(set �뵵, �߰��Ǵ� ���� �ƴ�)</param>
    public void AssignSlotItem(ItemData data, uint count = 1)
    {
        if (data != null)
        {
            ItemData = data;
            ItemCount = count;
            IsEquipped = false;
            Debug.Log($"�κ��丮 {slotIndex}�� ���Կ� \"{ItemData.itemName}\" �������� {ItemCount}�� ����");
        }
        else
        {
            ClearSlotItem();    // data�� null�̸� �ش� ������ �ʱ�ȭ
        }
    }

    /// <summary>
    /// �� ������ ���� �Լ�
    /// </summary>
    public void ClearSlotItem()
    {
        ItemData = null;
        ItemCount = 0;
        IsEquipped = false;
        Debug.Log($"�κ��丮 {slotIndex}�� ������ ���ϴ�.");
    }

    /// <summary>
    /// �� ���Կ� ������ ������ ������Ű�� �Լ�
    /// </summary>
    /// <param name="overCount">(��¿�)�߰��ϴٰ� ��ģ ����</param>
    /// <param name="increaseCount">������ų ����</param>
    /// <returns>��������(true�� �ȳ�ġ�� �� �߰� �Ǽ� ����, false�� ���ƴ�)</returns>
    /// <summary>
    /// �� ���Կ� ������ ������ ������Ű�� �Լ�
    /// </summary>
    /// <param name="overCount">(��¿�)�߰��ϴٰ� ��ģ ����</param>
    /// <param name="increaseCount">������ų ����</param>
    /// <returns>��������(true�� �ȳ�ġ�� �� �߰� �Ǽ� ����, false�� ���ƴ�)</returns>
    public bool IncreaseSlotItem(out uint overCount, uint increaseCount = 1)
    {
        bool result;

        // ���߿� ������ �ڵ�
        result = false;
        overCount = 1;
        // -----------------
        int over;

        uint newCount = ItemCount + increaseCount;
        over = (int)newCount - (int)ItemData.maxStackCount;

        if (over > 0)
        {
            // ���ƴ�.
            ItemCount = ItemData.maxStackCount;
            overCount = (uint)over;
            result = false;
            Debug.Log($"�κ��丮 {slotIndex}�� ���Կ� \"{ItemData.itemName}\" �������� �ִ�ġ���� ����. ���� {ItemCount}��. {over}�� ��ħ");
        }
        else
        {
            // �ȳ��ƴ�.
            ItemCount = newCount;
            overCount = 0;
            result = true;
            Debug.Log($"�κ��丮 {slotIndex}�� ���Կ� \"{ItemData.itemName}\" �������� {increaseCount}�� ����. ���� {ItemCount}��.");
        }

        return result;
    }

    /// <summary>
    /// �� ���Կ� ������ ���� ���ҽ�Ű�� �Լ�
    /// </summary>
    /// <param name="decreaseCount">���ҽ�ų ������ ����</param>
    public void DecreaseSlotItem(uint decreaseCount = 1)
    {
        int newCount = (int)ItemCount - (int)decreaseCount;
        if (newCount < 1)
        {
            // ������ ������ ��� ���
            ClearSlotItem();
        }
        else
        {
            // ���Կ� �������� �����ִ� ���
            ItemCount = (uint)newCount;
            Debug.Log($"�κ��丮 {slotIndex}�� ���Կ� \"{ItemData.itemName}\" �������� {decreaseCount}�� ����. ���� {ItemCount}��.");
        }
    }

    public void UseItem(GameObject target)
    {

    }

    public void EquipItem(GameObject target)
    {

    }
}
