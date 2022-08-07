using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBinding : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -2, 0);
        transform.rotation = Quaternion.EulerAngles(new Vector3(0, 0, -30));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        Rotation(1000);
    }

    public void Rotation(float speed)
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
