using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSystem : MonoBehaviour
{
    [Header("UI")]
    public Text textLabel;
    public Text Name;

    [Header("TextFile")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;



    bool textFinished; //是否完成打字
    bool cancelTyping; //跳过打字

    List<string> textList = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        GetTextFormFile(textFile);
        index = 0;
    }
    private void OnEnable()
    {
        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (textFinished && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinished && !cancelTyping)
            {
                cancelTyping = true;
            }

        }
    }


    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;


        var lineData = file.text.Split('\n');
        textList.AddRange(lineData);

    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        switch (textList[index].Trim()) //显示说话人名
        {
            case "0":
                Name.text = "";
                index++;
                break;

            case "A":
                Name.text = "machine";
                index++;
                break;
        }

        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;


    }

}
