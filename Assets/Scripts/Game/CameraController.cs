using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        Vector3 targetPos = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed);
        targetPos.z = -10;
        transform.position = targetPos;
    }
}
