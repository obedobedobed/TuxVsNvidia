using UnityEngine;
using UnityEngine.InputSystem;

public class ShotgunController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float coolDown;
    [SerializeField] private Range bulletsRange;
    [SerializeField] public int bulletsCount;
    [SerializeField] private AudioSource audShot;
    private float originalCoolDown;
    private bool looksRight = true;
    private Animator anim;
    private GameController gameController;

    // For anim
    [SerializeField] private bool isKnockbacking = false;
    [SerializeField] private bool knockbackRight = false;

    private void OnEnable()
    {
        InputManager.Instance.actions.Player.Attack.performed += Shoot;
    }

    private void OnDisable()
    {
        InputManager.Instance.actions.Player.Attack.performed -= Shoot;
    }

    private void Start()
    {
        // Getting components
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        anim = GetComponent<Animator>();
        originalCoolDown = coolDown;

        // Sigma mode
        if (GlobalVariables.SigmaMode)
        {
            coolDown /= 3;
            originalCoolDown /= 3;
            bulletsCount *= 3;
        }
    }

    private void Update()
    {
        // Folowing a cursor
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint
        (
            new Vector3
            (
                InputManager.Instance.actions.Player.CursorPos.ReadValue<Vector2>().x,
                InputManager.Instance.actions.Player.CursorPos.ReadValue<Vector2>().y,
                Camera.main.transform.position.z * -1
            )
        );

        if (!isKnockbacking && !gameController.gamePaused)
        {
            Vector2 direction = cursorPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Fliping
            if (cursorPos.x < transform.position.x && looksRight)
            {
                Flip();
            }
            else if (cursorPos.x > transform.position.x && !looksRight)
            {
                Flip();
            }
        }

        // Cooldown
        if (coolDown < originalCoolDown)
        {
            coolDown += Time.deltaTime;
        }

        // Animation (variables changes from unity animator)
        if (isKnockbacking)
        {
            if (!knockbackRight)
            {
                transform.Translate(Vector2.left * Time.deltaTime * 0.1f);
            }
            else if (knockbackRight)
            {
                transform.Translate(Vector2.right * Time.deltaTime * 0.1f);
            }
        }
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        if (coolDown >= originalCoolDown && !gameController.gamePaused)
        {
            // Getting shotpoint
            Transform shotPoint = GameObject.FindGameObjectWithTag("ShotPoint").GetComponent<Transform>();

            // Shooting bulletsCount bullets
            for (int i = 0; i < bulletsCount; i++)
            {
                Quaternion targetRot = Quaternion.Euler
                (
                    transform.localEulerAngles.x,
                    transform.localEulerAngles.y,
                    transform.localEulerAngles.z + Random.Range(bulletsRange.MinimalValue, bulletsRange.MaximalValue)
                );

                Instantiate(bulletPrefab, shotPoint.position, targetRot);
            }

            // Cooldown
            coolDown = 0;
            // Animations
            anim.SetTrigger("Shoot");
            // Sounds
            AudioManager.PlaySFX(audShot);
        }
    }

    private void Flip()
    {
        looksRight = !looksRight;
        transform.localScale = new Vector2
        (
            transform.localScale.x,
            transform.localScale.y * -1
        );
    }
}