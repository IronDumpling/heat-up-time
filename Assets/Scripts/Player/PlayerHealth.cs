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
    // Villain Damage
    public GameObject villain;
    // Heating
    private float heatingDamage;
    public float damageBound;
    public float recoverBound;
    // Change Color
    public Gradient gradient;
    public Image fill;
    // Blink Color
    public SpriteRenderer myRenderer;
    public int blinks;
    public float time;

    private HeatInfo hI;

    // Start is called before the first frame update
    private void Awake()
    {
        // Health Bar
        SetMaxHealth(20f);

        // Heating
        heatingDamage = maxHealth / 20;
        damageBound = 0.9f;

        // Pointer
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.enabled = true;

        // Blink
        blinks = 1;
        time = 0.1f;

        hI = GetComponent<HeatInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hI.curHeat < hI.minHeat * damageBound || hI.curHeat > hI.maxHeat * damageBound) {
            ContinousRecover(heatingDamage);
        }

        // Increase Health by picking hearts
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
        curHealth -= decreaseValue * Time.deltaTime;
        SetCurHealth(curHealth);
        BlinkPlayer(blinks, time);

        // Start Again
        if (curHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Method 3. Damage Health Instantly
    public void Damage(float decreaseValue)
    {
        curHealth -= decreaseValue;
        SetCurHealth(curHealth);
        BlinkPlayer(blinks, time);

        // Start Again
        if (curHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

    // Method 6. Blink After Damage
    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for(int i = 0; i < numBlinks * 2; i++)
        {
            myRenderer.enabled = !myRenderer.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRenderer.enabled = true;
    }
}
