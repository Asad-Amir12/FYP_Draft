// Assets/Scripts/Inventory/InventoryManager.cs
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;

    [Header("Inventory Settings")]
    public int slotCount = 16;
    public List<InventorySlot> slots;
    [Header("Sellable Items Data")]
    [SerializeField] private SellableItems sellableItems;

    [Header("Owned Items Data")]
    [SerializeField] private ItemData testSO; // For testing purposes
    [SerializeField] private int OwnedCurrency;
    [SerializeField] private int baseReward = 50;
    public static event Action OnInventoryUpdated;
    public static event Action OnRewardsGiven;
    public int LastReward = 0;
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            this.transform.parent = null;
            DontDestroyOnLoad(gameObject);

            // AddDummyData(); // For testing purposes

        }
        else Destroy(gameObject);
        DataCarrier.PlayerCurrency = OwnedCurrency;
    }
    void Start()
    {
        EventBus.OnLevelCleared += GiveRewards;
        slots = new List<InventorySlot>(slotCount);
        for (int i = 0; i < slotCount; i++)
            slots.Add(new InventorySlot(null, 0));
    }
    public void AddDummyData()
    {
        for (int i = 0; i < slotCount; i++)
            slots.Add(new InventorySlot(testSO, 1));
    }
    // Try to add an item; returns true if successful
    public bool AddItem(ItemData item, int amount = 1)
    {
        // // 1) If stackable, try to find existing slot
        // if (item.stackable)
        // {
        //     foreach (var slot in slots)
        //     {
        //         if (slot.item == item && slot.CanAdd(amount))
        //         {
        //             slot.quantity += amount;
        //             OnInventoryUpdated?.Invoke();
        //             return true;
        //         }
        //     }
        // }
        // 2) Otherwise find an empty slot
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.quantity = amount;

                return true;
            }
        }
        // No room
        Debug.LogWarning("Inventory full!");
        return false;
    }
    void GiveRewards()
    {
        int reward = baseReward + (100 * DataCarrier.SelectedLevelIndex);
        OwnedCurrency += reward;
        DataCarrier.PlayerCurrency = OwnedCurrency;
        LastReward = reward;
        OnRewardsGiven?.Invoke();
        // GameWonPanelUI UI = FindObjectOfType<GameWonPanelUI>();
        // UI.SetRewardsText(reward.ToString());
        Debug.Log("DataCarrier : " + DataCarrier.PlayerCurrency);
        Debug.Log("InventoryManager : " + OwnedCurrency);
    }
    public void ResetCurrency()
    {
        OwnedCurrency = 0;

    }
    public bool OnItemBought(ItemData data)
    {
        if (data.itemCost > OwnedCurrency) return false;
        OwnedCurrency -= data.itemCost;
        DataCarrier.PlayerCurrency = OwnedCurrency;
        bool success = AddItem(data);
        if (success)
        {

            OnInventoryUpdated?.Invoke();
        }
        return success;
    }
    public bool RemoveItemByName(string name)
    {
        foreach (var slot in slots)
        {

            if (slot.item != null && slot.item.itemName == name)
            {

                bool success = RemoveItem(slot.item);
                if (success) OnInventoryUpdated?.Invoke();
                return success;
            }
        }
        return false;
    }
    public bool RemoveItem(ItemData item, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                if (slot.quantity >= amount)
                {
                    slot.quantity -= amount;
                    if (slot.quantity == 0)
                        slot.item = null;

                    return true;
                }
            }
        }
        Debug.LogWarning("Not enough items to remove!");
        return false;
    }

    public SellableItems GetSellableItems()
    {
        if (sellableItems == null)
            throw new Exception("NO items to sell :(");
        return sellableItems;
    }
    void OnDestroy()
    {
        EventBus.OnLevelCleared -= GiveRewards;
    }
}
