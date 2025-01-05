using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    [SerializeField] private Cinemachine.CinemachineFreeLook freeLook;
    [SerializeField] private InputReader inputReader;



    void Awake(){
        inputReader.OnMenuControlsPerformed += OnMenuControlsPerformed;
        EventBus.GameResumed += OnResume;
    }

    private void OnResume(){
        freeLook.enabled = true;
    }

    private void OnMenuControlsPerformed()
    {
       
        freeLook.enabled = !inputReader.GamePaused;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
