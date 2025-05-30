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
    bool next;
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
        next = true;
        loadPage();
        
       


    }
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void loadPage()
    {
        currentPage = new List<string>();
        for (int i = 0; i < 7 - (Mathf.Clamp((pageNum * 7) - itemsID.Count, 0, 999)); i++)
        {
            currentPage.Add(itemsID[i]);
        }
        if (currentPage.Count > 0)
        {
            foreach (Transform child in ContainerGO.transform)
            {
                Destroy(child.gameObject);
            }

            
            for (int i = 0; i < currentPage.Count; i++)
            {
                GameObject go = Instantiate(ItemButtonPrefab);
               
                go.GetComponent<importantButtons>().instantiationOfButton(itemsID[((pageNum - 1) * 7) + i]);
                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - (80 * i), go.transform.position.z);
                go.transform.SetParent(ContainerGO.transform, false);
            }
        }
        else
        {
            if (next)
            {
                pageNum--;
            }
            else 
            {
                pageNum++;
            }

           
        }
        
        
        


    }
    public void nextPage(bool nexts)
    {
        GetComponent<AudioSource>().Play();
        next = nexts;
        if (nexts)
        {
            pageNum++;
        }
        else if (pageNum > 1) 
        {
            pageNum--;
        }
        
        loadPage();
    }
    public void importantItemsSelected()
    {

        GameObject go = Instantiate(caseWriter);
        go.GetComponent<casefilewriter>().importantID = importantItemsFinalList;
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            //importantItemsSelected();
        }
    }
}
