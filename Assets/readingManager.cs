using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class readingManager : MonoBehaviour
{
    int currentPage;
    TextMeshProUGUI[] pageTextBox;
    public string[] passage;
    public bool reading;
    Sprite bg;
    // Start is called before the first frame update
    void Start()
    {
        currentPage = 0;
        pageTextBox = new TextMeshProUGUI[] {transform.GetChild(0).GetComponent<TextMeshProUGUI>(), transform.GetChild(1).GetComponent<TextMeshProUGUI>() };
        Debug.Log("Found this much textbox:"+pageTextBox.Length);
        //foreach(var page in pageTextBox) { page.text = "This works omg lol. omg oga jdfla;dkf"; }
    }

    public bool load(string[] passages, Sprite backgroundImage)
    {
        bool tmp = false;
        passage = passages;
        reading= true;
        GetComponent<Image>().sprite = backgroundImage;
        bg = backgroundImage;
        try
        {
            pageTextBox[0].text = passages[currentPage];
            tmp = true;
            pageTextBox[1].text = passages[currentPage+1]; tmp = true;
        }
        catch
        {

        }
       
        return tmp;
    }

    public void changePage(int addin)
    {
        currentPage = Mathf.Clamp(currentPage+addin, 0, (int)(Mathf.Ceil(passage.Length/2)));
        
        load(passage, bg);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E)) && reading)
        {
            reading= false;
            FindAnyObjectByType<Detection>().currentReading = null;
            FindAnyObjectByType<clueManager>().toggleNotePad(true);
            gameObject.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.D) && reading)
        {
            changePage(2);
        }
        else if ((Input.GetKeyDown(KeyCode.A)) && reading)
        {
            changePage(-2);
        }
    }
}
