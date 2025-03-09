using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Tweak Healing and Extra Damage here")]
        [SerializeField] float healPerClick = 1f;
        [SerializeField] float extraDamagePerClick = 5f;
        [SerializeField] float timePerClick = 3f;

        [Space(3f)]
        public bool isRunning = false;

        [SerializeField]Camera battleCam;

        public CharacterTimer currentCharacter;
        CharacterTimer[] playerCharacters;
        int currentCharacterIndex = 0;
        int currentEnemyIndex = 0;
        CharacterTimer[] enemyCharacters;
        CharacterTimer selectedEnemy;

        [SerializeField] UnityEngine.UI.Slider playerSlider;
        [SerializeField] UnityEngine.UI.Slider enemyHealthBar;
        [SerializeField] private Image image;
        [SerializeField] Transform enemySliderContainer; // Assign UI container for enemy sliders
        [SerializeField] Image playerHands;
        [SerializeField] SpriteRenderer[] enemyRenderers; // Manually assigned renderers in the scene

        public float currentAttackBonus;
        public float currentHealthIncrease;

        private Dictionary<CharacterTimer, Slider> enemySliders = new Dictionary<CharacterTimer, Slider>();
        private Dictionary<CharacterTimer, SpriteRenderer> enemySpriteMap = new Dictionary<CharacterTimer, SpriteRenderer>();

        [SerializeField] GameObject winScreen;
        [SerializeField] GameObject loseScreen;

        [SerializeField] Animator Hand;
        [SerializeField] Animator Knob;
        [SerializeField] GameObject BAMVisual;

        [SerializeField] private Color chargeStartColor = Color.yellow; // Customizable in Inspector

        [SerializeField] private Color chargeMidColor = Color.red; // Customizable in Inspector

        [SerializeField] private Color chargeEndColor = Color.yellow; // Customizable in Inspector

        [Header("Visual Settings")]

        [SerializeField] private float starterValue = 0;
        [SerializeField] private float midValue = 25;
        [SerializeField] private float endValue = 40;


        public void ActivateBattle(CharacterGroup enemies)
        {
            isRunning = true;
            
            playerCharacters = PlayerInformation.Instance.characterGroup.party;



            enemyCharacters = enemies.party;
            currentCharacter = playerCharacters[0];
            selectedEnemy = enemyCharacters[0];

            foreach (CharacterTimer timer in enemyCharacters)
            {
                timer.enabled = true;
                timer.battleManager = this;
            }
            foreach (CharacterTimer timer in playerCharacters)
            {
                timer.battleManager = this;
            }

            for (int i = 0; i < enemyRenderers.Length; i++)
            {
               
                if (i < enemyCharacters.Length)
                {
                    enemyRenderers[i].transform.parent.gameObject.SetActive(true);
                    enemyRenderers[i].sprite = enemyCharacters[i].stats.characterSprite;
                    enemySpriteMap[enemyCharacters[i]] = enemyRenderers[i];

                    // Create enemy sliders
                    Slider clonedSlider = Instantiate(playerSlider, enemySliderContainer);
                    clonedSlider.gameObject.SetActive(true);
                    enemySliders[enemyCharacters[i]] = clonedSlider;
                    clonedSlider.targetGraphic.GetComponent<Image>().sprite = enemyCharacters[i].stats.headSprite;
                }
                else
                {
                    enemyRenderers[i].transform.parent.gameObject.SetActive(false);
                    // Hide unused renderers
                }
            }

            currentCharacter.enabled = true;
            selectedEnemy.enabled = true;
            UpdateEnemyVisuals();
            UpdatePlayerSlider();
        }

        public void Damage(CharacterTimer character, float amount)
        {
            if (character == currentCharacter)
            {
                selectedEnemy.CurrentHealth -= amount + currentAttackBonus;
                currentCharacter.CurrentHealth += currentHealthIncrease;
                currentAttackBonus = 0;
                currentHealthIncrease = 0;

                


                if (enemySpriteMap.ContainsKey(selectedEnemy))
                {
                    StartCoroutine(FlashRed(enemySpriteMap[selectedEnemy]));
                }

                StartCoroutine(AnimateHit());
                //currentCharacter.currentTime = 0;
            }
            else if (enemyCharacters.Contains(character))
            {
                currentCharacter.CurrentHealth -= amount;
                battleCam.GetComponent<CameraShake>().ShakeCamera();
                StartCoroutine(AnimateEnemyAttack(character));

            }
        }

        private void HandAnimationLogic()
        {

            if (currentCharacter.currentTime >= 50)
            {
                
            }
            else if (currentCharacter.currentTime >= 25)
            {
               
            }






            if (currentAttackBonus >= endValue)
            {

                Hand.gameObject.GetComponent<Image>().color = chargeEndColor;
            }
            else if (currentAttackBonus >= midValue)
            {
                Hand.Play("ChargeMid");
                Hand.gameObject.GetComponent<Image>().color = chargeMidColor;
            }
            else if (currentAttackBonus > starterValue)
            {
                Hand.Play("ChargeStart");
                Hand.gameObject.GetComponent<Image>().color = chargeStartColor;
            }
        }

        private IEnumerator AnimateEnemyAttack(CharacterTimer character)
        {
            enemySpriteMap[character].sprite = selectedEnemy.stats.characterSpriteAttack;
            yield return new WaitForSeconds(0.3f);
            enemySpriteMap[character].sprite = selectedEnemy.stats.characterSprite;


        }

        private IEnumerator AnimateHit()
        {
            enemySpriteMap[selectedEnemy].transform.parent.GetComponent<SpriteAnimator>().PlayShake();
            playerHands.sprite = currentCharacter.stats.armHit;
            Hand.Play("ChargeHit");

            BAMVisual.SetActive(true);
            BAMVisual.GetComponent<Animator>().Play("Pom");
            yield return StartCoroutine(AdjustFOV(96f, 85f, 0.25f));
            yield return StartCoroutine(AdjustFOV(85f, 96f, 0.25f)); // FOV goes back up immediately

            Debug.Log( currentAttackBonus);
            yield return new WaitForSeconds(0.5f);
            playerHands.sprite = currentCharacter.stats.armsRelaxed;
            Hand.gameObject.GetComponent<Image>().color = Color.white;
            BAMVisual.SetActive(false);
        }

        private IEnumerator AdjustFOV(float startFOV, float endFOV, float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                battleCam.fieldOfView = Mathf.Lerp(startFOV, endFOV, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            battleCam.fieldOfView = endFOV; // Ensure it reaches the exact target value
        }

        private IEnumerator FlashRed(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);






            UpdateEnemyVisuals();
        }

        public void SwitchCharacter()
        {
            if (!isRunning) return;
            if (playerCharacters.Length <= 1)
            {
                Debug.Log("No Other Characters");
                return;
            }


            currentCharacter.GetComponent<CharacterTimer>().enabled = false;

            if (currentCharacterIndex < playerCharacters.Length - 1) currentCharacterIndex++;
            else currentCharacterIndex = 0;

            if(playerCharacters[currentCharacterIndex].isDead)
            {
                Debug.Log("Tried to choose dead character");
            }
            currentCharacter = playerCharacters[currentCharacterIndex];
            currentCharacter.enabled = true;
            currentCharacter.currentTime = 0;
            playerHands.sprite = currentCharacter.stats.armsRelaxed;
            playerHands.GetComponent<Animator>().Play("HandsUp"); 
            UpdatePlayerSlider();
        }

        private void UpdatePlayerSlider()
        {
            playerSlider.targetGraphic.GetComponent<Image>().sprite = currentCharacter.stats.headSprite;
            image.sprite = currentCharacter.stats.headSprite;

        }

        private void Update()
        {
            if (!isRunning) return;
            playerSlider.value = currentCharacter.currentTime;
            HandAnimationLogic();
            // Update each enemy's slider
            foreach (var enemy in enemyCharacters)
            {
                if (enemySliders.ContainsKey(enemy))
                {
                    enemySliders[enemy].value = enemy.currentTime;
                }
            }

            // Character switching
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButtonDown("Jump"))
            {
                SwitchCharacter();
            }

            // Enemy selection
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Next Select"))
            {
                SwitchEnemy(-1);
            }
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Previous Select"))
            {
                SwitchEnemy(1);
            }

            CheckIfDead();
            ButtonEffects();

            CheckIfReadyToAttack();
        }

        private void CheckIfReadyToAttack()
        {
            foreach (var enemy in enemyCharacters)
            {
                if (enemy.currentTime > 0 && enemy.currentTime < 40)
                {
                    Animator shakerAnim = enemySpriteMap[enemy].GetComponentInChildren<Animator>();
                    shakerAnim.Play("Base");


                }
                else if (enemy.currentTime > 40 && enemy.currentTime < 80)
                {
                    enemySpriteMap[enemy].sprite = enemy.stats.characterSpriteReady;
                    Animator shakerAnim = enemySpriteMap[enemy].GetComponentInChildren<Animator>();
                    shakerAnim.Play("Shake");


                }
                else if (enemy.currentTime > 80)
                {
                    enemySpriteMap[enemy].GetComponentInChildren<Animator>().Play("Shake2");
                }


            }

        }

        private void SwitchEnemy(int direction)
        {
            if (enemyCharacters.Length < 1) return;

            int startIndex = currentEnemyIndex;

            for (int i = 0; i < enemyCharacters.Length; i++) // Loop to find a valid enemy
            {
                currentEnemyIndex = (currentEnemyIndex + direction + enemyCharacters.Length) % enemyCharacters.Length;

                if (!enemyCharacters[currentEnemyIndex].isDead)
                {
                    selectedEnemy = enemyCharacters[currentEnemyIndex];
                    UpdateEnemyVisuals();
                    return;
                }
            }

            // If all enemies are dead, keep the original index
            currentEnemyIndex = startIndex;
            Debug.Log("No valid enemy to switch to.");
        }



        private void UpdateEnemyVisuals()
        {
            foreach (var enemy in enemyCharacters)
            {
                enemySpriteMap[enemy].transform.parent.GetComponentInChildren<Slider>().value = enemy.CurrentHealth;
                if (enemy.isDead)
                {
                    if (enemySpriteMap[enemy].color != Color.black) enemySpriteMap[enemy].color = Color.black;
                    continue;
                }
                if (enemySpriteMap.ContainsKey(enemy))
                {

                    enemySpriteMap[enemy].color = (enemy == selectedEnemy) ? Color.white : Color.gray;
                }
            }
        }

        private void ButtonEffects()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetButtonDown("Fire2"))
            {
                currentAttackBonus += extraDamagePerClick + (1.15f * currentCharacter.stats.strength);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetButtonDown("Fire3"))
            {
                float totalTimeToAdd = timePerClick + (float)(currentCharacter.stats.speed * 10f); // New calculation
                StartCoroutine(AddOverTime(() => currentCharacter.currentTime += totalTimeToAdd / 10f, totalTimeToAdd));
                Knob.Play("Knob animation");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Fire1"))
            {
                currentHealthIncrease += healPerClick + (0.01f * currentCharacter.stats.hpMax);
                currentAttackBonus -= extraDamagePerClick + (1.15f * currentCharacter.stats.strength);
            }
        }

        private IEnumerator AddOverTime(System.Action incrementAction, float totalAmount)
        {
            float amountPerTick = totalAmount / 10f; // Spread over 1 second in 10 steps
            for (int i = 0; i < 10; i++)
            {
                incrementAction.Invoke();
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void CheckIfDead()
        {
            if (currentCharacter.isDead)
            {
                foreach (var character in playerCharacters)
                {
                    if (!character.isDead)
                    {
                        SwitchCharacter();
                        return;
                    }
                }
                loseScreen.SetActive(true);
            }

            if (selectedEnemy.isDead)
            {

                enemySpriteMap[selectedEnemy].color = Color.black;
                foreach (var character in enemyCharacters)
                {
                    if (!character.isDead)
                    {
                        selectedEnemy = character;
                        //UpdateEnemyVisuals();
                        return;
                    }
                }
                winScreen.SetActive(true);
            }
        }

        public void StopCombat()
        {
            
            foreach(var character in playerCharacters)
            { character.currentTime = 0;
                character.isDead = false;
                character.isAttacking = false;
                character.enabled = false;
            }
            selectedEnemy.GetComponentInParent<StartCombat>().onDefeat.Invoke();
            PlayerInformation.Instance.ExitCombat();
            SceneManager.UnloadSceneAsync("Combat");
        }
    }
}
