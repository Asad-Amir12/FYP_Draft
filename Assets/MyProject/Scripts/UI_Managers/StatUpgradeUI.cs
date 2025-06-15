using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class StatUpgradeUI : MonoBehaviour
{
    [SerializeField] private List<StatItemUI> statItemUIs;
    [SerializeField] private StatData playerStats;

    [Header("Description Panel")]
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI descriptionTitle;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI costText;
    private int totalStatsInCart = 0;
    private int totalCost = 0;

    void Start()
    {
        RefreshAllStats();
        foreach (var uiItem in statItemUIs)
        {
            var item = uiItem;
            item.increaseCountButton.onClick.AddListener(() => IncreaseCount(item.statType));
            item.decreaseCountButton.onClick.AddListener(() => DecreaseCount(item.statType));
        }
        buyButton.onClick.AddListener(ProcessUpgrade);
    }
    void IncreaseCount(StatType type)
    {
        foreach (var item in statItemUIs)
        {
            if (item.statType == type)
            {
                item.statValueText.text = (int.Parse(item.statValueText.text) + 1).ToString();
                item.value++;
                totalStatsInCart++;
                FocusOnItem(item);
            }
        }
        RefreshAllStats();
    }
    void DecreaseCount(StatType type)
    {
        foreach (var item in statItemUIs)
        {
            if (item.statType == type)
            {
                if (item.value <= 0) return;  // no negative queues
                item.statValueText.text = (int.Parse(item.statValueText.text) - 1).ToString();
                item.value--;
                totalStatsInCart--;
                FocusOnItem(item);
            }
        }
        RefreshAllStats();
    }

    void FocusOnItem(StatItemUI item)
    {
        descriptionText.text = item.statDescriptionText;
        descriptionTitle.text = item.statType.ToString();


    }
    void RefreshAllStats()
    {
        totalCost = 0;
        int cost = playerStats.GetCost(StatType.ATTACK);
        foreach (var item in statItemUIs)
        {
            int val = playerStats.GetValue(item.statType);


            item.statDescriptionText = $"Improves your {item.statType.ToString().ToLower()}";
            //item.statValueText.text = (int.Parse(item.statValueText.text)  1).ToString();
            // item.statCostText.text = $"Cost: {cost}";
        }
        totalCost = cost * totalStatsInCart;
        costText.text = totalCost.ToString();
    }

    public void ProcessUpgrade()
    {
        if (OnUpgradePressed())
        {

            EventBus.TriggerOnPlayerStatsChanged();
        }
    }
    public bool OnUpgradePressed()
    {


        if (DataCarrier.PlayerCurrency >= totalCost)
        {
            DataCarrier.PlayerCurrency -= totalCost;
            foreach (var item in statItemUIs)
            {
                switch (item.statType)
                {
                    case StatType.ATTACK:
                        DataCarrier.PlayerAttack += item.value;
                        EventBus.TriggerOnPlayerAttackDataChanged();
                        break;
                    case StatType.DEFENSE:
                        DataCarrier.PlayerDefense += item.value;
                        break;
                    case StatType.HEALTH:
                        DataCarrier.PlayerMaxHealth += item.value;
                        break;
                    case StatType.STAMINA:
                        DataCarrier.PlayerMaxStamina += item.value;
                        break;
                }
            }
            //playerStats.Increase(type);
            RefreshAllStats();
            return true;
        }
        else
        {
            Debug.Log("Not enough money, Bro!");
            return false;
        }
    }


}
