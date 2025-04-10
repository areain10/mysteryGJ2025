using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum Rooms { Shed, House, Lighthouse, Dock, Outside}
public class clueManager : MonoBehaviour
{
    [SerializeField] item testItem;
    public List<string[]> cluesWritten;
    public Canvas canvas;
    public TextMeshProUGUI text;
    public List<item> itemSeen;
    public List<Rooms> roomsVisited;
    [SerializeField] string testNum;
    [SerializeField] string testName;
    [SerializeField] string testRoom;

    public bool notepad;
    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindAnyObjectByType<gameManager>();
        notepad = false;
        canvas.enabled = notepad;
        Cursor.visible = !notepad;
        Cursor.lockState = CursorLockMode.Locked;
        cluesWritten = new List<string[]>
        {
            new string[] { testNum, testName, testRoom }
        };
        roomsVisited.Clear();
        roomsVisited.Add(Rooms.Outside);
        itemSeen.Clear();
        itemSeen.Add(testItem);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            gm.goToContradiction(gameObject.GetComponent<clueManager>());
            
        }
        text.text = "0 "+ itemSeen[0].itemName +"(s) in "+roomsVisited[0].ToString();
        if (notepad)
        {
            
        }
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
