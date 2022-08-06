using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;
    private GameObject obj;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)

    {
        obj = other.gameObject;

        if(obj.tag == "Player")
        {
            Button.SetActive(true);
        }
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        obj = other.gameObject;

        if (obj.tag == "Player")
        {
            Button.SetActive(false);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if(Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            talkUI.SetActive(true);
        }
    }
}
