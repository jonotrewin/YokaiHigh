using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SaveData", menuName = "SaveData")]

public class PlayerSaveData : ScriptableObject
{
    public bool isNewSave = true;
    public List<CharacterStats> CharacterGroup = new List<CharacterStats>();
    public PlayerMovement movement;

}
