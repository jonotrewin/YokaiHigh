using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PopeBlocking : MonoBehaviour
{
    [SerializeField] List<CharacterStats> _characterStats = new List<CharacterStats>();
    [SerializeField] private int DiscoveriesNeeded = 6;

    private void FixedUpdate()
    {
        int discoveredAmt = 0;
        foreach (CharacterStats characterStats in _characterStats)
        {
            if (characterStats.IsDiscovered)
                discoveredAmt++;
        }
        if (discoveredAmt > DiscoveriesNeeded)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var dr = FindObjectOfType<DialogueRunner>();
        if (dr.IsDialogueRunning) return;
        dr.StartDialogue("PopeBlock");
    }
}
