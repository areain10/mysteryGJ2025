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

    gameManager gm;

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
                firstInteractionText = new string[] { item[3] };
                followUpText = new string[] { item[4] }; 

            }
        }
    }
    public void displayprompt(bool show)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pickedUp()
    {
        Destroy(gameObject);
    }
}
