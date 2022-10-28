using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ADQuestInfo : MonoBehaviour // 퀘스트 신청서
{
    public GameObject ParentOBJ;
    public GameObject ChildOBJ;
    //스크립트 QuestInformation이랑 공통부분이 많음 >> AddQuest부분을 두 부분으로 나눠서 수정하면 될것같음. 후에 마을사람 모험가 프리팹에 수정
    // 다른 스크립트에 ADQuestInfo라는 스크립트 적용되어있는지 확인
    public Quest.QuestInfo myQuest;
    public TMP_Text Questname;
    public TMP_Text grade;
    public TMP_Text name;
    public TMP_Text info;
    public TMP_Text reward;
    public int index;

    GameObject QuestWindowArea; // AdDataWindow가 자식으로 생성되는 UiCanvas안의 부모 오브젝트

    public void Start()
    {
        QuestWindowArea = GameObject.Find("QuestWindowArea");// 모험가 정보보기를 눌렀을 때 부모로 정할 오브젝트를 미리 정해줌

        ShowQuest(myQuest);

        ParentOBJ = GameObject.Find("SpawnPoint1");
        ChildOBJ = ParentOBJ.transform.GetChild(0).gameObject;
    }

    public void ShowQuest(Quest.QuestInfo npc)
    {
        grade.text = npc.questgrade.ToString();
        name.text = "[" + npc.questname + "]";
        info.text = "\"" + npc.information + "\"";
        reward.text = "[골드 : " + npc.rewardgold.ToString() + "G]" + "\n" + "[평판 : " + npc.rewardfame.ToString() + "P]";
    }

    public void AddQuest() // 승낙
    {
        QuestManager.Instance.ProgressQuest(myQuest,ChildOBJ);
        ChildOBJ.GetComponent<Host>().exitchk = true;
        ChildOBJ.GetComponent<Host>().onSmile = true;
    }

    public void onDestroy()
    {
        Destroy(gameObject);
    }

    public void onQuestRfuse()
    {
        ChildOBJ.GetComponent<Host>().onAngry = true;
        ChildOBJ.GetComponent<Host>().exitchk = true;
        QuestManager.Instance.PostedQuest(myQuest);
    }

    public void onAdDataWindow() // 모험가 정보창 띄우기
    {
        GameObject obj = Instantiate(Resources.Load("UiPrefabs/AdDataWindow"), QuestWindowArea.transform) as GameObject;
    }
}
