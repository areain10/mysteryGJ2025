using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contradictManager : MonoBehaviour
{
    string[] currentClue;
    string[] currentDiscrepancy;

    public bool checkIfCorrect()
    {
        bool tmp = true;
        for (int i = 0; i < currentClue.Length; i++)
        {
            if (currentClue[i] != currentDiscrepancy[i])
            {
                tmp = false;
            }
        }
        return tmp;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
