using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image barImg;
    

    private void Awake()
    {
        barImg = GetComponent<Image>();
        barImg.fillAmount = 1f;
    }
}
