using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using static DialogueHelperClass;
using static ItemHelperClass;
using UnityEngine.UI;
using TMPro;

public class ItemGainedAnimation : MonoBehaviour
{
    public ItemData Item;
    [SerializeField] GameObject Bag;
    [SerializeField] Camera mainCamera;
    [SerializeField] Animator panelAnimator;
    [SerializeField] GameObject itemName;
    [SerializeField] GameObject itemDescription;
    public Image myImage;

    private Animator m_Animator;
    public Vector2 inputPositionVector;

    bool continueInputRecieved;
    bool dialogueEnded;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        myImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        InventoryManager.OnItemGained += ItemGained;
        InputManager.OnPosition += Position;
        DialogueManager.OnDialogueStarted += DialogueStarted;
        DialogueManager.OnDialogueEnded += DialogueEnded;
    }
        
    private void OnDisable()
    {
        InventoryManager.OnItemGained -= ItemGained;
        InputManager.OnPosition -= Position;
        DialogueManager.OnDialogueStarted -= DialogueStarted;
        DialogueManager.OnDialogueEnded -= DialogueEnded;
    }

    void ItemGained(ItemData item)
    {
        Item.ItemDescription = item.ItemDescription;
        Item.ItemName = item.ItemName;
        Item.ItemSprite = item.ItemSprite;
        myImage.sprite = Item.ItemSprite;
        StartCoroutine(MoveItemToBag());
    }

    void DialogueStarted(ConversationData data)
    {
        dialogueEnded = false;
    }

    void DialogueEnded()
    {
        dialogueEnded = true;
    }

    void Position(Vector2 input)
    {
        inputPositionVector = input;
    }

    private IEnumerator MoveItemToBag()
    {
        yield return new WaitUntil(() => dialogueEnded);
        continueInputRecieved = false;
        myImage.enabled = true;
        m_Animator.SetTrigger("MoveItem");
        panelAnimator.SetTrigger("PanelAppears");
        InputManager.OnClick += OnContinueInput;
        yield return new WaitUntil(() => continueInputRecieved);
        InputManager.OnClick -= OnContinueInput;
        itemName.SetActive(false);
        itemDescription.SetActive(false);
        m_Animator.SetTrigger("MoveItemBag");
        panelAnimator.SetTrigger("PanelDisappear");
    }

    private void EndOfItemAnimation()
    {
        itemName.SetActive(true);
        itemDescription.SetActive(true);
        itemName.GetComponent<TextMeshProUGUI>().text = Item.ItemName;
        itemDescription.GetComponent<TextMeshProUGUI>().text = Item.ItemDescription;
    }

    private void EndOfBagAnimation()
    {
        m_Animator.SetTrigger("BackToWait");
        panelAnimator.SetTrigger("BackToWait");
    }

    private void OnContinueInput() => continueInputRecieved = true;
}
