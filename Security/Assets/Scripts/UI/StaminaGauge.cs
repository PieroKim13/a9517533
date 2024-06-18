using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaGauge : GaugeBase
{
    private void Start()
    {
        // ���� �Ŵ������� �̱��� �޾ƿ���
        Player player = GameManager.Inst.Player;

        maxValue = player.MaxStamina;               // �ִ밪�� �÷��̾��� �ִ� ���׹̳�
        slider.value = player.Stamina / maxValue;   // �������� �� = �÷��̾��� ���� ���׹̳� / �ִ밪
        player.onStaminaChange += OnValueChange;

    }
}
