using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI[] textMoney;

    public static Money Instance;

    void Start()
    {
        Instance = this;
        money = PlayerPrefs.GetInt(nameof(money), 0);
    }

    void Update()
    {
        foreach (var money in textMoney)
        {
            money.text = this.money.ToString();
        }
    }

    public bool Spend(int count)
    {
        if (money >= count)
        {
            money -= count;
            PlayerPrefs.SetInt(nameof(money), money);
            return true;
        }

        return false;

    }

    public void Add(int count)
    {
        money += count;
        PlayerPrefs.SetInt(nameof(money), money);
    }
}
