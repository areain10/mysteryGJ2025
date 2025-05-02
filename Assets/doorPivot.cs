using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorPivot : MonoBehaviour
{
    public bool open;
    bool unlocked;
    [SerializeField]string requiredKey;
    // Start is called before the first frame update
    void Start()
    {
        unlocked = false;
    }
    public void openDoor(List<string> items)
    {
       
            if(requiredKey != null)
            {
               
                    foreach (var i in items)
                    {
                        if (i.Equals(requiredKey))
                        {
                            open = true;
                            unlocked = true;
                            gameObject.GetComponent<item>().enabled = false;
                            Transform parent = transform.parent.transform;
                            parent.localEulerAngles = new Vector3(parent.localEulerAngles.x, parent.localEulerAngles.y + 90, parent.localEulerAngles.z);
                        }
                    }
                
                
            }
            else
            {
                open = true;
                Transform parent = transform.parent.transform;
                parent.localEulerAngles = new Vector3(parent.localEulerAngles.x, parent.localEulerAngles.y + 90, parent.localEulerAngles.z);
            }
        
            
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
