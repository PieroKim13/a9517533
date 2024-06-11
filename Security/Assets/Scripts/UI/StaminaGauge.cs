using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaGauge : GaugeBase
{
    private void Start()
    {
        Player player = GameManager.Inst.Player;
        maxValue = player.MaxStamina;
        slider.value = player.Stamina / maxValue;
        player.onStaminaChange += OnValueChange;

    }
}
