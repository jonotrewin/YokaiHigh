using Febucci.UI;
using Febucci.UI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using Assets;

public class Interactable_Dialogue : MonoBehaviour, IInteractable
{
    [SerializeField]string characterName; //this needs to be the same as name in yarn file

    DialogueRunner dr;
    TypewriterCore twc;
    SpriteAnimator anim;
    TextMeshProUGUI characterNameUI;


    public string conversationStartNode;

    private void Start()
    {
       TryGetComponent<SpriteAnimator>(out anim);
    }
    public void Interact()
    {
        
        
        if(dr == null) dr = FindObjectOfType<DialogueRunner>();
        if (dr.IsDialogueRunning) return; 
        dr.StartDialogue(conversationStartNode);
        if(twc ==  null) twc = FindObjectOfType<TypewriterCore>();
     
        twc.onTypewriterStart.AddListener(AnimateSpeech);
        twc.onTextShowed.AddListener(StopAnimateSpeech);
        TypewriterByCharacter typewriterByCharacter = twc as TypewriterByCharacter;
        if (typewriterByCharacter != null)
        {
            typewriterByCharacter.onCharacterVisible.AddListener((x) => { PlaySound(); });
        }
        if (characterNameUI == null) characterNameUI = GameObject.FindGameObjectWithTag("CharacterName").GetComponent<TextMeshProUGUI>();

        dr.onDialogueComplete.AddListener(RemoveAllListeners);
   






    }

    private void RemoveAllListeners()
    {
        StopAnimateSpeech();
        twc.onTypewriterStart.RemoveListener(AnimateSpeech);
        twc.onTextShowed.RemoveListener(StopAnimateSpeech);
        dr.onDialogueComplete.RemoveListener(RemoveAllListeners);
    }

    private void AnimateSpeech()
    {
        //call animation here
        
        if(characterName != characterNameUI.text) return;
        GetComponent<SpriteAnimator>().PlaySquashStretch();
        Debug.Log("Anim");
    }
    private void StopAnimateSpeech()
    {
      
    }

    private void PlaySound()
    {
        if (characterName != characterNameUI.text) return;
        //sound logic here.
        Debug.Log("Dialogue Sound Played");
        //_dialogueAudio.PlaySelectedDialogue();
    }

}
