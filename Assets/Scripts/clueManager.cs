using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Rooms { Shed, House, Lighthouse, Dock}
public class clueManager : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI text;
    public List<item> itemSeen;
    public List<Rooms> roomsVisited;
    public bool notepad;
    // Start is called before the first frame update
    void Start()
    {
        notepad = false;
        canvas.enabled = notepad;
        Cursor.visible = !notepad;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
            if(Physics.Raycast(ray, out hit,1f))
            {
                if(hit.collider != null)
                {
                    try
                    {
                        item script = hit.collider.gameObject.GetComponent<item>();
                        itemSeen.Add(script);
                        script.pickedUp();

                    }
                    catch
                    {

                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            notepad = !notepad;
            canvas.enabled = notepad;
            text.text = itemSeen[0].itemName;
            if (notepad)
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            }
            
            

        }

    }
}
