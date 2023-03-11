using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.Netcode;
#if MULTIPLAYER_TOOLS
using Unity.Multiplayer.Tools;
#endif
using Unity.Profiling;
using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using TMPro;


public class FirstPersonController : NetworkBehaviour
{
    <summary>
    //NIGGHDFDFHJAGFGH
    </summary>
    
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpHeight = 2f;
    [SerializeField] public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Transform cameraTransform;

    private float verticalRotation = 0f;
    private float verticalVelocity = 0f;

    public Animator anim;

    public TMP_Text fpsText;
    public float deltaTime;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }

    

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Deadly")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    private void Update()
    {


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * horizontalInput;
        Vector3 moveVertical = transform.forward * verticalInput;
        Vector3 moveDirection = (moveHorizontal + moveVertical).normalized * moveSpeed;

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        moveDirection.y = verticalVelocity;

        controller.Move(moveDirection * Time.deltaTime);

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("trigger", true);

        }
        else
        {
            anim.SetBool("trigger", false);
        }

        if (transform.position.y < -150)
        {
            Respawn();
        }

        void Respawn()
        {
            SceneManager.LoadScene("Main Game");
        }

    }
}
