using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioSource audStep;
    [SerializeField] private Vector2 audStepPitchRange;
    private bool isRuning = false;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 cameraTargetPos = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = cameraTargetPos;
    }

    private void FixedUpdate()
    {
        // Run logic

        Vector2 direction = InputManager.Instance.actions.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = direction * speed;

        // Animations

        if (direction != new Vector2(0, 0))
        {
            anim.SetBool("IsRun", true);
        }
        else
        {
            anim.SetBool("IsRun", false);
        }

        // Sounds

        audStep.pitch = Random.Range(audStepPitchRange.x, audStepPitchRange.y);

        if (direction != new Vector2(0, 0) && !isRuning)
        {
            isRuning = true;
            AudioManager.PlaySFX(ref audStep, false);
        }
        else if (direction == new Vector2(0, 0) && isRuning)
        {
            isRuning = false;
            AudioManager.PlaySFX(ref audStep, true);
        }
    }
}
