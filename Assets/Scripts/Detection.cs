using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Detection : MonoBehaviour
{
    [SerializeField] private Color startColor;
    [SerializeField] private Renderer renderer;

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
        if (renderer == null)
            renderer = GetComponent<Renderer>();

        if (itemPopup != null)
        {
            itemDesc = itemPopup.GetComponentInChildren<TextMeshProUGUI>();
            itemPopup.SetActive(false);
        }

        var col = renderer.material.color;
        col.a = 1f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject) 
            {
                Debug.Log("Interacted with: " + hit.collider.gameObject.name);

                if (!seen)
                {
                    currentText = firstInteractionText;
                    currentLine = 0;

                    itemPopup.SetActive(true);
                    itemDesc.text = currentText[currentLine];
                    isInteracting = true;
                }
                else
                {
                    currentText = followUpText;
                    currentLine = 0;

                    itemPopup.SetActive(true);
                    itemDesc.text = currentText[currentLine];
                    isInteracting = true;
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
        startColor = renderer.material.color;
        renderer.material.color = Color.red;
    }

    void OnMouseExit()
    {
        renderer.material.color = startColor;
    }
}
