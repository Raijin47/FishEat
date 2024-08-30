using UnityEngine;

public class Coin : MonoBehaviour
{
    public int count;

    public GameObject efx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            if (collision.attachedRigidbody.TryGetComponent(out PlayerMovement player))
            {
                Money.Instance.Add(count);
                Audio.Play(ClipType.coin);

                if (efx != null)
                {
                    Instantiate(efx, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }
}
