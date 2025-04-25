using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    class ButtonUILogic : MonoBehaviour
    {
        [SerializeField] Slider healthButton;
        [SerializeField] Slider DamageButton;
        BattleManager bm;

        private void Start()
        {
            bm = FindAnyObjectByType<BattleManager>();
        }
        private void Update()
        {

        }
    }
}
