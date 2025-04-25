using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Assets
{
    public class ChangeYarnVariable:MonoBehaviour
    {
        
        public void ChangeVariable(string variableName)
        {
            var variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
            
                variableStorage.SetValue(variableName,  true);
            
        }

    }
}
