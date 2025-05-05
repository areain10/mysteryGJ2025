using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class notification : MonoBehaviour
{
    bool fading;
    // Start is called before the first frame update
    public void ShowNotification(string message)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = message;
        StopAllCoroutines();
        StartCoroutine(fadeInNoti(1));
        
    }
    public IEnumerator fadeInNoti(int multiplier)
    {
        if (multiplier >= 0)
        {
            fading=true;
            float tmp = 0f;
            while (tmp <= 1)
            {

                GetComponent<Image>().color = new Color(0f, 0f, 0f, tmp);
                GetComponentInChildren<TextMeshProUGUI>().color=new Color(1f, 1f, 1f, tmp);
                tmp += 0.1f * multiplier;
                Debug.Log(tmp);
                yield return new WaitForSeconds(0.05f);

            }
            GetComponent<Image>().color = new Color(0f, 0f, 0f,1);
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1);
            yield return new WaitForSeconds(4f);
            yield return fadeInNoti(-1);
            //yield return null;
        }
        else
        {
            
            float tmp = 1f;
            while (tmp > 0)
            {
                GetComponent<Image>().color = new Color(0f, 0f, 0f, tmp);
                GetComponentInChildren<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, tmp);
                tmp += 0.1f * multiplier;
                yield return new WaitForSeconds(0.05f);

            }
            GetComponent<Image>().color = new Color(0f, 0f, 0f, 0);
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0);
            fading = false;
            //blackScreen.gameObject.SetActive(false);
        }


    }
    void Start()
    {
        fading = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
