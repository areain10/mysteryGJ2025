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

    private int currentLine = 0;
    private bool isInteracting = false;
    private bool seen = false; 

    private string[] currentText;
    private bool isPromptActive = false;

    void Start()
    {
        

        if (itemPopup != null)
        {
            itemDesc = itemPopup.GetComponentInChildren<TextMeshProUGUI>();
            itemPopup.SetActive(false);
        }

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
    void Update()
    {
        RaycastHit hits;
        Ray rays = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(rays, out hits) && hits.collider.gameObject.GetComponent<item>() != null && !isInteracting)
        {
            item script = hits.collider.gameObject.GetComponent<item>();
            Debug.Log("DisplayingPrompt: " + script.itemName);

            script.displayprompt();
        }
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && !isInteracting ) 
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

            if (Physics.Raycast(ray, out hit)) 
            {
                
                if (hit.collider.gameObject.GetComponent<item>() != null) 
                {
                    
                    item script = hit.collider.gameObject.GetComponent<item>();
                    Debug.Log("Interacted with: " + script.itemName) ;

                    GetComponent<clueManager>().interactedWIthItem(script);
                    if (!script.seen)
                    {
                        script.seen = true;
                        currentText = script.firstInteractionText;
                        currentLine = 0;
                        try
                        {
                            if( script.gameObject.GetComponent<doorPivot>().open == false)
                            {
                                itemPopup.SetActive(true);
                                itemDesc.text = currentText[currentLine];
                                isInteracting = true;
                            }
                        }
                        catch
                        {
                            itemPopup.SetActive(true);
                            itemDesc.text = currentText[currentLine];
                            isInteracting = true;
                        }
                        
                    }
                    else
                    {
                        currentText = script.followUpText;
                        currentLine = 0;

                        itemPopup.SetActive(true);
                        itemDesc.text = currentText[currentLine];
                        isInteracting = true;
                    }
                }
                if (hit.collider.gameObject.GetComponent<door>() != null)
                {
                    door script = hit.collider.gameObject.GetComponent<door>();
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
                if (hit.collider.gameObject.GetComponent<doorPivot>() != null)
                {
                    doorPivot script = hit.collider.gameObject.GetComponent<doorPivot>();
                    Debug.Log("OpenDoor");
                    script.openDoor(gameObject.GetComponent<clueManager>().itemsInteracted);

                }
            }
        }

        if (isInteracting && itemPopup.activeSelf)
        {
            if (isPromptActive)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractWithObject(); 
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ClosePopup();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
                {
                    Continue(); 
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ClosePopup();
                }
            }
        }
    }

    private void Continue()
    {
        currentLine++;

        if (currentLine < currentText.Length)
        {
            itemDesc.text = currentText[currentLine];
        }
        else
        {
            ClosePopup();
            if (interactionPromptText.Length > 0)
            {
                ActivatePrompt();
            }
            else
            {
                ClosePopup();
            }
        }
    }

    private void ActivatePrompt()
    {
        if (!string.IsNullOrEmpty(interactionPromptText))
        {
            itemDesc.text = interactionPromptText;
        }
        isPromptActive = true;
    }

    private void InteractWithObject()
    {
        Debug.Log("You have interacted with the object!"); 
        ClosePopup();
    }

    private void ClosePopup()
    {
        itemPopup.SetActive(false);
        isInteracting = false;
        isPromptActive = false;
        seen = true; 
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
