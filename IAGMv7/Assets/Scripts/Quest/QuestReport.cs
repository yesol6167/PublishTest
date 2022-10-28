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
    bool chk; // 성공 실패 여부 체크

    public struct QuestNPC
    {
        public string Name;
        public Quest.QuestInfo myQuestInfo; // 생성될 때 명성에 따른 랜덤 퀘스트 분배
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
        Debug.Log(chk ? "[성공]" : "[실패]");*/
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
        Debug.Log(chk ? "[성공]" : "[실패]");
        Debug.Log(chk);
        grade.text = npc.questgrade.ToString();
        name.text = "[" + npc.questname + "]";
        Result.text = chk ? "[성공]" : "[실패]"; // 성공 실패 여부 체크후 변경
        reward.text = "[골드 : " + npc.rewardgold.ToString() + "G]" + "\n" + "[평판 : " + npc.rewardfame.ToString() + "P]";
    }

    public void AddReward()
    {
        Debug.Log(ChildOBJ.GetComponent<Host>().Quest.gameObject);
        Destroy(ChildOBJ.GetComponent<Host>().Quest.gameObject);
        QuestManager.Instance.EndQuest(myQuest);
        if (chk)
        { // 성공시 증가 연산
            GameManager.Instance.Gold += myQuest.rewardgold;
            GameManager.Instance.Fame += myQuest.rewardfame;
            ChildOBJ.GetComponent<Host>().onSmile = true;
        }
        else
        { // 실패시 감소 연산
            GameManager.Instance.Gold -= myQuest.rewardgold;
            GameManager.Instance.Fame -= myQuest.rewardfame;
            ChildOBJ.GetComponent<Host>().onAngry = true;
        }
        //현재 퀘스트 리스트를 완료 퀘스트 리스트에 추가
        Destroy(gameObject); // 확인시 연산 후 페이지 삭제
        
    }
    
    
}
