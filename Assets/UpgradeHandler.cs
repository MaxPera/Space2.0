using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField]PlayerLevelUp playerLevelUp;
    public void UpgradeDamage()
    {
        playerLevelUp.damageBoost += .1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    public void Upgraderange()
    {
        playerLevelUp.rangeBoost += .1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);

       
    }

    public void UpgradeHealth()
    {
        playerLevelUp.healthBoost += .1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}
