using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public string itemID;
    [HideInInspector] public string itemName;

    [TextArea(2, 5)]
    [HideInInspector] public string[] firstInteractionText;

    [TextArea(2, 5)]
    [HideInInspector] public string[] followUpText;

    [TextArea(2, 5)]
    [HideInInspector] public string interactionPromptText;

    public bool seen = false;
    bool canPickedUp;
    gameManager gm;

    float timer;
    bool displaying;
    GameObject displayPrompt;
        // Start is called before the first frame update
    void Start()
    {
        gm= GameObject.FindAnyObjectByType<gameManager>();
        foreach(var item in gm.itemMasterList)
        {
            //Debug.Log(gm.itemMasterList[0][0] + " " + itemID);
            if (item[0] == itemID)
            {
                itemName = item[1];
                interactionPromptText = item[2];
                canPickedUp = interactionPromptText[0].ToString().ToLower() == "p";
                firstInteractionText = item[3].Split('|');
                followUpText = item[4].Split('|'); 

            }
        }
    }
    public void displayprompt()
    {
        timer = Mathf.Clamp(timer+0.3f,0,1);
        
    }
    void checkIfShouldDisplay()
    {
        
        if (!displaying && timer > 0.1f)
        {
            displaying = true;
            try
            {
                displayPrompt.gameObject.SetActive(true);
            }
            catch {
                var dpPre = Resources.Load<GameObject>("dpPre");
                GameObject go = Instantiate(dpPre);
                go.transform.SetParent(transform, true);
                //go.transform.parent = transform;
                go.transform.position = transform.position;
                displayPrompt = go;
            }
        }
        else if (displaying && timer <= 0.1f)
        {
            displaying = false;
            try
            {
                displayPrompt.gameObject.SetActive(false);
            }
            catch { }
        }

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (displayPrompt!=null)
        {
            //Debug.Log(timer + "    " + displaying);
            
        }
        timer = Mathf.Clamp(timer - 0.01f, 0, 1);

    }
    void Update()
    {
        
        //Debug.Log(timer);
        checkIfShouldDisplay();
    }
    public void pickedUp()
    {
        timer = 0;
        if(canPickedUp)
        {
            Destroy(gameObject);

        }
    }
}
