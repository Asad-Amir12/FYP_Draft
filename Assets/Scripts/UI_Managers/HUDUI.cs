using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider StaminaSlider;
    [SerializeField] private TextMeshProUGUI CurrencyText;
    [SerializeField] private List<SlotItem> Consumables;

    void Start()
    {
        PlayerInfo.OnUpdateHealth += UpdateHealth;
        PlayerInfo.OnConsumableUsed += UpdateConsumables;
        HealthSlider.maxValue = DataCarrier.PlayerMaxHealth;
        HealthSlider.value = DataCarrier.PlayerMaxHealth;
        StaminaSlider.maxValue = DataCarrier.PlayerMaxStamina;
        StaminaSlider.value = DataCarrier.PlayerMaxStamina;
        CurrencyText.text = DataCarrier.PlayerCurrency.ToString();
    }
    void UpdateHealth(int health)
    {
        HealthSlider.value = health;

    }
    void UpdateConsumables(int index)
    {
        if (index < 0 || index > Consumables.Count)
        {
            Debug.LogWarning("Invalid consumable index: " + index);
            return;
        }

        SlotItem item = Consumables[index - 1];
        item.itemCountText.text = (int.Parse(item.itemCountText.text) - 1).ToString();
        if (int.Parse(item.itemCountText.text) <= 0)
        {
            Debug.Log("nulled the image");
            item.itemIcon.enabled = false;
            item.itemCountText.transform.parent.gameObject.SetActive(false);
        }
    }
    void OnDestroy()
    {
        PlayerInfo.OnUpdateHealth -= UpdateHealth;
        PlayerInfo.OnConsumableUsed -= UpdateConsumables;
    }

}
