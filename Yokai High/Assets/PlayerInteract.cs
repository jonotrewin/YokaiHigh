using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class PlayerInteract : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (PlayerInformation.Instance.isInCombat) return;

            if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if(Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1")) 
                interactable.Interact();
            }
        }       
    }
}
