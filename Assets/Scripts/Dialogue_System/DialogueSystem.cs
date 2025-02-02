using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        [TextArea(3, 5)]
        public string dialogueText;
    }

    [System.Serializable]
    public class Dialogue
    {
        public List<DialogueLine> lines;
    }

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    public Dialogue currentDialogue;
    private int currentLineIndex = 0;

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(Dialogue newDialogue)
    {
        currentDialogue = newDialogue;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (currentLineIndex < currentDialogue.lines.Count)
        {
            DialogueLine line = currentDialogue.lines[currentLineIndex];
            nameText.text = line.speakerName;
            dialogueText.text = line.dialogueText;
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }
    }
}
