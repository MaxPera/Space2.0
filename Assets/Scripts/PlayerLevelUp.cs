using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLevelUp : MonoBehaviour
{
    public int level = 1;
    public int currentLevel;
    public float levelUpExp = 300;
    public float playerExp = 0;
    public int OnLevelMultiplier = 3;

    public float maxHealth = 50;
    public float health;
    public float healthMultiplier = 1.25f;
    

    public float damageBoost = 1;
    public float healthBoost = 1;
    public float rangeBoost = 1;

    bool turnOffCanvas = false;
    public bool hasKey = false;
    

    public Canvas UpgradesCanvas;
    public Canvas DeathScreen;

    private void Awake()
    {
        currentLevel = level;
        health = maxHealth;
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        LevelUp();
    }

    void LevelUp()
    {
        if (playerExp >= levelUpExp && currentLevel <= level)
        {
            levelUpExp *= OnLevelMultiplier;
            level++;
            maxHealth *= healthMultiplier;
            health = maxHealth;
            UpgradesCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        currentLevel = level;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <=0)
        {
            Die();
        }
    }



    public void Die()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        DeathScreen.gameObject.SetActive(true);
    }

    
    

}
