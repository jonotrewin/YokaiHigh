using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Assets
{
    public class Character : MonoBehaviour
    {

        [SerializeField] public CharacterStats stats;

        float currentHealth = 100;


        public bool isAttacking = false;

        public UnityEvent OnDeath;



        public float CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                if (value == currentHealth) { return; }

                currentHealth = value;

                if (value <= 0)
                {
                    _isDead = true;
                    OnDeath?.Invoke();
                    currentHealth = 0;
                }
            }
        }

        public bool _isDead = false;

        private void Start()
        {

            CurrentHealth = stats.hpMax;
        }


    }
}

