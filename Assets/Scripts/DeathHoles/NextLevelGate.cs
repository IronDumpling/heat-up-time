using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelGate : MonoBehaviour
{
    private GameObject collObj;
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collObj = collision.gameObject;

        if(collObj.tag == "Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        else if(collObj.tag == "Villain")
            collObj.GetComponent<GraffitiController>().Die();
    }
}
