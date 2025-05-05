using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionsButton : MonoBehaviour
{
    // Start is called before the first frame update
    public int num;
    void Start()
    {
        
    }
    public void chooseOptions()
    {
        GameObject.FindAnyObjectByType<casefilewriter>().chooseOption(num);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
