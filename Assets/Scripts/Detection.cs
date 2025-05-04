using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;

public class Detection : MonoBehaviour
{
    [SerializeField] private Color startColor;
    //[SerializeField] private Renderer renderer;

    [SerializeField] private GameObject itemPopup;
    private TextMeshProUGUI itemDesc;

    [TextArea(2, 5)]
    [SerializeField] private string[] firstInteractionText;

    [TextArea(2, 5)]
    [SerializeField] private string[] followUpText;

    [TextArea(2, 5)]
    [SerializeField] private string interactionPromptText;
    [SerializeField] AudioSource talkingsfx;

    private int currentLine = 0;
    public bool isInteracting = false;
    private bool seen = false; 

    private string[] currentText;
    private bool isPromptActive = false;
    readingManager readingManager;
    public readable currentReading;
    bool canNextLine;
    float continueTimer;
    void Start()
    {
        currentLine= 0;
        currentReading = null;
        readingManager = FindAnyObjectByType<readingManager>();
        readingManager.gameObject.SetActive(false);
        if (itemPopup != null)
        {
            itemDesc = itemPopup.GetComponentInChildren<TextMeshProUGUI>();
            itemPopup.SetActive(false);
        }
        canNextLine = false;
       // var col = renderer.material.color;
        //col.a = 1f;
    }
    IEnumerator teleportTo(Transform loc)
    {
        gameObject.GetComponent<CharacterController>().enabled = false;
        StartCoroutine(GameObject.FindAnyObjectByType<gameManager>().fadeInBlack(1));
        yield return new WaitForSeconds(1f);

        gameObject.transform.position = loc.position;
        gameObject.GetComponent<CharacterController>().enabled = true;
    }
    void LoadInteractionText(string[] texts)
    {
        
        currentText = texts;
        Debug.Log(currentLine + "  " + currentText.Length);
        if(currentLine>=currentText.Length)
        {
            Debug.Log("Show interaction text");
            ClosePopup();
        }
        else
        {
            itemPopup.SetActive(true);
            canNextLine = false;
            StartCoroutine(writeOutInteractionBox(currentText[currentLine]));
            //itemDesc.text = currentText[currentLine];

        }
    }
    IEnumerator writeOutInteractionBox(string text)
    {
        itemDesc.text = "";
        foreach (char c in text)
        {
            if (!talkingsfx.isPlaying)
            {
                talkingsfx.Play();
            }
            
            itemDesc.text+= c;
            yield return new WaitForSeconds(0.02f);
        }
        
        canNextLine = true;
        yield return new WaitForSeconds(2f);
        canNextLine = false;
        currentLine++;
        LoadInteractionText(currentText);
        


    }
    void Update()
    {
        RaycastHit hits;
        Ray rays = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(rays, out hits,3f) && hits.collider.gameObject.GetComponent<item>() != null && !isInteracting && !readingManager.reading)
        {
            item script = hits.collider.gameObject.GetComponent<item>();
            Debug.Log("DisplayingPrompt: " + script.itemName);

            script.displayprompt();
        }
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && !isInteracting ) 
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

            if (Physics.Raycast(ray, out hit,3f)) 
            {
                
                if (hit.collider.gameObject.GetComponent<item>() != null) 
                {
                    isInteracting = true;
                    item script = hit.collider.gameObject.GetComponent<item>();
                    Debug.Log("Interacted with: " + script.itemName) ;

                    GetComponent<clueManager>().interactedWIthItem(script);
                    if (!script.seen)
                    {
                        script.seen = true;
                        script.pickedUp();
                        isInteracting = true;
                        try
                        {
                            if( script.gameObject.GetComponent<doorPivot>().open == false)
                            {
                                LoadInteractionText(script.firstInteractionText);
                            }
                        }
                        catch
                        {
                            LoadInteractionText(script.firstInteractionText);
                        }
                        
                    }
                    else
                    {
                        try
                        {
                            if (script.gameObject.GetComponent<doorPivot>().open == false)
                            {
                                LoadInteractionText(script.followUpText);
                            }
                        }
                        catch
                        {
                            LoadInteractionText(script.followUpText);
                        }
                        
                       
                    }
                }
                if (hit.collider.gameObject.GetComponent<door>() != null)
                {
                    door script = hit.collider.gameObject.GetComponent<door>();
                    try
                    {
                        script.gameObject.GetComponent<AudioSource>().Play();
                    }
                    catch { }
                    Debug.Log("FoundDoor");
                    StartCoroutine( teleportTo(script.spawnPoint));
                    
                }
                if (hit.collider.gameObject.GetComponent<safe>() != null)
                {
                    safe script = hit.collider.gameObject.GetComponent<safe>();
                    Debug.Log("FoundSafe");
                    if (!script.keypad)
                    {
                        script.ShowKeypad();
                    }
                    
                }
                if (hit.collider.gameObject.GetComponent<readable>() != null)
                {
                    readable script = hit.collider.gameObject.GetComponent<readable>();
                    currentReading = script;
                    
                    //Debug.Log();
                    

                }
                if (hit.collider.gameObject.GetComponent<doorPivot>() != null)
                {
                    doorPivot script = hit.collider.gameObject.GetComponent<doorPivot>();
                    Debug.Log("OpenDoor");
                    script.openDoor(gameObject.GetComponent<clueManager>().itemsInteracted);

                }
            }
        }

        if (isInteracting && itemPopup.activeSelf && Input.GetKeyDown(KeyCode.E) && canNextLine)
        {
            currentLine++;
            LoadInteractionText(currentText);
        }
    }


    private void ClosePopup()
    {
        if (currentReading != null)
        {
            readingManager.gameObject.SetActive(true);
            isInteracting = true;
            GetComponent<clueManager>().toggleNotePad(true);
            readingManager.load(currentReading.passages, currentReading.background);

        }
        itemPopup.SetActive(false);
        isInteracting = false;
        isPromptActive = false;
        seen = true;
        currentLine = 0;
    }

    void OnMouseEnter()
    {
        //startColor = renderer.material.color;
        //renderer.material.color = Color.red;
    }

    void OnMouseExit()
    {
        //renderer.material.color = startColor;
    }
}
