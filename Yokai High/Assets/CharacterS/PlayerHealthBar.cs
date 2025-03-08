using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    BattleManager bm;
    Slider slider;
    void Start()
    {
        bm = FindAnyObjectByType<BattleManager>();
        slider = GetComponent<Slider>();
       
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = bm.currentCharacter.CurrentHealth;
    }
}
