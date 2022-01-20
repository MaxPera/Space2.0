using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public Transform pTransform;
    Transform sTransform;
    public PlayerLevelUp playerLevelUp;
    public Canvas victoryScreen;
    // Start is called before the first frame update
    void Start()
    {
        sTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(sTransform.position, pTransform.position) <= 3 && playerLevelUp.hasKey == true)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            victoryScreen.gameObject.SetActive(true);
        }
    }
}
