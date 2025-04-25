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
        [Header("Meow meow!!! :3")]
        [SerializeField] float healPerClick = 1f;

        CharacterGroup _playerTeam;
        CharacterGroup _enemyTeam;

        Character _selectedTeammate;
        Character _selectedEnemy;



        public void ActivateBattle(CharacterGroup enemies)
        {
            _enemyTeam = enemies;
        }

        public void Damage(Character character, float amount)
        {

        }

        private void HandAnimationLogic()
        {

        }


        public void SwitchCharacter(Character selected)
        {
            _selectedTeammate = selected;
        }

        public void SwitchTarget(Character selected)
        {
            _selectedEnemy = selected;
        }

        public void StopCombat()
        {


        }
    }
}