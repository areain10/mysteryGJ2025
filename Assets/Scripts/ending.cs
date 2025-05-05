using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ending : MonoBehaviour
{
    // Start is called before the first frame update
    int points;
    [SerializeField]Sprite[] endingImages;
    gameManager gm;

    void Start()
    {
        gm = FindAnyObjectByType<gameManager>();
        try
        {
            points = gm.finalScores;
            if(points >=0 && points < 4)
            {
                GetComponentInChildren<Image>().sprite = endingImages[0];
            }
            else if (points >= 4 && points < 8)
            {
                GetComponentInChildren<Image>().sprite = endingImages[1];
            }
            else
            {
                if (gm.homoOrNah)
                {
                    GetComponentInChildren<Image>().sprite = endingImages[2];

                }
                else
                {
                    GetComponentInChildren<Image>().sprite = endingImages[3];
                }
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
