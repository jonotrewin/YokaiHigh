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

        [Header("Variables")]
        [SerializeField] Camera battleCam;
        [SerializeField] EnemyCombatInstance[] enemyRenderers = Array.Empty<EnemyCombatInstance>(); // Manually assigned renderers in the scene


        public void ActivateBattle(CharacterGroup enemies)
        {
            _enemyTeam = enemies;

            _playerTeam = PlayerInformation.Instance.characterGroup;

            // _selectedTeammate = _playerTeam.party[0];
            // _selectedEnemy = _enemyTeam.party[0];

            for (int i = 0; i < enemyRenderers.Length; i++)
            {
                if (i < _enemyTeam.party.Count)
                {
                    enemyRenderers[i].transform.parent.gameObject.SetActive(true);
                    enemyRenderers[i].SetReference(_enemyTeam.party[i], this);
                }


                else
                {
                    enemyRenderers[i].transform.parent.gameObject.SetActive(false);
                    // Hide unused renderers
                }


            }
        }

        public void DamageEnemy(float amount)
        {
            _selectedEnemy.CurrentHealth -= amount;
        }
        public void DamagePlayer(float amount)
        {
            _selectedTeammate.CurrentHealth -= amount;
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