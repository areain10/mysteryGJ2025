using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayPrompt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("display prompt");
        try
        {
            collision.collider.gameObject.GetComponent<item>();
            Debug.Log("display prompt");
        }
        catch
        {

        }
    }
}
