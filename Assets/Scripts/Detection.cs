using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private Color startColor;
    [SerializeField] private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        var col = renderer.material.color;
        col.a = 1f;
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
            }
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
