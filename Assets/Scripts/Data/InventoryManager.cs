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
    [SerializeField] private ItemData testSO; // For testing purposes
    public static event Action OnInventoryUpdated;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            slots = new List<InventorySlot>(slotCount);
            AddDummyData(); // For testing purposes
            // for (int i = 0; i < slotCount; i++)
            //     slots.Add(new InventorySlot(null, 0));
        }
        else Destroy(gameObject);
    }
    public void AddDummyData()
    {
        for (int i = 0; i < slotCount; i++)
            slots.Add(new InventorySlot(testSO, 1));
    }
    // Try to add an item; returns true if successful
    public bool AddItem(ItemData item, int amount = 1)
    {
        // 1) If stackable, try to find existing slot
        if (item.stackable)
        {
            foreach (var slot in slots)
            {
                if (slot.item == item && slot.CanAdd(amount))
                {
                    slot.quantity += amount;
                    OnInventoryUpdated?.Invoke();
                    return true;
                }
            }
        }
        // 2) Otherwise find an empty slot
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.quantity = amount;
                OnInventoryUpdated?.Invoke();
                return true;
            }
        }
        // No room
        Debug.LogWarning("Inventory full!");
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
                    OnInventoryUpdated?.Invoke();
                    return true;
                }
            }
        }
        Debug.LogWarning("Not enough items to remove!");
        return false;
    }
}
