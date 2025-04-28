using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class importantItems : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject caseWriter;
    [SerializeField] GameObject ItemButtonPrefab;
    [SerializeField] GameObject ContainerGO;
    public List<string> itemsID;
    List<string> currentPage;
    int pageNum;
    public List<string> importantItemsFinalList;
    void Start()
    {
        //caseWriter.SetActive(false);
        pageNum = 1;
        try
        {
            itemsID = FindAnyObjectByType<gameManager>().clueID;
        }
        catch{
            itemsID = new List<string> { "001", "002"};
        }
        loadPage();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
    }
    void loadPage()
    {
        foreach (Transform child in ContainerGO.transform)
        {
            Destroy(child.gameObject);
        }
       
        currentPage = new List<string>();
        for (int i = 0;i < 9 - (Mathf.Clamp((pageNum * 9) - itemsID.Count, 0, 999) ); i++)
        {
            currentPage.Add(itemsID[i]);
        }
        for (int i = 0; i < currentPage.Count; i++)
        {
            GameObject go = Instantiate(ItemButtonPrefab);
            go.GetComponent<importantButtons>().instantiationOfButton(itemsID[((pageNum-1)*9)+i]);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - (80 * i), go.transform.position.z);
            go.transform.SetParent(ContainerGO.transform, false);
        }


    }
    public void importantItemsSelected()
    {

        GameObject go = Instantiate(caseWriter);
        go.GetComponent<casefilewriter>().importantID = importantItemsFinalList;
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            importantItemsSelected();
        }
    }
}
