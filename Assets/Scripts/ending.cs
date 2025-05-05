using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
public class ending : MonoBehaviour
{
    // Start is called before the first frame update
    int points;
    [SerializeField]Sprite[] endingImages;
    gameManager gm;
    public Ending[] choices;

    void Start()
    {
        gm = FindAnyObjectByType<gameManager>();
        choices[0] = gm.endingChoices[0];
        choices[1] = gm.endingChoices[1];
        if (gm.finalScores > 3)
        {
            switch(gm.endingChoices[0])
            {
                case Ending.selfdmurder:
                    if (gm.endingChoices[1]!=Ending.nosearch)
                    {
                        GetComponent<EndingText>().endingWords[1] = "Work at the lighthouse resumes after O’Brien’s body is removed. His family mourns but find solace in the presumed death of the man who killed him. You are celebrated for your careful work.";
                        
                    }
                    else
                    {
                        GetComponent<EndingText>().endingWords[1] = "Work at the lighthouse resumes after O’Brien’s body is removed. His family persistently demand the arrest and execution of his killer(s). ";

                    }

                    break;
                case Ending.suicideaccidental:
                    GetComponent<EndingText>().endingWords[1] = "Work at the lighthouse resumes after O’Brien’s body is removed. His family mourns but doubt the legitimacy of the case report. A new investigation began, but was greatly hindered by being late. ";
                    break;
                default:
                    GetComponent<EndingText>().endingWords[1] = "O’Brien’s body is found in the keeper’s quarters and work resumes after its removal. His family express anger and resentment towards what seemed to be a shoddy investigation. A new investigation began, but was greatly hindered by being late.";

                    break;
            }
            switch (gm.endingChoices[1])
            {
                case Ending.wanted:
                    GetComponent<EndingText>().endingWords[3] = "You wonder if you’ve done the right thing.";

                    GetComponent<EndingText>().endingWords[2] = "Being wanted, Jack Moore is soon found with Owen Kelly. Both are charged and given grave sentences for their crimes. You are celebrated for your noble work.";
                    break;
                case Ending.thellight:
                    if (gm.endingChoices[0] == Ending.selfdmurder)
                    {
                        GetComponent<EndingText>().endingWords[2] = "Search parties are eventually sent out for Moore, discovering him with a man by the name of Owen Kelly. They are put to death for their work in the murder of Hugh O’Brien.";
                        GetComponent<EndingText>().endingWords[3] = "You wonder if you’ve done the right thing.";

                    }
                    else
                    {
                        GetComponent<EndingText>().endingWords[2] = "Search parties are eventually sent out for Moore, discovering him with a man by the name of Owen Kelly. They are jailed for gross indecency, and Moore receives an additional charge for abandoning the light.";

                    }
                    break;
                case Ending.nosearch:
                    GetComponent<EndingText>().endingWords[3] = "You’ve done good, Detective.";

                    GetComponent<EndingText>().endingWords[2] = "Neither Moore nor Kelly are ever found. One day, you receive a letter thanking you for your careful investigation into the case, addressed from “Fox and Aphrodite.” ";
                    break;



            }
        }
        else 
        { 
            GetComponent<EndingText>().endingWords[2] = "As the case is reopened, Moore is found with a man named Owen Kelly. It is determined that Moore murdered O’Brien and he is put to death; Kelly is imprisoned for gross indecency.";
            GetComponent<EndingText>().endingWords[3] = "Your superiors were unhappy with your work on the case and fired you. You will never work as an investigator again.";
            GetComponent<EndingText>().endingWords[1] = "O’Brien’s body is found in the keeper’s quarters and work resumes after its removal. His family express anger and resentment towards what seemed to be a shoddy investigation. A new investigation began, but was greatly hindered by being late.";
        }
        StartCoroutine(GetComponent<EndingText>().FadeText());
       
            Debug.Log("FinalSCore=" + points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
