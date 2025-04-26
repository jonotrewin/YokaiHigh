using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokedex : MonoBehaviour
{
    [SerializeField] List<CharacterStats> _characterStats = new List<CharacterStats>();
    [SerializeField] PokedexEntry _baseEntry;
    private void Start()
    {
        foreach (CharacterStats characterStats in _characterStats)
        {
            GameObject thisObj = GameObject.Instantiate(_baseEntry.gameObject, _baseEntry.transform.parent);
            thisObj.SetActive(true);
            thisObj.GetComponent<PokedexEntry>().SetData(characterStats);

        }
        _baseEntry.gameObject.SetActive(false);
    }
}
