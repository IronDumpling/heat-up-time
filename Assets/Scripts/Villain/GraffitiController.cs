using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraffitiController : MonoBehaviour
{
    public const int PLAYER = 3;
    public const int VILLAINS = 7;
    public const int BULLETS = 8;
    public const int PLATFORMS = 9;
    
    // Component pointers
    protected GameObject collideObj;
    protected List<GameObject> collideObjs;

    // Health System
    public float maxHealth { get; set; }
    public float curHealth { get; set; }
    public Slider healthBar;
    public Gradient healthBarGradient;
    public Image fill;
    protected Canvas myHealthBar;

    // Heating System
    public float heatingDamage;
    public float heatDamageBound;
    [SerializeField]
    public float curHeat;
    public float lowerHeatBound { get; set; }
    public float upperHeatBound { get; set; }

    // Color Change
    [SerializeField] private SpriteRenderer render;
    public Gradient gradient;

    // Falling
    public int fallingBound;

    // Moving
    protected Vector3[] moveRanges = new Vector3[2];
    protected int moveIndex = 0;
    protected bool notJumped;

    // Attack
    public float damage;
    public float speed;

    // Detect Player
    public float radius;
    protected GameObject target;
    public LayerMask detectorLayer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Health 
        maxHealth = 10;
        curHealth = maxHealth;
        healthBar.value = curHealth;
        healthBar.maxValue = maxHealth;
        fill.color = healthBarGradient.Evaluate(1f);
        myHealthBar = transform.Find("WorldSpaceUI").gameObject.GetComponent<Canvas>();
        // Pointer
        render = GetComponent<SpriteRenderer>();
        collideObjs = new List<GameObject>();
        // Heat
        heatingDamage = maxHealth/10;
        heatDamageBound = 0.8f;
        // Falling
        fallingBound = -20;
        // Jumping Flag
        notJumped = true;
        // Attack
        speed = 1f; // Kind of Slow
        damage = 1;
        // Detect Player
        radius = 3f;
        StartCoroutine(DetectionCoroutine());
    }

    // Update per frame
    protected virtual void Update()
    {
        // Heat/Cooling Health Damage
        if (curHeat >= upperHeatBound * heatDamageBound // 150 * 0.8 ~ 150
            && curHeat <= lowerHeatBound * heatDamageBound) // -150 ~ -150 * 0.8
        {
            ContinousDamage(heatingDamage);
        }

        // Die Conditions
        if (transform.position.y < fallingBound)
        {
            Die();
        }
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    public void OnCollisionExit2D(Collision2D collision){
        collideObjs.Remove(collision.gameObject);
    }

    // Method 2. Damage Health
    public void Damage(float decreaseValue)
    {
        curHealth -= decreaseValue;
        healthBar.value = curHealth;
        fill.color = healthBarGradient.Evaluate(healthBar.normalizedValue);
        myHealthBar.enabled = true;

        if (curHealth <= 0)
        {
            Die();
        }
    }

    // Method 3. Damage Health Continously
    void ContinousDamage(float decreaseValue)
    {
        curHealth -= decreaseValue * Time.deltaTime;
        healthBar.value = curHealth;
        fill.color = healthBarGradient.Evaluate(healthBar.normalizedValue);
        myHealthBar.enabled = true;

        if (curHealth <= 0)
        {
            Die();
        }
    }

    // Method 4. Die
    void Die()
    {
        Destroy(this.gameObject);
    }


    // Method 8.
    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    // Method 9.
    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)transform.position, radius, detectorLayer);

        if (collider != null)
        {
            target = collider.gameObject;
        }
        else
        {
            target = null;
        }
    }

    // Method 10. Basic Moving AI
    public virtual void Move()
    {
        // Catch player
        if (target &&
            transform.position.x > moveRanges[0].x &&
            transform.position.x < moveRanges[1].x)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                                                     target.transform.position,
                                                     speed * Time.deltaTime);
        }
        // Move on the plane 
        else if(moveRanges != null)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                                                        moveRanges[moveIndex],
                                                        speed * Time.deltaTime);
            if(transform.position.x == moveRanges[moveIndex].x)
            {
                if(moveIndex == 1)
                {
                    moveIndex--;
                }
                else
                {
                    moveIndex++;
                }
            }
        }
    }

    // Method 11. Move Range on the Platform
    public void GetMoveRange(GameObject obj)
    {
        Vector3 position = obj.GetComponent<Transform>().position;
        float width = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = obj.GetComponent<SpriteRenderer>().bounds.size.y + GetComponent<SpriteRenderer>().bounds.size.y;

        moveRanges[0] = new Vector3(position.x - width / 2, position.y + height / 2, 0);
        moveRanges[1] = new Vector3(position.x + width / 2, position.y + height / 2, 0);
        moveIndex = 0;
    }
}
