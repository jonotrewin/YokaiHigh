using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Character")]
    public class CharacterStats : ScriptableObject
    {
        [Header("Basic info")]
        public string Name;
        public string Description;
        public bool IsDiscovered = false;


        [Space]
        [Header("Combat stats")]

        public float speed;
        public float currentHP;
        public float hpMax;
        public float strength;

        [Space]
        [Header("sprites")]
        public Sprite characterSprite;
        public Sprite characterSpriteReady;
        public Sprite characterSpriteAttack;

        [Space]
        [Header("Combat moves")]
        [SerializeField] List<ActionWeights> _actions = new List<ActionWeights>();

        public void HealFull()
        {
            currentHP = hpMax;
        }

        public void Heal(float amt)
        {
            currentHP += amt;
            if (currentHP > hpMax)
            {
                currentHP = hpMax;
            }
        }
        public EnemyActions GetAction()
        {
            if (_actions == null || _actions.Count == 0)
                return EnemyActions.Flop;
            if (_actions.Count == 1)
                return _actions[0].Action;

            float CumulativeWeight = 0;
            foreach (ActionWeights action in _actions)
            {
                CumulativeWeight += action.Weight;
            }

            float chance = Random.Range(0, CumulativeWeight);

            float chanceSearch = 0;
            foreach (ActionWeights action in _actions)
            {
                chanceSearch += action.Weight;
                if (chance <= chanceSearch)
                    return action.Action;
            }


            return EnemyActions.Flop;
            // fallback
        }

    }

    [Serializable]
    public class ActionWeights
    {
        public EnemyActions Action;
        public float Weight;
    }
}

