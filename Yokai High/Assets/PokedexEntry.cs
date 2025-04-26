using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokedexEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _image;

    CharacterStats characterRef;
    public void SetData(CharacterStats info)
    {
        characterRef = info;

        _description.text = characterRef.Description;
        _image.sprite = info.characterSprite;
        SetIsDiscovered();
    }

    private void SetIsDiscovered()
    {
        if (characterRef.IsDiscovered)
        {
            _image.color = Color.white;
            _title.text = characterRef.Name;

        }
        else
        {
            _image.color = Color.black;
            _title.text = new string('?', characterRef.Name.Length);
        }
    }

    private void FixedUpdate()
    {
        SetIsDiscovered();
    }
}
