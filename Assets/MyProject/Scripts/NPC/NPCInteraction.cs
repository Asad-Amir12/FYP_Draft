using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionPrompt; // "Press E to Talk" UI
    public GameObject dialogueCanvas;    // Dialogue UI Canvas
    private InputReader inputReader;
    [SerializeField] private Button closeButton;
    private PlayerInputManager inputManager;
    private bool playerInRange = false;

    void Start()
    {
        interactionPrompt.SetActive(false);
        dialogueCanvas.SetActive(false);
        Time.timeScale = 1f; // Ensure time is normal at start
        inputReader = FindObjectOfType<InputReader>();
        closeButton.onClick.AddListener(CloseDialogue);
        inputManager = FindObjectOfType<PlayerInputManager>();
        if (inputReader == null)
        {
            throw new Exception("InputReader not found in scene.");
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenDialogue();
        }



    }

    void OpenDialogue()
    {
        dialogueCanvas.SetActive(true);
        interactionPrompt.SetActive(false);
        //Time.timeScale = 0f; // Pause the game
        StartCoroutine(ToggleControls(false, 0.5f));

        inputManager.ToggleFreelook(false);
    }

    void CloseDialogue()
    {
        dialogueCanvas.SetActive(false);
        //Time.timeScale = 1f; // Resume the game
        inputManager.ToggleFreelook(true);
        StartCoroutine(ToggleControls(true, 0.5f));

    }

    IEnumerator ToggleControls(bool state, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        // inputReader.enabled = state;
        if (state)
            inputReader.EnableControls();

        else
            inputReader.DisableControls();

        inputManager.TogglePlayerStateMachine(state);
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
