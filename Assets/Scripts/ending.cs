using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ending : MonoBehaviour
{
    // Start is called before the first frame update
    int points;
    Sprite[] endingImages;
    void Start()
    {
        try
        {
            points = FindAnyObjectByType<gameManager>().finalScores;
            if(points >=0 && points < 3)
            {
                GetComponentInChildren<Image>().sprite = endingImages[0];
            }
            else if (points >= 3 && points < 5)
            {
                GetComponentInChildren<Image>().sprite = endingImages[0];
            }
            else if (points >= 5 && points < 8)
            {
                GetComponentInChildren<Image>().sprite = endingImages[0];
            }
            else
            {
                
            }
        }
        catch
        {
            Debug.Log("FinalSCore=" + points);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
