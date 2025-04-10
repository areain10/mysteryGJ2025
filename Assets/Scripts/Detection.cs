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
    [SerializeField] private string description;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        
        itemDesc = itemPopup.GetComponentInChildren<TextMeshProUGUI>();

        var col = renderer.material.color;
        col.a = 1f;

        itemPopup.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Interactable")) 
            {
                Debug.Log("Interacted with: " + hit.collider.gameObject.name);

                itemPopup.SetActive(true);
            }
        }

        if (itemPopup.activeSelf)
        {
            itemDesc.text = description;
        }
    }

    void OnMouseEnter ()
    {
        startColor = renderer.material.color;
        renderer.material.color = Color.red;
    }

    void OnMouseExit ()
    {
        renderer.material.color = startColor;
    }
}
