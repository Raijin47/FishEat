using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
   // public Animator animator;
    public TMP_Text text;
    public BonusType type;

    public int count
    {
        get => _count;
        set
        {
            _count = value;
            text.text = _count.ToString();
        }
    }

    private int _count;

    public void Use()
    {
        Bonuses.Spend(type);
    }

    internal void SpendAnim()
    {
        //animator.SetTrigger("Spend");
    }
}
