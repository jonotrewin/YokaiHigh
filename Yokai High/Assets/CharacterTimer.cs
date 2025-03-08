using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets
{
    public class CharacterTimer : MonoBehaviour
    {

        public BattleManager battleManager;
        public float currentTime = 0;
        float timeToAttack = 100;
        [SerializeField] public CharacterStats stats;

        float currentHealth = 100;

        public bool isAttacking = false;

        


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

            CurrentHealth = stats.hpMax;
        }

        private void Update()
        {
            if(isDead) return;

            if (currentTime < timeToAttack)
                currentTime += stats.speed;
            else if(!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(ResetTime());
            }
        }

        private IEnumerator ResetTime()
        {
         
            battleManager.Damage(this, stats.strength);
            yield return new WaitForSeconds(0.5f);
            currentTime = 0;
            isAttacking = false;

        }


    }
}

