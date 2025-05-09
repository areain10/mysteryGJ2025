using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class safe : MonoBehaviour
{
    public bool open;
    public bool keypad;
    public List<int> correctCode;
    List<int> currentCode;
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI codeText;
    [SerializeField] GameObject openSprite;
    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
        keypad = false;
        openSprite.SetActive(false);
        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("sound/safeclick");
    }
    public void ShowKeypad()
    {
        if(keypad)
        {
            GameObject.FindAnyObjectByType<clueManager>().toggleNotePad(false);
            //GameObject.FindAnyObjectByType<clueManager>().gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //GameObject.FindAnyObjectByType<PlayerController>().isCrouching = false;
            GameObject.FindAnyObjectByType<clueManager>().gameObject.transform.position = new Vector3(GameObject.FindAnyObjectByType<clueManager>().gameObject.transform.position.x, GameObject.FindAnyObjectByType<clueManager>().gameObject.transform.position.y + 3, GameObject.FindAnyObjectByType<clueManager>().gameObject.transform.position.z);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            keypad = false;
            canvas.gameObject.SetActive(false);
        }
        else
        {
            GameObject.FindAnyObjectByType<clueManager>().toggleNotePad(true);

            //GameObject.FindAnyObjectByType<clueManager>().gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            keypad = true;
            currentCode = new List<int>() { 0,0,0};
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            canvas.gameObject.SetActive(true);
        }
        
    }
    public void changeNumber(int index)
    {
        currentCode[index] = (currentCode[index] + 1) % 10;
        GetComponent<AudioSource>().Play();
    }
    public void tryOpen()
    {
        bool same = true;
        for(int i = 0;i < correctCode.Count; i++)
        {
            if (correctCode[i] != currentCode[i])
            {
                same = false;
            }

        }
        if(same)
        {
            open = true;
            openSafe();
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
    }
    public void openSafe()
    {
        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("sound/safeopen");
        GetComponent<AudioSource>().Play();
        ShowKeypad();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
        openSprite.SetActive(true) ;
        GameObject.FindAnyObjectByType<clueManager>().toggleNotePad(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(keypad) 
        { 
            if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1))
            {

                ShowKeypad();
            }
            codeText.text = currentCode[0] + " " + currentCode[1] + " " + currentCode[2];
        }

        

    }
}
