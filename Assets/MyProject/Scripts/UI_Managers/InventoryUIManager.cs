using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance { get; private set; }
    [SerializeField] private List<InventoryItemUI> inventoryItems;
    [SerializeField] private TextMeshProUGUI displayDescription;
    [SerializeField] private Image displayDescriptionSprite;
    [SerializeField] private Button closeButton;
    [Header("Inventory UI Panel")]
    [SerializeField] private GameObject inventoryPanel;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            this.transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        EventBus.OnInventoryOpened += ShowInventory;
        closeButton.onClick.AddListener(HideInventory);
        //RefreshSlots();
        //InventoryManager.OnInventoryUpdated += RefreshUI;
    }

    void EnableGraphicsRaycaster()
    {
        gameObject.transform.GetChild(0).GetComponent<GraphicRaycaster>().enabled = true;

    }
    void DisableGraphicsRaycaster()
    {
        gameObject.transform.GetChild(0).GetComponent<GraphicRaycaster>().enabled = false;
    }
    void RefreshUI()
    {
        foreach (var itemUI in inventoryItems)
        {
            itemUI.ClearEventListeners();
        }
        RefreshSlots();
    }
    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
        EventBus.TriggerOnInventoryClosed();
        InventoryManager.OnInventoryUpdated -= RefreshUI;
        foreach (var itemUI in inventoryItems)
        {
            itemUI.ClearEventListeners();
        }
        EventBus.GamePaused -= DisableGraphicsRaycaster;
        EventBus.GameResumed -= EnableGraphicsRaycaster;
    }
    public void ShowInventory()
    {
        EventBus.GamePaused += DisableGraphicsRaycaster;
        EventBus.GameResumed += EnableGraphicsRaycaster;
        inventoryPanel.SetActive(true);
        InventoryManager.OnInventoryUpdated += RefreshUI;
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



    void OnDestroy()
    {
        InventoryManager.OnInventoryUpdated -= RefreshUI;
        foreach (var itemUI in inventoryItems)
        {
            itemUI.ClearEventListeners();
        }
        closeButton.onClick.RemoveAllListeners();
    }


}
