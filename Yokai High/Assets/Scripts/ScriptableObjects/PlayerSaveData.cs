using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SaveData", menuName = "SaveData")]

public class PlayerSaveData : ScriptableObject
{
    public bool isNewSave = true;
    public List<Character> CharacterGroup = new List<Character>();
    public PlayerMovement movement;

}
