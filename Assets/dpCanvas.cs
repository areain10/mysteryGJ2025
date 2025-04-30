using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dpCanvas : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindAnyObjectByType<clueManager>().gameObject;
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("uiCam").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
    }
}
