using UnityEngine;

public class LayerController : MonoBehaviour
{
    private Transform playerTrans;
    private SpriteRenderer sr;
    [SerializeField] private float offset;

    private void Start()
    {
        GameObject playerTmp = GameObject.FindGameObjectWithTag("Player");
        if(playerTmp != null)
        {
            playerTrans = playerTmp.GetComponent<Transform>();
        }
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        bool playerIsUpper = false;

        if(playerTrans != null)
        {
            playerIsUpper = playerTrans.position.y + offset > transform.position.y;
        }

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
