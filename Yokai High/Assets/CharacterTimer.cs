using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets
{
    class CharacterTimer : MonoBehaviour
    {
        [SerializeField] Slider slider;
        BattleManager battleManager;
        public float currentTime = 0;
        float timeToAttack = 100;
        [SerializeField] public CharacterStats stats;

        float currentHealth = 100;

        


        public float CurrentHealth
        {
            get { return currentHealth; }
            set 
            {
                currentHealth = value;
                if (value <= 0) isDead = true;
            }
        }

        public bool isDead = false;

        private void Start()
        {
            battleManager = FindAnyObjectByType<BattleManager>();
            CurrentHealth = stats.hpMax;
        }

        private void Update()
        {
            if(isDead) return;

            if (currentTime < timeToAttack)
                currentTime += stats.speed;
            else
            {
                currentTime = 0;
                battleManager.Damage(this,stats.strength);
            }
        }

   
    }
}

