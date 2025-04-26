using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

namespace Assets
{
    public class PlayerInformation : MonoBehaviour
    {
        public static PlayerInformation Instance;
        public PlayerSaveData SaveData;


        public bool isInCombat;
        public CharacterGroup characterGroup;
        public PlayerMovement movement;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("MULTIPLE PLAYER INFORMATIONS");
            }
            // characterGroup = GetComponent<CharacterGroup>();
            // movement = GetComponent<PlayerMovement>();

            if (!SaveData.isNewSave)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            // characterGroup.party = SaveData.CharacterGroup;
            // movement = SaveData.movement;
        }

        private void OnDisable()
        {
            SaveData.isNewSave = false;
            SaveData.CharacterGroup = characterGroup.party;
            SaveData.movement = movement;
        }

        public void EnterCombat()
        {
            isInCombat = true;
            movement.enabled = false;
        }

        public void ExitCombat()
        {
            isInCombat = false;
            movement.enabled = true;
        }
    }
}
