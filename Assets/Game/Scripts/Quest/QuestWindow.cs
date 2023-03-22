using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalsContent;
    [SerializeField] private Text xpText;
    [SerializeField] private Text goldText;

    private List<GameObject> goalsIndicators = new List<GameObject>();

    public void Initialize(Quest quest)
    {
        titleText.text = quest.Information.name;
        descriptionText.text = quest.Information.description;

        var offset = new Vector3(0, 15, 0);
        int loopTimes = 0;



        gameObject.SetActive(true);

        foreach (var goal in quest.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalsContent);
            goalsIndicators.Add(goalObj);
            goalObj.transform.position += (offset * -loopTimes);
            goalObj.transform.Find("Text").GetComponent<Text>().text = goal.GetDescription();

            GameObject countObj = goalObj.transform.Find("Count").gameObject;

            if(goal.Completed)
            {
                countObj.SetActive(false);
                
                goalObj.transform.Find("Done").gameObject.SetActive(true);
            } else
            {
                countObj.GetComponent<Text>().text = goal.CurrentAmount + "/" + goal.RequiredAmount;

            }
            loopTimes++;
        }

        xpText.text = quest.reward.XP.ToString() + " XP";
        goldText.text = quest.reward.Currency.ToString() + " Gold";

        StartCoroutine(StartCountdown());
    }

    public void UpdateIndicators(Quest quest)
    {
        //TODO : ASK ABOUT DESYNC UNPDATE

        int indicatorIndex = 0;
        foreach (var goal in quest.Goals)
        {
            goalsIndicators[indicatorIndex].transform.Find("Count").gameObject.GetComponent<Text>().text = goal.CurrentAmount + "/" + goal.RequiredAmount;
            indicatorIndex++;
        }
        //StartCoroutine(StartCountdownUpdate(quest));
    }

    public IEnumerator StartCountdown(float countdownValue = 10f)
    {
        yield return new WaitForSeconds(countdownValue);
        gameObject.SetActive(false);
    }

    public IEnumerator StartCountdownUpdate(Quest quest)
    {
        yield return new WaitForSeconds(1f);
        int indicatorIndex = 0;
        foreach (var goal in quest.Goals)
        {
            goalsIndicators[indicatorIndex].transform.Find("Count").gameObject.GetComponent<Text>().text = goal.CurrentAmount + "/" + goal.RequiredAmount;
            indicatorIndex++;
        }
    }

    public void closeWindow()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < goalsContent.childCount; i++)
        {
            Destroy(goalsContent.GetChild(i).gameObject);
        }
    }

}
