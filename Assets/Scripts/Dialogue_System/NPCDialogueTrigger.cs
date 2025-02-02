using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public DialogueSystem dialogueSystem;
    public Dialogue npcDialogue;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // Press 'E' to start
        {
            dialogueSystem.StartDialogue(npcDialogue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
