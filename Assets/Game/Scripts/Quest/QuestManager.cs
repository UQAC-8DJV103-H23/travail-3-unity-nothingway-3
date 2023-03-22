using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questHolder;

    public List<Quest> CurrentQuests;
    [SerializeField] private int currentActiveQuest;

    private void Awake()
    {
        foreach (var quest in CurrentQuests) 
        {
            quest.Initialize();
            quest.QuestCompleted.AddListener(OnQuestCompleted);

            
            questHolder.SetActive(true);

        }
        currentActiveQuest = 0;

        questHolder.GetComponent<QuestWindow>().Initialize(CurrentQuests[currentActiveQuest]);

        print("Current quest : " + CurrentQuests[currentActiveQuest].name);
    }

    private void OnQuestCompleted(Quest quest)
    {
        print("quest completed");
        currentActiveQuest++;
        questHolder.GetComponent<QuestWindow>().closeWindow();
        questHolder.GetComponent<QuestWindow>().Initialize(CurrentQuests[currentActiveQuest]);

        print("Current quest : " + CurrentQuests[currentActiveQuest].name);
    }

    public void Slay(string killedEnemie)
    {
        EventManager.Instance.QueueEvent(new KillGameEvent(killedEnemie));
        print("Slayed");
        questHolder.GetComponent<QuestWindow>().UpdateIndicators(CurrentQuests[currentActiveQuest]);
    }

    public void Fetched(string fetchedItem)
    {
        EventManager.Instance.QueueEvent(new FetchGameEvent(fetchedItem));
        print("Fetched!");
        questHolder.GetComponent<QuestWindow>().UpdateIndicators(CurrentQuests[currentActiveQuest]);
    }
}
