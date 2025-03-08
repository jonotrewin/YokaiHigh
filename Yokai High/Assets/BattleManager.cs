using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    class BattleManager : MonoBehaviour
    {
        bool isRunning = false;

        public CharacterTimer currentCharacter;
        CharacterTimer[] playerCharacters;
        int currentCharacterIndex = 0;
        CharacterTimer[] enemyCharacters;
        CharacterTimer selectedEnemy;

        [SerializeField] UnityEngine.UI.Slider playerSlider;
        [SerializeField] Transform enemySliderContainer; // Assign UI container for enemy sliders
        [SerializeField] Image playerHands;
        [SerializeField] SpriteRenderer[] enemyRenderers; // Manually assigned renderers in the scene

        float currentAttackBonus;
        float currentHealthIncrease;

        private Dictionary<CharacterTimer, Slider> enemySliders = new Dictionary<CharacterTimer, Slider>();
        private Dictionary<CharacterTimer, SpriteRenderer> enemySpriteMap = new Dictionary<CharacterTimer, SpriteRenderer>();

        public void ActivateBattle(CharacterGroup player, CharacterGroup enemies)
        {
            isRunning = true;
            playerCharacters = player.party;
            enemyCharacters = enemies.party;
            currentCharacter = playerCharacters[0];
            selectedEnemy = enemyCharacters[0];

            for (int i = 0; i < enemyRenderers.Length; i++)
            {
                if (i < enemyCharacters.Length)
                {
                    enemyRenderers[i].gameObject.SetActive(true);
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
                    enemyRenderers[i].gameObject.SetActive(false); // Hide unused renderers
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
                currentCharacter.currentTime = 0;
            }
            else if (enemyCharacters.Contains(character))
            {
                currentCharacter.CurrentHealth -= amount;
                Camera.main.GetComponent<CameraShake>().ShakeCamera();
            }
        }

        private IEnumerator FlashRed(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.05f);
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

            currentCharacter = playerCharacters[currentCharacterIndex];
            currentCharacter.enabled = true;
            currentCharacter.currentTime = 0;
            playerHands.sprite = currentCharacter.stats.armsRelaxed;
            UpdatePlayerSlider();
        }

        private void UpdatePlayerSlider()
        {
            playerSlider.targetGraphic.GetComponent<Image>().sprite = currentCharacter.stats.headSprite;
        }

        private void Update()
        {
            playerSlider.value = currentCharacter.currentTime;

            // Update each enemy's slider
            foreach (var enemy in enemyCharacters)
            {
                if (enemySliders.ContainsKey(enemy))
                {
                    enemySliders[enemy].value = enemy.currentTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) { SwitchCharacter(); }
            if (Input.GetKeyDown(KeyCode.Q)) { SwitchEnemy(-1); }
            if (Input.GetKeyDown(KeyCode.E)) { SwitchEnemy(1); }

            CheckIfDead();
            ButtonEffects();
        }

        private void SwitchEnemy(int direction)
        {
            if (enemyCharacters.Length <= 1) return;

            int currentIndex = Array.IndexOf(enemyCharacters, selectedEnemy);
            int newIndex = currentIndex;

            for (int i = 1; i <= enemyCharacters.Length; i++)
            {
                newIndex = (currentIndex + (i * direction) + enemyCharacters.Length) % enemyCharacters.Length;
                if (!enemyCharacters[newIndex].isDead)
                {
                    selectedEnemy = enemyCharacters[newIndex];
                    UpdateEnemyVisuals();
                    return;
                }
            }
        }

        private void UpdateEnemyVisuals()
        {
            foreach (var enemy in enemyCharacters)
            {
                if (enemySpriteMap.ContainsKey(enemy))
                {
                    enemySpriteMap[enemy].color = (enemy == selectedEnemy) ? Color.white : Color.gray;
                }
            }
        }

        private void ButtonEffects()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentAttackBonus += 1f;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentCharacter.currentTime += 3f;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentHealthIncrease += 0.3f;
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
                StopCombat();
            }

            if (selectedEnemy.isDead)
            {

                enemySpriteMap[selectedEnemy].color = Color.black;
                foreach (var character in enemyCharacters)
                {
                    if (!character.isDead)
                    {
                        selectedEnemy = character;
                        UpdateEnemyVisuals();
                        return;
                    }
                }
                StopCombat();
            }
        }

        private void StopCombat()
        {
            currentCharacter.gameObject.SetActive(false);
            selectedEnemy.gameObject.SetActive(false);
        }
    }
}
