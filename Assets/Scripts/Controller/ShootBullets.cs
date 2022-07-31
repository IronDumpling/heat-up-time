using UnityEngine;
using System.Collections;

public class ShootBullets : MonoBehaviour
{
    // Shoot Direction Pointers
    [SerializeField] private Camera mainCamera;
    private Vector3 mousePosition;
    private Vector3 shootDirection;
    // Bullet Types Pointers
    public GameObject bulletType;
    public float curHeat;
    // Speed
    public float bulletVelocity;
    // Bounds
    private float boundHeat;
    public float lowMidBound;
    public float midHighBound;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        bulletVelocity = 15f;

        boundHeat = GetComponent<PlayerHeat>().boundHeat;
        lowMidBound = boundHeat / 3;
        midHighBound = boundHeat * 2/3;
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot Bullet
        if (Input.GetKeyDown(KeyCode.Mouse0) &&
            GetComponent<PlayerHeat>().curHeat > 0)
        {
            Shoot();
            GetComponent<PlayerHeat>().ShootHeat(bulletType.
                GetComponent<BulletController>().curHeat);
        }

        // Switch Bullet Types
        curHeat = GetComponent<PlayerHeat>().curHeat;

        // Low Heat Bullet
        if(curHeat <= lowMidBound)
        {
            bulletType = Resources.Load<GameObject>("Prefabs/Bullets/LowHeatBullet");
        }

        // High Heat Bullet
        else if (midHighBound < curHeat)
        {
            bulletType = Resources.Load<GameObject>("Prefabs/Bullets/HighHeatBullet");
        }

        // Mid Heat Bullet
        else
        {
            bulletType = Resources.Load<GameObject>("Prefabs/Bullets/MediumHeatBullet");
        }
    }

    // Method 1. Shoot
    void Shoot()
    {
        // Get direction
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        shootDirection = Vector3.Normalize(mousePosition - transform.position);

        // Generate bullet
        Vector3 bulletPosition = new Vector3(transform.position.x + shootDirection.x,
            transform.position.y + shootDirection.y, 0);
        GameObject bullet = Instantiate(bulletType, bulletPosition, Quaternion.Euler(Vector3.zero));

        // Shoot to the mouse direction
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * bulletVelocity,
                                                                  shootDirection.y * bulletVelocity);
    }
}
