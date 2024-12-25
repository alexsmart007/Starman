using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueTextField;
    [SerializeField] Image textBoxImage;
    [SerializeField] Image voiceTextBoxImage;

    public void Display() => ToggleChildrenDisplay(true);

    public void Hide() => ToggleChildrenDisplay(false);

    private void ToggleChildrenDisplay(bool shouldDisplay)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(shouldDisplay);
        }
    }

    public void UpdateDialogueText(string text)
    {
        dialogueTextField.text = text;
    }
}
