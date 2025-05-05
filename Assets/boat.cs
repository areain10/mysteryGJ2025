using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject confirmEndDayCanvas;
    bool canend;
    void Start()
    {
        confirmEndDayCanvas.SetActive(false);
    }
    public void endDayPrompt()
    {
       canend = true;
        //StartCoroutine(endday());
    }
    IEnumerator endday()
    {
        if (canend)
        {
            gameManager gm = FindAnyObjectByType<gameManager>();
            StartCoroutine(gm.fadeInBlack(1));
            yield return new WaitForSeconds(1);
            gm.goToContradiction(FindAnyObjectByType<clueManager>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(canend)
        {
            if(confirmEndDayCanvas.activeSelf == false)
            {
                confirmEndDayCanvas.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(endday());
                canend = false;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                canend = false;
                confirmEndDayCanvas.SetActive(false);
                FindAnyObjectByType<Detection>().ClosePopup();

            }
        }
    }
}
