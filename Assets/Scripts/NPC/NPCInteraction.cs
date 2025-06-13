using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionPrompt; // "Press E to Talk" UI
    public GameObject dialogueCanvas;    // Dialogue UI Canvas

    private bool playerInRange = false;

    void Start()
    {
        interactionPrompt.SetActive(false);
        dialogueCanvas.SetActive(false);
        Time.timeScale = 1f; // Ensure time is normal at start
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenDialogue();
        }

        // Optional: Close dialogue with Escape
        if (dialogueCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDialogue();
        }
    }

    void OpenDialogue()
    {
        dialogueCanvas.SetActive(true);
        interactionPrompt.SetActive(false);
        Time.timeScale = 0f; // Pause the game
    }

    void CloseDialogue()
    {
        dialogueCanvas.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false);
            playerInRange = false;
            CloseDialogue(); // In case player exits trigger while dialogue is open
        }
    }
}
