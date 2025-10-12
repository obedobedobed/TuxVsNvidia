using UnityEngine;

public class LayerController : MonoBehaviour
{
    private Transform playertTrans;
    private SpriteRenderer sr;
    [SerializeField] private float offset;

    private void Start()
    {
        playertTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        bool playerIsUpper = playertTrans.position.y + offset > transform.position.y;

        if (playerIsUpper)
        {
            sr.sortingLayerName = "UpperPlayer";
        }
        else if (!playerIsUpper)
        {
            sr.sortingLayerName = "UnderPlayer";
        }
    }
}
