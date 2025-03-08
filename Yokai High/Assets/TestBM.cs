using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBM : MonoBehaviour
{
    [SerializeField] BattleManager bm;
    [SerializeField]CharacterGroup player;
    [SerializeField]CharacterGroup enemy;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            bm.ActivateBattle(player, enemy);
        }
    }
}
