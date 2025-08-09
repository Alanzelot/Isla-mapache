using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 7f;
    public float gravity = -9.81f;

    [Header("Componentes")]
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;

    void Update()
    {
        // Verificar si está en el suelo (usando un pequeño cubo invisible)
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(0.5f, 0.1f, 0.5f), Quaternion.identity, groundMask);

        // Resetear velocidad vertical si está en el suelo
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Pequeña fuerza hacia abajo para asegurar el contacto
        }

        // Correr (Shift izquierdo)
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Movimiento (WASD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Salto (Barra espaciadora)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}