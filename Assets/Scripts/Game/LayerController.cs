using UnityEngine;

// This scripts need for correct layer rendering
public class LayerController : MonoBehaviour
{
    private Transform playerTrans;
    private SpriteRenderer sr;
    [SerializeField] private float offset;

    private void Start()
    {
        // Getting player and checking for no null
        GameObject playerTmp = GameObject.FindGameObjectWithTag("Player");
        if (playerTmp != null) playerTrans = playerTmp.GetComponent<Transform>();
        // Getting components
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Checking for player Y position and changing layer
        bool playerIsUpper = false;

        if (playerTrans != null)
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
