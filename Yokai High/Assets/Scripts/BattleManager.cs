using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;


namespace Assets
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Tweak Healing and Extra Damage here")]
        [SerializeField] float healPerClick = 1f;



        public void ActivateBattle(CharacterGroup enemies)
        {

        }

        public void Damage(Character character, float amount)
        {

        }

        private void HandAnimationLogic()
        {


        }


        public void SwitchCharacter()
        {

        }

        private void UpdatePlayerSlider()
        {


        }

        private void Update()
        {

        }

        private void CheckIfReadyToAttack()
        {


        }

        private void SwitchEnemy(int direction)
        {

        }



        private void UpdateEnemyVisuals()
        {

        }
        private bool wasLTPressed = false;
        private bool wasRTPressed = false;

        private void ButtonEffects()
        {

        }


        private void CheckIfDead()
        {

        }

        public void StopCombat()
        {


        }
    }
}