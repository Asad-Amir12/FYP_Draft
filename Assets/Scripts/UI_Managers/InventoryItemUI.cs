using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button itemButton;

    public void Initialize(Sprite icon, System.Action onClickAction)
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = icon;
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
    public void ClearIcon()
    {
        if (itemIcon != null)
            itemIcon.sprite = null;
    }
}
