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
        [Space]

        CharacterGroup _playerTeam;
        CharacterGroup _enemyTeam;

        [SerializeField] PlayerCombatInstance _selectedTeammate;
        EnemyCombatInstance _selectedEnemy;

        [Header("Variables")]
        [SerializeField] Camera battleCam;
        [SerializeField] EnemyCombatInstance[] enemyRenderers = Array.Empty<EnemyCombatInstance>(); // Manually assigned renderers in the scene



        private List<EnemyCombatInstance> _enemiesAlive = new List<EnemyCombatInstance>();


        int selectionIdx = 0;
        public void ActivateBattle(CharacterGroup enemies)
        {
            _enemyTeam = enemies;

            _playerTeam = PlayerInformation.Instance.characterGroup;
            //_selectedTeammate = _playerTeam.party[0];

            _selectedTeammate.SetReference(_playerTeam.party[0], this);

            // _selectedTeammate = _playerTeam.party[0];
            // _selectedEnemy = _enemyTeam.party[0];

            for (int i = 0; i < enemyRenderers.Length; i++)
            {
                if (i < _enemyTeam.party.Count)
                {
                    enemyRenderers[i].transform.gameObject.SetActive(true);
                    enemyRenderers[i].SetReference(_enemyTeam.party[i], this);
                    _enemiesAlive.Add(enemyRenderers[i]);
                }


                else
                {
                    enemyRenderers[i].transform.gameObject.SetActive(false);
                    // Hide unused renderers
                }


            }
            _selectedEnemy = enemyRenderers[0];
            _selectedEnemy.SetSelected(true);

        }

        public void DamageEnemy(float amount)
        {
            if (_selectedEnemy.Damage(amount))
            {
                _enemiesAlive.Remove(_selectedEnemy);
                foreach (EnemyCombatInstance instance in enemyRenderers)
                {
                    if (!instance.IsDead())
                    {
                        return;
                    }

                }
                StopCombat();
            }
        }
        public void AttemptCapture()
        {
            _selectedEnemy.AttemptCapture();
            foreach (EnemyCombatInstance instance in enemyRenderers)
            {
                if (!instance.IsDead())
                {
                    return;
                }

            }
            StopCombat();
        }

        internal void SlowEnemy()
        {
            _selectedEnemy.Slow();
        }

        public void DamagePlayer(float amount)
        {
            if (_selectedTeammate.Damage(amount))
            {
                if (_playerTeam.IsDead())
                {
                    StopCombat();
                }
            }
        }


        public void SwitchCharacter(CharacterStats selected)
        {
            _selectedTeammate.SetReference(selected, this);
        }

        public void NextTarget()
        {
            SwitchTarget(selectionIdx + 1);
        }
        public void PreviousTarget()
        {
            SwitchTarget(selectionIdx - 1);
        }

        public void SwitchTarget(int idx)
        {
            selectionIdx = idx % (_enemiesAlive.Count);
            _selectedEnemy = _enemiesAlive[selectionIdx];
            foreach (EnemyCombatInstance instance in enemyRenderers)
            { instance.SetSelected(false); }
            _selectedEnemy.SetSelected(true);
        }

        public void StopCombat()
        {
            AudioManager.Instance.Stop("CombatMusic");
            AudioManager.Instance.Stop("CombatStart");

            AudioManager.Instance.Play("WorldAmbience");
            AudioManager.Instance.Play("WorldMusic");



            PlayerInformation.Instance.ExitCombat();
            try
            {
                var whateva = (RandomEncounterGroup)GameObject.FindAnyObjectByType(typeof(RandomEncounterGroup));
                whateva.GenerateNewEncounter();
            }
            finally
            {
                SceneManager.UnloadSceneAsync("Combat");
            }

        }

    }
}