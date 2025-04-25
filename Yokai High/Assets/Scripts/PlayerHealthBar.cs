using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    BattleManager bm;
    Slider slider;
    [SerializeField]Slider increaseSlider;
    void Start()
    {
        bm = FindAnyObjectByType<BattleManager>();
        slider = GetComponent<Slider>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!bm.isRunning) return;
        slider.value = bm.currentCharacter.CurrentHealth;
        increaseSlider.value = slider.value + bm.currentHealthIncrease;
    }
}
