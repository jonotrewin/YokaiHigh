using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSceneSwitcher : MonoBehaviour
{
    [SerializeField] private BattleManager _battleManager; 
    [SerializeField] private float _waitTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForSeconds());
    }

    private IEnumerator WaitForSeconds()
    {

        yield return new WaitForSeconds(_waitTime);
        _battleManager.StopCombat();



    }
}
