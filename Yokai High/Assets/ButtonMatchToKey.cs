using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMatchToKey : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")||Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
