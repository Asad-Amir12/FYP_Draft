using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MerchantUI : MonoBehaviour
{
    [SerializeField] private List<ShopItemUI> ShopItemSlots;

    [Header("Description Tab Data")]
    [SerializeField] private Image focusedItemIcon;
    [SerializeField] private TextMeshProUGUI focusedItemDescription;
    [SerializeField] private TextMeshProUGUI focusedItemTitle;
    [SerializeField] private TextMeshProUGUI focusedItemPrice;
    [SerializeField] private Button buyButton;
    private SellableItems itemsToSell;
    private ItemData focusedItem;

    // Start is called before the first frame update
    void Start()
    {
        itemsToSell = InventoryManager.Instance.GetSellableItems();

        for (int i = 0; i < ShopItemSlots.Count; i++)
        {
            var data = itemsToSell.sellableItems[i];
            ShopItemSlots[i].Initialize(data, () => OnItemFocused(data));
        }
        buyButton.onClick.AddListener(OnBuyItem);
    }


    void OnItemFocused(ItemData data)
    {
        focusedItem = data;
        focusedItemIcon.sprite = data.icon;
        focusedItemDescription.text = data.itemDescription;
        focusedItemTitle.text = data.itemName;
        focusedItemPrice.text = data.itemCost.ToString();

    }

    void OnBuyItem()
    {
        InventoryManager.Instance.OnItemBought(focusedItem);
    }


}
