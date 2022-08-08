using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveTalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;
    private GameObject obj;
    public TextAsset specifiedtextFile;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)

    {
        obj = other.gameObject;

        if (obj.tag == "Player")
        {
            talkUI.GetComponent<DialogSystem>().textFile = specifiedtextFile;
            if (talkUI.GetComponent<DialogSystem>().index == 0)
            {
                talkUI.GetComponent<DialogSystem>().GetTextFormFile(specifiedtextFile);
            }
            talkUI.SetActive(true);
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        obj = other.gameObject;

        if (obj.tag == "Player")
        {
            talkUI.SetActive(false);
            talkUI.GetComponent<DialogSystem>().index = 0;
        }
    }
    // Update is called once per frame
    
            
        

    
}