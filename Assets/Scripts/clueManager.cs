using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public enum Rooms { Shed, House, Lighthouse, Dock, Outside}
public class clueManager : MonoBehaviour
{
    [SerializeField] List<item> testItem;
    public List<Rooms> testRooms;
    public List<string[]> cluesWritten;
    public Canvas canvas;
    public TextMeshProUGUI text;
    public TextMeshProUGUI debugtext;
    public List<item> itemSeen;
    public List<Rooms> roomsVisited;
    [SerializeField] string testNum;
    [SerializeField] string testName;
    [SerializeField] string testRoom;
    [SerializeField] AudioSource clueSound;
    int currentItemSelection;
    int currentRoomSelection;
    int currentNotepad;
    int currentNum;
    int currentSelection;
    int pageNum;
    public bool notepad;
    public List<string> itemsInteracted;
    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindAnyObjectByType<gameManager>();
        notepad = false;
        canvas.enabled = notepad;
        
        itemsInteracted = new List<string>();
        //itemsInteracted.Add("000");
        cluesWritten = new List<string[]>
        {
            new string[] { testNum, testName, testRoom }
        };
        roomsVisited.Clear();
        roomsVisited = testRooms;
        itemSeen.Clear();
        itemSeen = testItem;

    }
    public void interactedWIthItem(item script)
    {
        bool tmp = false;
        foreach(var item in itemsInteracted)
        {
            if(item == script.itemID)
            {
                tmp= true;
            }
        }
        if (!tmp)
        {
            itemsInteracted.Add(script.itemID);
            FindAnyObjectByType<notification>().ShowNotification("Clue has been noted down");
            clueSound.Play();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if (pageNum==-1)
        {
            debugtext.text = "WRITING CLUE";
        }
        else
        {
            debugtext.text = "BROWSING CLUES";
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            gm.goToContradiction(gameObject.GetComponent<clueManager>());
            
        }
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0)) && notepad)
        {
            //toggleNotePad(false);

        }
        /*
        if (notepad)
        {
            if(pageNum == -1)
            {
                text.text = currentNum + " " + itemSeen[currentItemSelection].itemName + "(s) in " + roomsVisited[currentRoomSelection].ToString();
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (currentSelection == 1)
                    {
                        currentItemSelection = Mathf.Abs(currentItemSelection + 1) % itemSeen.Count;
                    }
                    else if (currentSelection == 2)
                    {
                        currentRoomSelection = Mathf.Abs(currentRoomSelection + 1) % roomsVisited.Count;
                    }
                    else
                    {
                        currentNum++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    if (currentSelection == 1)
                    {
                        currentItemSelection = Mathf.Abs(currentItemSelection - 1) % itemSeen.Count;
                    }
                    else if (currentSelection == 2)
                    {
                        currentRoomSelection = Mathf.Abs(currentRoomSelection - 1) % roomsVisited.Count;
                    }
                    else if (currentNum >= 1)
                    {
                        currentNum--;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    currentSelection = Mathf.Abs(currentSelection - 1) % 3;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    currentSelection = Mathf.Abs(currentSelection + 1) % 3;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    //clue gets written down
                    cluesWritten.Add(new string[] { currentNum.ToString(), itemSeen[currentItemSelection].itemName, roomsVisited[currentRoomSelection].ToString() });
                    pageNum = 0;
                }
            }
            else
            {
                text.text = cluesWritten[pageNum][0] + " " + cluesWritten[pageNum][1] + "(s) in " + cluesWritten[pageNum][2];
                if (Input.GetKeyDown(KeyCode.A))
                {
                    pageNum = Mathf.Abs(pageNum - 1) % cluesWritten.Count;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    pageNum = Mathf.Abs(pageNum + 1) % cluesWritten.Count;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentItemSelection = 0;
                    currentNotepad = 0;
                    currentNum = 0;
                    currentRoomSelection = 0;
                    currentSelection = 0;
                    pageNum = -1;
                }
            }
            
        }*/
        /*if (Input.GetKeyDown(KeyCode.E))
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
        }*/
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
    public void toggleNotePad(bool newVal)
    {
        notepad = newVal;
        switch (newVal)
        {
            case true: gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;GetComponent<Detection>().isInteracting = true;Cursor.lockState = CursorLockMode.Confined;Cursor.visible=true ;
                break;
            case false: gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; GetComponent<Detection>().isInteracting = false; Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
                break;
                
        }
    }
}
