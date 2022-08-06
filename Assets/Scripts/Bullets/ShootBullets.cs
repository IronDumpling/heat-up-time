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
    // Speed
    public float bulletVelocity;

    [Range(0, 1)]
    public float bulletHeatPercent = 0.1f;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        bulletVelocity = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot Bullet
        if (Input.GetKeyDown(KeyCode.Mouse0) &&
            GetComponent<PlayerHeat>().curHeat > 0)
        {
            Shoot();
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


        PlayerHeat pH = this.GetComponent<PlayerHeat>();
        BulletController bH = bullet.GetComponent<BulletController>();
        bH.bulletHeat = pH.curHeat * bulletHeatPercent;
        pH.ShootHeat(bH.bulletHeat);
    }
}
