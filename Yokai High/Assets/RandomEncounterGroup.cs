using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounterGroup : MonoBehaviour
{
    [SerializeField] CharacterGroup _characterGroup;
    [SerializeField] List<CharacterStats> _characterStats = new List<CharacterStats>();
    void Start()
    {
        GenerateNewEncounter();
        Object.DontDestroyOnLoad(transform.parent.gameObject);
    }

    public void GenerateNewEncounter()
    {
        List<CharacterStats> newPart = new List<CharacterStats>();
        int groupSize = Random.Range(1, 4);
        for (int i = 0; i < groupSize; i++)
        {
            CharacterStats thisAnimal = _characterStats[Random.Range(0, _characterStats.Count)];
            newPart.Add(thisAnimal);
        }
        _characterGroup.party = newPart;
    }
}
