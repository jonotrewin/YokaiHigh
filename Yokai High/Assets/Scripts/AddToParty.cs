using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

namespace Assets
{
    public class AddToParty : MonoBehaviour
    {
        public CharacterTimer characterToAdd;

        [YarnCommand("AddCharacterToParty")]
        public void AddCharacterToParty() { 
            PlayerInformation.Instance.characterGroup.party.Add(characterToAdd);
        }
    }
}
