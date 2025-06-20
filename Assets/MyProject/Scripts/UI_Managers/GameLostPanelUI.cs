using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLostPanelUI : MonoBehaviour
{
    [SerializeField] private Button returnButton;


    // Start is called before the first frame update
    void Start()
    {

        returnButton.onClick.AddListener(OnReturnButtonClicked);
    }

    void OnReturnButtonClicked()
    {
        gameObject.SetActive(false);
        LoadingScene.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    void OnDestroy()
    {
        returnButton.onClick.RemoveListener(OnReturnButtonClicked);
    }
}
