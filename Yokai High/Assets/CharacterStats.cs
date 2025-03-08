using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Character")]
    class CharacterStats : ScriptableObject
    {
        public float speed;
        public float hpMax;
        public float strength;
        public Sprite armsRelaxed;
        public Sprite armHit;
        public Sprite armDamage;
        public Sprite characterSprite;
        public Sprite headSprite;
        

    }
}

