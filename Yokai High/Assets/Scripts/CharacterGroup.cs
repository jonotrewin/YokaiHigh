using System.Collections.Generic;
using UnityEngine;


namespace Assets
{
    public class CharacterGroup : MonoBehaviour
    {

        public List<CharacterStats> party;
        public bool IsDead()
        {
            foreach (CharacterStats stats in party)
            {
                if (stats.currentHP > 0)
                    return false;
            }

            return true;

        }

    }
}

