using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public LineRenderer laserLine;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 aimDirection;
    private bool isAimingLocked = false;

    public float bulletSpeed = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleAimingInput();
        HandleActionInput();

        UpdateLaser();
    }

    void FixedUpdate()
    {
        
        if (!isAimingLocked)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Detenemos al personaje
        }
    }

   
    void HandleMovementInput()
    {
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        
       
        moveInput.x = Mathf.Round(horizontalInput);
        moveInput.y = Mathf.Round(verticalInput);

      
        moveInput.Normalize();
    }

  
    void HandleAimingInput()
    {
        
        Vector2 rightStickInput =
            new Vector2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"));
        if (rightStickInput.sqrMagnitude > 0.1f) // Comprobamos si el stick se está moviendo
        {
            aimDirection = rightStickInput.normalized;
        }

      
        if (aimDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

 
    void HandleActionInput()
    {
    
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown("joystick button 4"))
        {
            isAimingLocked = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp("joystick button 4"))
        {
            isAimingLocked = false;
        }

        // Botón para disparar (RB en mando de Xbox es 'joystick button 5')
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("joystick button 5"))
        {
            Shoot();
        }
    }

    void UpdateLaser()
    {
        if (aimDirection != Vector2.zero)
        {
            laserLine.SetPosition(0, firePoint.position);
            laserLine.SetPosition(1, firePoint.position + (Vector3)aimDirection * 15f);
        }
    }

    void Shoot()
    {
       
        
            if (bulletPrefab != null && firePoint != null)
            {
                // crear la bala en la posición y rotación del punto de disparo.
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                // Aplica la velocidad.
                
                if (bulletRb != null)
                {
                    bulletRb.linearVelocity = firePoint.right * bulletSpeed;
                }
            }
        
    }
}