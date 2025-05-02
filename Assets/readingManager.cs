using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class readingManager : MonoBehaviour
{
    int currentPage;
    TextMeshProUGUI[] pageTextBox;
    public string[] passage;
    bool reading;
    // Start is called before the first frame update
    void Start()
    {
        pageTextBox = new TextMeshProUGUI[] {transform.GetChild(0).GetComponent<TextMeshProUGUI>(), transform.GetChild(1).GetComponent<TextMeshProUGUI>() };
        Debug.Log("Found this much textbox:"+pageTextBox.Length);
        //foreach(var page in pageTextBox) { page.text = "This works omg lol. omg oga jdfla;dkf"; }
    }

    public void load(string[] passages, Sprite backgroundImage)
    {
        passage = passages;
        reading= true;
        GetComponent<Image>().sprite = backgroundImage;
        pageTextBox[0].text = passages[0];
        pageTextBox[1].text = passages[1];
    }

    

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0)) && reading)
        {
            reading= false;
            FindAnyObjectByType<Detection>().currentReading = null;
            gameObject.SetActive(false);

        }
    }
}
