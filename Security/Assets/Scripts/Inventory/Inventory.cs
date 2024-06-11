using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// �κ��丮�� ����ִ� �κ� ������ �⺻ ����
    /// </summary>
    public const int Default_Inventory_Size = 6;

    /// <summary>
    /// �ӽý��Կ� �ε���
    /// </summary>
    public const uint TempSlotIndex = 999;

    /// <summary>
    /// �� �κ��丮�� ����ִ� ������ �迭
    /// </summary>
    InvenSlot[] slots;

    /// <summary>
    /// �κ��丮 ���Կ� �����ϱ� ���� �ε���
    /// </summary>
    /// <param name="index">������ �ε���</param>
    /// <returns>����</returns>
    public InvenSlot this[uint index] => slots[index];

    /// <summary>
    /// �κ��丮 ������ ����
    /// </summary>
    public int SlotCount => slots.Length;

    /// <summary>
    /// �ӽ� ����(�巡�׳� ������ �и��۾��� �� �� ���)
    /// </summary>
    InvenSlot tempSlot;
    public InvenSlot TemSlot => tempSlot;

    /// <summary>
    /// ������ ������ �޴���(������ ������ �����͸� Ȯ���� �� �ִ�.)
    /// </summary>
    ItemDataManager itemDataManager;

    /// <summary>
    /// �κ��丮 ������
    /// </summary>
    Player owner;
    public Player Owner => owner;

    /// <summary>
    /// �κ��丮 ������
    /// </summary>
    /// <param name="owner">�κ��丮 ������</param>
    /// <param name="size">�κ��丮�� ũ��</param>
    public Inventory(Player owner, uint size = Default_Inventory_Size)
    {
        slots = new InvenSlot[size];
        for(uint i =0; i < size; i++)
        {
            slots[i] = new InvenSlot(i);
        }
        tempSlot = new InvenSlot(TempSlotIndex);
        itemDataManager = GameManager.Inst.ItemData;
        this.owner = owner;
    }

    /// <summary>
    /// �κ��丮�� �������� �ϳ� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="code">�߰��� ������ ����</param>
    /// <returns>true�� �߰� ����, false�� �߰� ����</returns>
    public bool AddItem(ItemCode code)
    {
        bool result = false;
        ItemData data = itemDataManager[code];

        InvenSlot sameDataSlot = FindSameItem(data);
        if (sameDataSlot != null)
        {
            // ���� ������ �������� �ִ�.
            // ������ ���� 1 ������Ű��� ��� �ޱ�
            result = sameDataSlot.IncreaseSlotItem(out uint _);  // ��ġ�� ������ �ǹ� ��� ���� ���� ����
        }
        else
        {
            // ���� ������ �������� ����.
            InvenSlot emptySlot = FindEmptySlot();
            if (emptySlot != null)
            {
                emptySlot.AssignSlotItem(data); // �󽽷��� ������ ������ �ϳ� �Ҵ�
                result = true;
            }
            else
            {
                // ����ִ� ������ ����.
                Debug.Log("������ �߰� ���� : �κ��丮�� ���� ���ֽ��ϴ�.");
            }
        }

        return result;
    }

    /// <summary>
    /// �κ��丮�� Ư�� ���Կ� �������� �ϳ� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="code"></param>
    /// <param name="slotIndex"></param>
    /// <returns></returns>
    public bool AddItem(ItemCode code, uint slotIndex)
    {
        bool result = false;

        return result;
    }


    // �κ��丮���� �������� ���� ������ŭ �����ϴ� �Լ�
    // �κ��丮���� �������� �����ϴ� �Լ�
    // �κ��丮�� ���� ���� �Լ�
    // �κ��丮�� Ư�� ��ġ���� �ٸ� ��ġ�� �������� �̵���Ű�� �Լ�
    // �κ��丮���� �������� ������ ����� �ӽ� �������� ������ �Լ�
    // �κ��丮�� �����ϴ� �Լ�

    /// <summary>
    /// �κ��丮�� �Ķ���Ϳ� ���� ������ �������� ����ִ� ������ ã�� �Լ�
    /// </summary>
    /// <param name="data">ã�� ������ ����</param>
    /// <returns>���� ������ �������� �κ��丮�� ������ null, ������ null�� �ƴ� ��</returns>
    InvenSlot FindSameItem(ItemData data)
    {
        return null;
    }

    /// <summary>
    /// �κ��丮���� ����ִ� ������ ã�� �Լ�
    /// </summary>
    /// <returns>����ִ� ����(ù��°)</returns>
    InvenSlot FindEmptySlot()
    {
        return null;
    }
}
