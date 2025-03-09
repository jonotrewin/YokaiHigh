using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Doorway: MonoBehaviour
    {
        public Transform destination;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMovement>(out _))
            {
                other.gameObject.transform.position = destination.transform.position;
            }
        }
    }
}
