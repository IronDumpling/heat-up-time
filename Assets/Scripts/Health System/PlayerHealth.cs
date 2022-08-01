using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Health Bar
    public float curHealth;
    public float maxHealth;
    public Slider healthBar;
    // Falling
    public float fallingDamage;
    // Villain Damage
    public GameObject villain;
    // Heating
    private float heatingDamage;
    public float damageBound;
    public float recoverBound;
    // Change Color
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    private void Awake()
    {
        // Health Bar
        SetMaxHealth(20);

        // Falling
        fallingDamage = maxHealth / 5;

        // Heating
        heatingDamage = maxHealth / 20;
        damageBound = 0.9f;
        recoverBound = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease Health by heating
        if (this.GetComponent<PlayerHeat>().curHeat >=
            this.GetComponent<PlayerHeat>().boundHeat * damageBound)
        {
            ContinousDamage(heatingDamage);
        }

        // Increse Health by cooling
        else if (this.GetComponent<PlayerHeat>().curHeat <=
                 this.GetComponent<PlayerHeat>().boundHeat * recoverBound)
        {
            ContinousRecover(heatingDamage);
        }

        // Increase Health by picking hearts

        // Start Again
        if (curHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Method 0. Set Max Health
    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        curHealth = maxHealth;
        healthBar.value = curHealth;
        healthBar.maxValue = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    // Method 1. Set Current Heat
    public void SetCurHealth(float health)
    {
        healthBar.value = health;
        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }

    // Method 2. Damage Health Continously
    void ContinousDamage(float decreaseValue)
    {
        Debug.Log("In continuous decrease");

        curHealth -= decreaseValue * Time.deltaTime;
        SetCurHealth(curHealth);
    }

    // Method 3. Damage Health Instantly
    public void Damage(float decreaseValue)
    {
        curHealth -= decreaseValue;
        SetCurHealth(curHealth);
    }

    // Method 4. Recover Continously
    void ContinousRecover(float decreaseValue)
    {
        if (curHealth <= maxHealth)
        {
            curHealth += decreaseValue * Time.deltaTime;
        }

        SetCurHealth(curHealth);
    }

    // Method 5. Recover Continously
    void Recover(float decreaseValue)
    {
        if (curHealth <= maxHealth)
        {
            curHealth += decreaseValue;
        }

        SetCurHealth(curHealth);
    }
}
