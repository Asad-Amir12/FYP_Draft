using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private List<InventoryItemUI> inventoryItems;
    [SerializeField] private TextMeshProUGUI displayDescription;
    [SerializeField] private Image displayDescriptionSprite;
    // Start is called before the first frame update
    void Start()
    {
        //RefreshSlots();
        //InventoryManager.OnInventoryUpdated += RefreshUI;
    }
    void RefreshUI()
    {
        foreach (var itemUI in inventoryItems)
        {
            itemUI.ClearEventListeners();
        }
        RefreshSlots();
    }

    void RefreshSlots()
    {
        var slots = InventoryManager.Instance.slots;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            var ui = inventoryItems[i];
            if (i < slots.Count && slots[i].item != null)
            {
                var slotData = slots[i];
                ui.Initialize(slotData.item.icon,
                    () =>
                    {
                        displayDescriptionSprite.sprite = slotData.item.icon;
                        displayDescription.text = slotData.item.itemDescription;
                        Debug.Log("Item clicked: " + slotData.item.name);
                    }
                );
            }
            else
            {
                // No item here – fully clear the UI slot
                ui.ClearEventListeners();
                ui.ClearIcon();        // implement this to hide the icon
            }
        }
    }

    void OnEnable()
    {

        InventoryManager.OnInventoryUpdated += RefreshUI;
        RefreshSlots();
    }
    void OnDisable()
    {
        InventoryManager.OnInventoryUpdated -= RefreshUI;
        foreach (var itemUI in inventoryItems)
        {
            itemUI.ClearEventListeners();
        }
    }

    void OnDestroy()
    {
        InventoryManager.OnInventoryUpdated -= RefreshUI;
        foreach (var itemUI in inventoryItems)
        {
            itemUI.ClearEventListeners();
        }
    }


}
