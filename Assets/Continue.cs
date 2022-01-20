using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    Canvas thisCanvas;

    private void Start()
    {
        thisCanvas = GetComponent<Canvas>();
    }

    public void ContinueButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        thisCanvas.gameObject.SetActive(false);
    }
}
