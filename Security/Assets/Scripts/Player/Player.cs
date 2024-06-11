using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    PlayerController controller;
    IEnumerator sprintCorutine;

    public Action<float> onStaminaChange { get; set; }

    bool IsAlive => hp > 0;

    private void Start()
    {
        sprintCorutine = sprintDerease();
    }

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        controller.onSprinting = () => StaminaChangeCoroutine(true);
        controller.offSprinting = () => StaminaChangeCoroutine(false);

    }

    /// <summary>
    /// ü��
    /// </summary>
    float hp = 100.0f;
    public float HP
    {
        get => hp;
        set
        {
            if (IsAlive)
            {
                hp = value;
                if (hp <= 0)
                {
                    hp = Mathf.Clamp(hp, 0, MaxHP);

                }
            }
        }
    }
    /// <summary>
    /// �ִ� ü��
    /// </summary>
    float maxHP = 100.0f;
    public float MaxHP => maxHP;

    /// <summary>
    /// ���׹̳�
    /// </summary>
    float stamina = 100.0f;
    public float Stamina
    {
        get => stamina;
        set
        {
            if (IsAlive)
            {
                stamina = Mathf.Clamp(value, 0, MaxStamina);
                onStaminaChange?.Invoke(stamina / MaxStamina);
            }
        }
    }
    /// <summary>
    /// �ִ� ���׹̳�
    /// </summary>
    float maxStamina = 100.0f;
    public float MaxStamina => maxStamina;

    /// <summary>
    /// ���׹̳� �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator sprintDerease()
    {
        while (true)
        {
            Stamina -= 20.0f * Time.deltaTime;
            if (Stamina <= 0)
            {
                controller.EndSequence();
            }
            yield return null;
        }
    }

    private void StaminaChangeCoroutine(bool isSprint)
    {
        if (isSprint)
        {
            StopCoroutine(sprintCorutine);
            sprintCorutine = sprintDerease();
            StartCoroutine(sprintCorutine);
        }
        else
        {
            StopCoroutine(sprintCorutine);
            sprintCorutine = StaminaRegetateCorutine();
            StartCoroutine(sprintCorutine);
        }
    }

    /// <summary>
    /// ���׹̳� �ڵ� ȸ�� �ڷ�ƾ(���� 0.75�� ��)
    /// </summary>
    /// <returns></returns>
    IEnumerator StaminaRegetateCorutine()
    {
        //�ڷ�ƾ ���� �� 0.5�� ���
        yield return new WaitForSeconds(0.75f);
        //ȸ���� = ��ü ���׹̳��� 1/10
        float regenpersec = MaxStamina / 10;
        float timeElapsed = 0.0f;
        while (timeElapsed < 10)
        {
            timeElapsed += Time.deltaTime;
            Stamina += Time.deltaTime * regenpersec;

            yield return null;
        }
    }
}
