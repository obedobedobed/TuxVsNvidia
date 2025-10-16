using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioSource audStep;
    [SerializeField] private Range audStepPitchRange;
    private bool isRuning = false;
    private Vector2 direction;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Run logic

        direction = InputManager.Instance.actions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Run logic

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

        audStep.pitch = Random.Range(audStepPitchRange.MinimalValue, audStepPitchRange.MaximalValue);

        if (direction != new Vector2(0, 0) && !isRuning)
        {
            isRuning = true;
            AudioManager.PlaySFX(audStep, false);
        }
        else if (direction == new Vector2(0, 0) && isRuning)
        {
            isRuning = false;
            AudioManager.PlaySFX(audStep, true);
        }
    }
}
