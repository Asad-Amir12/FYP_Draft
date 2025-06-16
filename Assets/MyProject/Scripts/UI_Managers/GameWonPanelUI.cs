using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWonPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardsText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button goBackButton;

    // Start is called before the first frame update
    void Awake()
    {

    }
    void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        goBackButton.onClick.AddListener(OnGoBackButtonClicked);
        // InventoryManager.OnRewardsGiven += UpdateRewardsText;
    }

    public void UpdateRewardsText()
    {
        SetRewardsText(DataCarrier.PlayerCurrency.ToString());
    }

    public void SetRewardsText(string text)
    {
        rewardsText.text = text;
    }

    private void OnContinueButtonClicked()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnGoBackButtonClicked()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    void OnDestroy()
    {
        continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        goBackButton.onClick.RemoveListener(OnGoBackButtonClicked);
        //     InventoryManager.OnRewardsGiven -= UpdateRewardsText;
    }


}
