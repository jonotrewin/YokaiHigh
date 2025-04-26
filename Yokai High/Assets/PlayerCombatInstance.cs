using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatInstance : MonoBehaviour
{
    [Header("Meow meow!!! :3")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CharacterStats _characterReference;
    [SerializeField] private Image Timer;
    [SerializeField] private TextMeshProUGUI _headsUpText;

    [SerializeField] private Slider _health;
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

        _characterReference.HealFull();

        _spriteRenderer.color = Color.white;

        HasInitialised = true;
    }


    private void FixedUpdate()
    {
        if (!HasInitialised)
            return;

        _health.value = _characterReference.currentHP / _characterReference.hpMax;

        timer += Time.deltaTime;
        Timer.fillAmount = timer / timePerAction;

        if (timer > timePerAction)
        {
            DoAction(_nextAction);
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
        }
    }

    public void SetAction(EnemyActions Action)
    {
        _nextAction = Action;
    }

    public void SetAction(int Action)
    {
        _nextAction = (EnemyActions)Action;
    }

    internal bool Damage(float amount)
    {
        _characterReference.currentHP -= amount;
        HasInitialised = false;

        if ((_characterReference.currentHP <= 0))
        {
            _spriteRenderer.color = Color.gray;
        }

        return (_characterReference.currentHP <= 0);
    }
}
