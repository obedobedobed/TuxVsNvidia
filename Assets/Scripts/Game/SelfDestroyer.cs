using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    private bool destroyAnimating;
    private SpriteRenderer[] sprites;

    public void SelfDestroy(bool animate = false)
    {
        if (animate == true) destroyAnimating = true;
        else Destroy(gameObject);
    }

    private void Start()
    {
        sprites = new SpriteRenderer[transform.childCount + 1];
        sprites[0] = GetComponent<SpriteRenderer>();

        for(int i = 1; i < transform.childCount; i++)
        {
            sprites[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (destroyAnimating)
        {
            for(int i = 0; i < transform.childCount + 1; i++)
            {
                Color color = sprites[i].color;
                color.a -= Time.deltaTime * 0.5f;
                sprites[i].color = color;
                if (color.a <= 0) Destroy(gameObject);
            }
        }
    }
}
