using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSellableItem", menuName = "Inventory/Sellable Item Data")]
public class SellableItems : ScriptableObject
{
    public List<ItemData> sellableItems;
}
