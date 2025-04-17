using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class importantButtons : MonoBehaviour
{
    importantItems iiManager;
    public string ItemID;
    // Start is called before the first frame update
    void Start()
    {
        //ItemID = Random.Range(0,100).ToString();
        iiManager = GameObject.FindAnyObjectByType<importantItems>();
    }
    public void instantiationOfButton(string id)
    {
        ItemID = id;
        gameManager gm = GameObject.FindAnyObjectByType<gameManager>();
        foreach (var item in gm.itemMasterList)
        {
            //Debug.Log(gm.itemMasterList[0][0] + " " + itemID);
            if (item[0] == ItemID)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = item[1];

            }
        }
        
    }
    public void toggleImportantItem()
    {
        if (iiManager.importantItemsFinalList.Contains(ItemID))
        {
            iiManager.importantItemsFinalList.Remove(ItemID);
            GetComponent<Image>().color = Color.white;

        }
        else
        {
            if(iiManager.importantItemsFinalList.Count < 7)
            {
                iiManager.importantItemsFinalList.Add(ItemID);
                GetComponent<Image>().color = Color.green;
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
