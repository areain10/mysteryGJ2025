using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startTimer());
    }
    IEnumerator startTimer()
    {
        yield return new WaitForSeconds(20);
        gameObject.GetComponent<gameManager>().goToContradiction(GameObject.FindAnyObjectByType<clueManager>());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
