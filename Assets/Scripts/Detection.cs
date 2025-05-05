using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Transactions;

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
    bool canTeleport;
    Transform tpTrans;
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
        canTeleport=false;
        // var col = renderer.material.color;
        //col.a = 1f;
    }
    IEnumerator teleportTo(Transform loc)
    {
        canTeleport = false;
        toggleSize();
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
    //bool canTeleport;
    void Update()
    {
        if(canTeleport && !isInteracting)
        {
            canTeleport=false;
            StartCoroutine(teleportTo(tpTrans));
        }
        RaycastHit hits;
        //Ray rays = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // Ray rays = Physics.Raycast( Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward);
        Debug.DrawRay(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward * 3f, Color.green);
        if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hits, 3.5f) && hits.collider.gameObject.GetComponent<item>() != null && !isInteracting && !readingManager.reading)
        {

            item script = hits.collider.gameObject.GetComponent<item>();
            //Debug.Log("DisplayingPrompt: " + script.itemName);
            script.displayprompt();
        }
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && !isInteracting ) 
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

            if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit,3.5f)) 
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
                    Debug.Log("FoundDoor "+isInteracting);
                    tpTrans = script.spawnPoint;
                    canTeleport = true;
                    //StartCoroutine( teleportTo(script.spawnPoint));
                    
                }
                else if (hit.collider.gameObject.GetComponent<safe>() != null)
                {
                    safe script = hit.collider.gameObject.GetComponent<safe>();
                    Debug.Log("FoundSafe");
                    if (!script.keypad)
                    {
                        script.ShowKeypad();
                    }
                    
                }
                else if (hit.collider.gameObject.GetComponent<readable>() != null)
                {
                    readable script = hit.collider.gameObject.GetComponent<readable>();
                    currentReading = script;
                    
                    //Debug.Log();
                    

                }
                else if (hit.collider.gameObject.GetComponent<doorPivot>() != null)
                {
                    doorPivot script = hit.collider.gameObject.GetComponent<doorPivot>();
                    Debug.Log("OpenDoor");
                    script.openDoor(gameObject.GetComponent<clueManager>().itemsInteracted);

                }
                else if (hit.collider.gameObject.GetComponent<boat>()!= null)
                {
                    boat script = hit.collider.gameObject.GetComponent<boat>();
                    script.endDayPrompt();
                }
            }
        }

        if (isInteracting && itemPopup.activeSelf && Input.GetKeyDown(KeyCode.E) && canNextLine)
        {
            currentLine++;
            LoadInteractionText(currentText);
        }
    }

    void toggleSize()
    {
        Debug.Log("TRANS:"+ transform.localScale.x);
        if (transform.localScale.x >= 1.5f)
        {
            transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        }
        else
        {
            transform.localScale = new Vector3(1.4f, 1.5f, 1.4f);
        }
    }
    public void ClosePopup()
    {
        if (currentReading != null)
        {
            itemPopup.SetActive(false);
            readingManager.gameObject.SetActive(true);
            isInteracting = true;
            GetComponent<clueManager>().toggleNotePad(true);
            readingManager.load(currentReading.passages, currentReading.background);

        }
        
        else
        {
            itemPopup.SetActive(false);
            isInteracting = false;
            isPromptActive = false;
            seen = true;
            currentLine = 0;
        }
       
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
