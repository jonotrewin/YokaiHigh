using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyCombatInstance : MonoBehaviour
{
    [Header("Meow meow!!! :3")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CharacterStats _characterReference;
    [SerializeField] private Image Timer;
    [SerializeField] private TextMeshProUGUI _headsUpText;
    [SerializeField] private Slider _health;

    [SerializeField] private Transform _selection;

    [SerializeField] private SpriteAnimator SpriteAnimator;
    [SerializeField] Animator Shaker;


    float SlowModifier = 1;

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

        SlowModifier = 1;
        HasInitialised = true;
    }


    private void FixedUpdate()
    {
        if (!HasInitialised)
            return;

        _health.value = Health / _characterReference.hpMax;

        timer += Time.deltaTime;
        Timer.fillAmount = timer / (timePerAction * SlowModifier);

        if (timer > timePerAction * SlowModifier)
        {
            DoAction(_nextAction);
            DecideAction();
            timer = 0f;
        }
        else if (timer * 4 > timePerAction * 3 * SlowModifier)
        {
            // _spriteRenderer.sprite = _characterReference.characterSpriteAttack;
            SpriteAnimator.PlayShake();
        }
        else if (timer * 2 > timePerAction * SlowModifier)
        {
            // _spriteRenderer.sprite = _characterReference.characterSpriteReady;
        }
        else
        {
            // _spriteRenderer.sprite = _characterReference.characterSprite;
        }

    }

    public void SetSelected(bool isSelected)
    {
        _selection.gameObject.SetActive(isSelected);
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
            _spriteRenderer.sprite = _characterReference.characterSprite;

            HasInitialised = false;


            if (_characterReference.Name == "Popito Benedicto")
            {
                Debug.Log("GAME END");
                StartCoroutine(GameEnding());
            }
        }




        return (Health <= 0);
    }

    private IEnumerator GameEnding()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Ending");
    }

    internal bool AttemptCapture()
    {
        float chanceToCapture = ((100 - Health) / 100f);
        if (chanceToCapture >= Random.value)
        {
            Capture();
            HasInitialised = false;
            return true;
        }

        return false;
    }

    private void Capture()
    {
        _characterReference.currentHP = _characterReference.hpMax / 3;
        _characterReference.IsDiscovered = true;
        _spriteRenderer.color = Color.green;


    }

    internal void Slow()
    {
        SlowModifier *= 1.4f;
    }
}

[Serializable]
public enum EnemyActions
{
    Flop,
    Attack,
    Heal,
    Capture,
    Slow,
    None

}
