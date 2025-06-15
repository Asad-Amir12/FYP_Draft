using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemUI : MonoBehaviour
{

    [SerializeField] private Image itemIcon;
    [SerializeField] private Button itemButton;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;

    public void Initialize(ItemData data, System.Action onClickAction)
    {
        if (data != null)
        {
            itemIcon.sprite = data.icon;
            itemName.text = data.itemName;
            itemPrice.text = data.itemCost.ToString();

        }

        if (itemButton != null)
        {
            itemButton.onClick.RemoveAllListeners();
            itemButton.onClick.AddListener(() => onClickAction?.Invoke());
        }
    }

    public void ClearEventListeners()
    {
        if (itemButton != null)
        {
            itemButton.onClick.RemoveAllListeners();
        }
    }
    public void ClearData()
    {
        if (itemIcon != null)
            itemIcon.sprite = null;
        itemName.text = "";
        itemPrice.text = "";
    }

    void OnDestroy()
    {
        ClearEventListeners();
    }
}
