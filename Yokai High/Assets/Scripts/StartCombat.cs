using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class StartCombat : MonoBehaviour, IInteractable
    {
        CharacterGroup characterGroup;
  
        private void Start()
        {
            
            characterGroup = GetComponent<CharacterGroup>();
        }
        public void Interact()
        {
            StartCoroutine(LoadCombat());
        }

        private IEnumerator LoadCombat()
        {
            PlayerInformation.Instance.EnterCombat();
            SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
            yield return new WaitForSeconds(1);
            FindObjectOfType<BattleManager>().ActivateBattle(characterGroup);
        }
    }
}
