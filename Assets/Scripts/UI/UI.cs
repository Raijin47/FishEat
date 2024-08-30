using UnityEngine;
using UnityEngine.Events;

public class UI : MonoBehaviour
{
    public static UI Instance;
    public GameObject[] pages;
    public int id;

    public UnityEvent<int> OnChangePage;

    private void Awake()
    {
        Application.targetFrameRate = 0;
        Instance = this;
    }

    void Start()
    {
        ChangePage(id);
    }

    public void SetPage(int id)
    {
        ChangePage(id);
        Audio.Play(0);
    }

    private void ChangePage(int id)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == id);
        }

        OnChangePage?.Invoke(id);
    }

    public void SetOveridePage(int id)
    {
        pages[id].SetActive(true);
        OnChangePage?.Invoke(id);
    }
}
