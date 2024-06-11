using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScr : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = GameManager.Inst.Player;
        player.Stamina -= 10.0f;
    }
}
