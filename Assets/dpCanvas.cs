using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dpCanvas : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = Vector3.zero;
        player = GameObject.FindGameObjectWithTag("uiCam").GetComponent<Camera>().gameObject;
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("uiCam").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3
        (
             90,
             transform.eulerAngles.y,
             0
        );
        //transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        transform.LookAt(player.transform.position);
    }
}
