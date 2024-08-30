using UnityEngine;
using UnityEngine.Events;

public enum BonusType
{
    Protection,
    SpeedBoost,
    FishTraffic
}

public class Bonuses : MonoBehaviour
{
    [SerializeField]
    private Bonus[] bonuses;
    public static Bonuses Instance;

    [Space, Header("Main")]
    public int[] prices;
    public int[] countBonus = new int[3];

    [Space, Header("Bonus Settings")]
    public float timeSpeedBoost = 10;
    public float timeFishTraffic = 10;

    [Space]
    public UnityEvent<BonusType> OnBonusChange;
    public UnityEvent<BonusType> OnBonusUse;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        countBonus = new int[prices.Length];

        for (int i = 0; i < countBonus.Length; i++)
        {
            countBonus[i] = PlayerPrefs.GetInt("Bonus" + (BonusType)i, 0);
        }

        UpdateCount();
    }

    private void UpdateCount()
    {
        for (int i = 0; i < countBonus.Length; i++)
        {
            bonuses[i].count = countBonus[(int)bonuses[i].type];
        }
    }
    private void Visual(BonusType type)
    {
        Bonus bonus = GetBonus(type);
        bonus.SpendAnim();
    }

    private Bonus GetBonus(BonusType type)
    {
        for (int i = 0; i < bonuses.Length; i++)
        {
            if (bonuses[i].type == type)
                return bonuses[i];
        }

        return null;
    }


    public void AddBonus(int id)
    {
        AddBonus((BonusType)id);
    }

    public static void AddBonus(BonusType type)
    {
        if (Money.Instance.Spend(Instance.prices[(int)type]))
        {
            Instance.countBonus[(int)type]++;

            Instance.UpdateCount();
            Instance.OnBonusChange?.Invoke(type);

            PlayerPrefs.SetInt("Bonus" + type, Instance.countBonus[(int)type]);
            Audio.Play(ClipType.buyBonus);
        }
    }

    public static bool Spend(BonusType type)
    {
        if (Instance.countBonus[(int)type] > 0)
        {
            Instance.countBonus[(int)type]--;

            Instance.UpdateCount();
            Instance.Visual(type);
            Instance.OnBonusChange?.Invoke(type);
            Instance.OnBonusUse?.Invoke(type);

            PlayerPrefs.SetInt("Bonus" + type, Instance.countBonus[(int)type]);
            Audio.Play(ClipType.bonus);
            return true;
        }

        return false;
    }
}
