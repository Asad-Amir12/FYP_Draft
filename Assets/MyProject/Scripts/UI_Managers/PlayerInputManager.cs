using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    [SerializeField] private Cinemachine.CinemachineFreeLook freeLook;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerStateMachine playerStateMachine;


    void Awake()
    {
        inputReader.OnMenuControlsPerformed += OnMenuControlsPerformed;
        EventBus.GameResumed += OnResume;

    }

    private void OnResume()
    {
        freeLook = FindObjectOfType<Cinemachine.CinemachineFreeLook>();
        freeLook.enabled = true;
    }

    private void OnMenuControlsPerformed()
    {

        freeLook = FindObjectOfType<Cinemachine.CinemachineFreeLook>();
        if (!freeLook)
        {
            Debug.LogError("FreeLook is NULL");
        }
        freeLook.enabled = !inputReader.GamePaused;

    }
    public void ToggleFreelook(bool state)
    {
        freeLook.enabled = state;
    }

    public void TogglePlayerStateMachine(bool state)
    {
        playerStateMachine.enabled = state;
    }


}
