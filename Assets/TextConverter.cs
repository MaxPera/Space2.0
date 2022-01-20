using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextConverter : MonoBehaviour
{
    Text inputText;
    public PlayerLevelUp playerLevelUp;

    [SerializeField] bool isHealth;
    [SerializeField] bool isEXP;
    // Start is called before the first frame update
    void Start()
    {
        inputText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHealth == true)
            inputText.text = playerLevelUp.health.ToString("0");
        if (isEXP == true)
            inputText.text = playerLevelUp.playerExp.ToString("0");
    }
}
