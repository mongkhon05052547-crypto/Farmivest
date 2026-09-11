using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float rotationSpeed = 10f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1.5f;
    public float gravity = -19.62f;

    private CharacterController controller;
    private Vector3 velocity;
    private Transform mainCamera;
    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        // ดึงคอมโพเนนต์ Animator
        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();

        if (Camera.main != null) mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        // รีเซ็ตแรงโน้มถ่วงเมื่ออยู่บนพื้น
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // รับค่าปุ่มเดิน WASD
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float animSpeed = 0f;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + (mainCamera != null ? mainCamera.eulerAngles.y : 0f);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            float speed = isSprinting ? sprintSpeed : moveSpeed;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed * Time.deltaTime));

            // กำหนดความเร็วส่งไปให้ Animator (เดิน = 1, วิ่ง = 2)
            animSpeed = isSprinting ? 2f : 1f;
        }

        // ส่งค่าไปที่ Parameter ชื่อ Speed ใน Animator
        if (anim != null)
        {
            anim.SetFloat("Speed", animSpeed, 0.1f, Time.deltaTime);
        }

        // กระโดด
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // แรงโน้มถ่วง
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}