using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class EnemyCombatInstance : MonoBehaviour
{
    [Header("Meow meow!!! :3")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CharacterStats _characterReference;
    [SerializeField] private Image Timer;
    [SerializeField] private TextMeshProUGUI _headsUpText;
    [SerializeField] private Slider _health;

    private float Health;

    private float timePerAction { get { return (3 / _characterReference.speed); } }
    // magic number, dont worry about it!
    // higher speed does quicker actions makes more sense


    EnemyActions _nextAction;

    private BattleManager _battleManager;

    float timer = 0;

    bool HasInitialised = false;

    public void SetReference(CharacterStats character, BattleManager battle)
    {
        _characterReference = character;
        _spriteRenderer.sprite = character.characterSprite;
        _battleManager = battle;

        Health = _characterReference.hpMax;
        DecideAction();

        _spriteRenderer.color = Color.white;

        HasInitialised = true;
    }


    private void FixedUpdate()
    {
        if (!HasInitialised)
            return;

        _health.value = Health / _characterReference.hpMax;

        timer += Time.deltaTime;
        Timer.fillAmount = timer / timePerAction;

        if (timer > timePerAction)
        {
            DoAction(_nextAction);
            DecideAction();
            timer = 0f;
        }
        else if (timer * 4 > timePerAction * 3)
        {
            _spriteRenderer.sprite = _characterReference.characterSpriteAttack;
        }
        else if (timer * 2 > timePerAction)
        {
            _spriteRenderer.sprite = _characterReference.characterSpriteReady;
        }
        else
        {
            _spriteRenderer.sprite = _characterReference.characterSprite;
        }

    }

    private void DoAction(EnemyActions Action)
    {
        switch (Action)
        {
            default:
                _battleManager.DamagePlayer(1);
                break;

            case (EnemyActions.Attack):
                _battleManager.DamagePlayer(_characterReference.strength);
                break;

            case (EnemyActions.Heal):
                _characterReference.Heal(_characterReference.hpMax / 10);
                break;

            case (EnemyActions.None):
                break;
        }
    }

    public void DecideAction()
    {
        _nextAction = _characterReference.GetAction();
        _headsUpText.text = "Next: " + _nextAction.ToString();

    }

    public bool IsDead()
    {
        return !HasInitialised;
    }
    internal bool Damage(float amount)
    {
        Health -= amount;

        if ((Health <= 0))
        {
            _spriteRenderer.color = Color.gray;
            HasInitialised = false;
        }

        return (Health <= 0);
    }
}

[Serializable]
public enum EnemyActions
{
    Flop,
    Attack,
    Heal,
    Capture,
    None

}
