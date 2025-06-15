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
    [SerializeField] public GameObject TimerObject;

    public static HUDUI Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            this.transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayerInfo.OnUpdateHealth += UpdateHealth;
        PlayerInfo.OnConsumableUsed += UpdateConsumables;
        InventoryManager.OnInventoryUpdated += UpdateCurrency;
        InventoryManager.OnInventoryUpdated += SetUpConsumables;
        EventBus.OnPlayerStatsChanged += UpdateHud;
        HealthSlider.maxValue = DataCarrier.PlayerMaxHealth;
        HealthSlider.value = DataCarrier.PlayerMaxHealth;
        StaminaSlider.maxValue = DataCarrier.PlayerMaxStamina;
        StaminaSlider.value = DataCarrier.PlayerMaxStamina;
        CurrencyText.text = DataCarrier.PlayerCurrency.ToString();
        ResetCount();
    }

    void ResetCount()
    {
        foreach (var item in Consumables)
        {
            item.itemCountText.text = "0";
        }
    }
    void UpdateHealth(int health)
    {
        HealthSlider.value = health;

    }
    void SetUpConsumables()
    {
        ResetCount();
        foreach (InventorySlot item in InventoryManager.Instance.slots)
        {
            if (item.item != null)
            {
                UpdateItemSlot(item.item);
            }
        }
    }
    void UpdateItemSlot(ItemData data)
    {
        for (int i = 0; i < Consumables.Count; i++)
        {

            if (data.itemName == Consumables[i].itemName)
            {
                Consumables[i].itemCountText.text = (int.Parse(Consumables[i].itemCountText.text) + 1).ToString();
                Consumables[i].itemIcon.enabled = true;
                Consumables[i].itemCountText.transform.parent.gameObject.SetActive(true);
            }
        }
    }
    void UpdateConsumables(int index)
    {
        if (index < 0 || index > Consumables.Count)
        {
            Debug.LogWarning("Invalid consumable index: " + index);
            return;
        }

        SlotItem item = Consumables[index - 1];
        if (int.Parse(item.itemCountText.text) <= 0)
        {
            return;
            // Debug.Log("nulled the image");
            // item.itemIcon.enabled = false;
            // item.itemCountText.transform.parent.gameObject.SetActive(false);
        }
        InventoryManager.Instance.RemoveItemByName(Consumables[index - 1].itemName);
        //  item.itemCountText.text = (int.Parse(item.itemCountText.text) - 1).ToString();
    }

    public void ToggleHUD(bool state)
    {
        HealthSlider.gameObject.SetActive(state);
        StaminaSlider.gameObject.SetActive(state);
        TimerObject.SetActive(state);

    }
    public void UpdateCurrency()
    {
        CurrencyText.text = DataCarrier.PlayerCurrency.ToString();
    }

    void OnDestroy()
    {
        PlayerInfo.OnUpdateHealth -= UpdateHealth;
        PlayerInfo.OnConsumableUsed -= UpdateConsumables;
        InventoryManager.OnInventoryUpdated -= UpdateCurrency;

    }
    private void UpdateHud()
    {
        HealthSlider.maxValue = DataCarrier.PlayerMaxHealth;
        HealthSlider.value = DataCarrier.PlayerMaxHealth;
        StaminaSlider.maxValue = DataCarrier.PlayerMaxStamina;
        StaminaSlider.value = DataCarrier.PlayerMaxStamina;
        CurrencyText.text = DataCarrier.PlayerCurrency.ToString();

    }

}
