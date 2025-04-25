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
    public class PlayerInformation: MonoBehaviour
    {
        public static PlayerInformation Instance;


        public bool  isInCombat;
        public CharacterGroup characterGroup;
        public PlayerMovement movement;

        private void Start()
        {
            Instance = this;
            
            characterGroup = GetComponent<CharacterGroup>();
            movement = GetComponent<PlayerMovement>();
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
