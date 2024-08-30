using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private float timeDestroy = 7;

    public bool flipX;

    void Start()
    {
        renderer.flipX = flipX;
        Destroy(gameObject, timeDestroy);
    }
}
