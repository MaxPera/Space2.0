using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBoost : MonoBehaviour
{
    public Movement movement;
    public Transform pTransform;
    Transform jTransform;
    public Canvas canvas;

    private void Start()
    {
        jTransform = GetComponent<Transform>();
    }
    public void Update()
    {
        if(Vector3.Distance(jTransform.position,pTransform.position) <= 2)
        {
            pickedUp();
        }
    }

    public void pickedUp()
    {
        movement.jumpForce = 450;
        movement.xSpeedMax = 10;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        canvas.gameObject.SetActive(true);

        Destroy(gameObject);
    }

    
}
