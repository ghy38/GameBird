using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text titleText;
    public Text contentText;
    public Text yourScore;

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow); 
    }

    public void UpdateDialog(string title, string content, string yourscore)
    {
        if (titleText)
            titleText.text = title;
        if (contentText)
            contentText.text = content;
        if (yourScore)
            yourScore.text = yourscore;
    }
}
