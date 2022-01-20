using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Transform playerCam;
    public Transform orientation;

    public Transform Lamp;



    private Rigidbody rb;
    private CapsuleCollider capc;
    private Transform trPlayer;
    private PlayerLevelUp playerLevelUp;
    //Move
    public float xSpeed = 2000;
    public float xSpeedMax = 15;
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;

    public float x, y;

    public float ySpeed;
    public float multiplier = 1f;
    public float slideMultiplier = 1f;

    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    private float jumpCooldown = 0.25f;
    public float jumpForce = 100;
    public float slideForce = 50;

    public bool isGrounded = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capc = GetComponent<CapsuleCollider>();
        trPlayer = GetComponent<Transform>();
        playerLevelUp = GetComponent<PlayerLevelUp>();
    }
    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }


    private void FixedUpdate()
    {
        Moving();
    }

    // Update is called once per frame


    void Update()
    {

        MyInput();
        Look();

    }

    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) Jump();


    }

    private void Moving()
    {

        Vector2 mag = FindVelLook();
        float xmag = mag.x, ymag = mag.y;

        CounterMoving(x, y, mag);

        float maxSpeed = this.xSpeedMax;

        if (x > 0 && xmag > maxSpeed) x = 0;
        if (x < 0 && xmag < -maxSpeed) x = 0;
        if (y > 0 && ymag > maxSpeed) y = 0;
        if (y < 0 && ymag < -maxSpeed) y = 0;

        rb.AddForce(orientation.transform.forward * y * xSpeed * Time.deltaTime * multiplier);
        rb.AddForce(orientation.transform.right * x * xSpeed * Time.deltaTime * multiplier);
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;


        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);


        Lamp.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
    }



    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce * 1.5f);
        rb.AddForce(Vector3.up * jumpForce * 0.5f);
    }

    public Vector2 FindVelLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitude = rb.velocity.magnitude;
        float ymag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
        float xmag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xmag, ymag);
    }

    private void CounterMoving(float x, float y, Vector2 mag)
    {
        if (Mathf.Abs(mag.x) > threshold && Mathf.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(xSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Mathf.Abs(mag.y) > threshold && Mathf.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(xSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

    }
    private void OnCollisionEnter(Collision other)
    {


        isGrounded = true;



    }
    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }

    

}


