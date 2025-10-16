using UnityEngine;
using UnityEngine.InputSystem;

public class ShotgunController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float coolDown;
    [SerializeField] private Range bulletsRange;
    [SerializeField] private int bulletsCount;
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
        anim = GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        originalCoolDown = coolDown;
    }

    private void Update()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint
        (
            new Vector3
            (
                InputManager.Instance.actions.Player.CursorPos.ReadValue<Vector2>().x,
                InputManager.Instance.actions.Player.CursorPos.ReadValue<Vector2>().y,
                Camera.main.transform.position.z * -1
            )
        );

        if(!isKnockbacking && !gameController.gamePaused)
        {
            Vector2 direction = cursorPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            if (cursorPos.x < transform.position.x && looksRight)
            {
                Flip();
            }
            else if (cursorPos.x > transform.position.x && !looksRight)
            {
                Flip();
            }
        }


        if (coolDown < originalCoolDown)
        {
            coolDown += Time.deltaTime;
        }

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
            Transform shotPoint = GameObject.FindGameObjectWithTag("ShotPoint").GetComponent<Transform>();

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

            coolDown = 0;

            anim.SetTrigger("Shoot");

            AudioManager.PlaySFX(audShot, false);
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