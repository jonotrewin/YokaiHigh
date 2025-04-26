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
        _title.text = info.Name;
        _description.text = info.Description;
        _image.sprite = info.characterSprite;
        characterRef = info;
        SetIsDiscovered();
    }

    private void SetIsDiscovered()
    {
        if (characterRef.IsDiscovered)
        {
            _image.color = Color.white;
        }
        else
        {
            _image.color = Color.black;
        }
    }

    private void FixedUpdate()
    {
        SetIsDiscovered();
    }
}
