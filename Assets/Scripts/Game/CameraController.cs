using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform player;

    private void Start()
    {
        GameObject playerTmp = GameObject.FindGameObjectWithTag("Player");
        if(playerTmp != null) player = playerTmp.GetComponent<Transform>();
    }
    
    private void Update()
    {
        if(player != null)
        {
            Vector3 targetPos = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed);
            targetPos.z = -10;
            transform.position = targetPos;
        }
    }
}
