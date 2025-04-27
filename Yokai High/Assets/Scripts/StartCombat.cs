using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

namespace Assets
{
    public class StartCombat : MonoBehaviour
    {
        CharacterGroup characterGroup;
        [SerializeField] public UnityEvent onDefeat;

        private void Start()
        {

            characterGroup = GetComponent<CharacterGroup>();
            onDefeat.AddListener(SetDefeated);
        }
        public void Interact()
        {
            StartCombatUp();
        }

        [YarnCommand("startcombatup")]
        public void StartCombatUp()
        {
            StartCoroutine(LoadCombat());
        }

        private IEnumerator LoadCombat()
        {
            //AudioManager.Instance.Play("CombatMusic");
            //AudioManager.Instance.Play("CombatStart");
            //
            //AudioManager.Instance.Stop("WorldAmbience");
            //AudioManager.Instance.Stop("WorldMusic");


            PlayerInformation.Instance.EnterCombat();
            yield return SceneManager.LoadSceneAsync("Combat", LoadSceneMode.Additive);

            FindObjectOfType<BattleManager>().ActivateBattle(characterGroup);
        }

        public void SetDefeated()
        {
            var variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();


            variableStorage.SetValue("$" + this.GetInstanceID() + "defeated", true);

        }
    }
}
