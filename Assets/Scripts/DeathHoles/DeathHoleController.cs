using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHoleController : MonoBehaviour
{
    protected const int PLAYER = 3;
    public const int VILLAINS = 7;
    protected GameObject collobj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collobj = collision.gameObject;

        if(collobj.layer == PLAYER)
        {
            AbsorbObj(collobj, 0.3f);
        }
        else if (collobj.layer == VILLAINS)
        {
            collobj.GetComponent<GraffitiController>().Die();
        }
    }

    protected void AbsorbObj(GameObject obj, float seconds)
    {
        obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        StartCoroutine(Absorbing(obj, seconds));
    }

    IEnumerator Absorbing(GameObject obj, float seconds)
    {
        Vector3 normalScale = obj.transform.localScale;

        for (float i = normalScale.x ; i >= 0 ;  i = i- 0.1f)
        {
            obj.transform.localScale = new Vector3(i, i, 1);
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            yield return new WaitForSeconds(seconds);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
