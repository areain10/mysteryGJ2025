using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorPivot : MonoBehaviour
{
    public bool open;
    bool unlocked;
    [SerializeField]string requiredKey;
    [SerializeField] GameObject openSprite;
    // Start is called before the first frame update
    void Start()
    {
        unlocked = false;
        try
        {
            openSprite.SetActive(false);
        }
        catch { }
        
    }
    public void openDoor(List<string> items)
    {
        if (open)
        {
            open = true;

        }
        else
        {
            if (requiredKey != null)
            {

                foreach (var i in items)
                {
                    if (i.Equals(requiredKey))
                    {
                        Debug.Log("OPENDOOR");
                        open = true;
                        unlocked = true;
                        //gameObject.GetComponent<item>().enabled = false;
                        try
                        {
                            openSprite.SetActive(true);
                            gameObject.SetActive(false);
                        }

                        catch { }
                        
                        //Destroy(gameObject.transform.GetChild(0).gameObject);
                        //Transform parent = transform.parent.transform;
                        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 90, transform.localEulerAngles.z);
                    }
                }


            }
            else
            {
                open = true;
                openSprite.SetActive(true);
                gameObject.SetActive(false);
                //Transform parent = transform.parent.transform;
                //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 90, transform.localEulerAngles.z);
            }
        }
            
        
            
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
