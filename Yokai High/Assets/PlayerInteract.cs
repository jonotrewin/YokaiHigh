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
        [SerializeField] GameObject prompt;
        private void OnTriggerStay(Collider other)
        {
            if (PlayerInformation.Instance.isInCombat) return;

            if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if(Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1")) 
                interactable.Interact();

                if(prompt.activeInHierarchy==false)
                prompt.SetActive(true);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out _))
                prompt.SetActive(false);
        }
       
    }
}
