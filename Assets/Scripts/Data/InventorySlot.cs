// Assets/Scripts/Inventory/InventorySlot.cs
[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public InventorySlot(ItemData item, int qty)
    {
        this.item = item;
        this.quantity = qty;
    }

    public bool CanAdd(int amount = 1)
    {
        if (item == null) return true;
        if (!item.stackable) return false;
        return quantity + amount <= item.maxStack;
    }
}
