using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatInstance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CharacterStats _characterReference;
    void Start()
    {

    }

    public void SetReference(CharacterStats character)
    {
        _characterReference = character;
        _spriteRenderer.sprite = character.characterSprite;
    }
}
