using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestReport : MonoBehaviour
{
    public GameObject ParentOBJ;
    public GameObject ChildOBJ;
    public Quest.QuestInfo myQuest;
    public TMP_Text Questname;
    public TMP_Text grade;
    public TMP_Text name;
    TMP_Text info;
    public TMP_Text reward;
    public TMP_Text Result;
    string result;
    bool chk; // ���� ���� ���� üũ

    public struct QuestNPC
    {
        public string Name;
        public Quest.QuestInfo myQuestInfo; // ������ �� ���� ���� ���� ����Ʈ �й�
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowQuest(myQuest);
        
        ParentOBJ = GameObject.Find("SpawnPoint1");
        ChildOBJ = ParentOBJ.transform.GetChild(0).gameObject;
        /*int rnd = Random.Range(0, 10);
        if (rnd > 4)
        {
            chk = true;
        }
        else
        {
            chk = false;
        }
        Debug.Log(chk ? "[����]" : "[����]");*/
    }

    public void ShowQuest(Quest.QuestInfo npc)
    {
        int rnd = Random.Range(0, 10);
        if (rnd > 4)
        {
            chk = true;
        }
        else
        {
            chk = false;
        }
        Debug.Log(chk ? "[����]" : "[����]");
        Debug.Log(chk);
        grade.text = npc.questgrade.ToString();
        name.text = "[" + npc.questname + "]";
        Result.text = chk ? "[����]" : "[����]"; // ���� ���� ���� üũ�� ����
        reward.text = "[��� : " + npc.rewardgold.ToString() + "G]" + "\n" + "[���� : " + npc.rewardfame.ToString() + "P]";
    }

    public void AddReward()
    {
        Debug.Log(ChildOBJ.GetComponent<Host>().Quest.gameObject);
        Destroy(ChildOBJ.GetComponent<Host>().Quest.gameObject);
        QuestManager.Instance.EndQuest(myQuest);
        if (chk)
        { // ������ ���� ����
            GameManager.Instance.Gold += myQuest.rewardgold;
            GameManager.Instance.Fame += myQuest.rewardfame;
            ChildOBJ.GetComponent<Host>().onSmile = true;
        }
        else
        { // ���н� ���� ����
            GameManager.Instance.Gold -= myQuest.rewardgold;
            GameManager.Instance.Fame -= myQuest.rewardfame;
            ChildOBJ.GetComponent<Host>().onAngry = true;
        }
        //���� ����Ʈ ����Ʈ�� �Ϸ� ����Ʈ ����Ʈ�� �߰�
        Destroy(gameObject); // Ȯ�ν� ���� �� ������ ����
        
    }
    
    
}
