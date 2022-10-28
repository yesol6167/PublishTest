using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestIcon : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Transform myTarget;
    public GameObject myButton;
    public Transform QuestWindow;
    Vector2 dragOffset = Vector2.zero;
    Quest.QuestInfo npcQuest;
    public GameObject hostobj;
    bool NPCchk; // ������� or ���谡 üũ��


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (myTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(myTarget.position);
            //-���߿� �̺κ� ������ ���� ������ ��� ã��-
            //myTarget.parent.parent.parent.parent.parent.parent.parent.parent.gameObject = host;
            hostobj = myTarget.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            NPCchk = hostobj.GetComponent<Host>().hostchk;// ȣ��Ʈ ���� �����Ŵ������� �ٷ� �޾ƿ���
            npcQuest = NPCchk ? hostobj.GetComponent<VLNpc>().myQuest : hostobj.GetComponent<QuestInformation>().myQuest; // ���׽����� ������� ��, ���谡 ������
        }
    }
    public void ShowRequestWindow() //����Ʈ ��ư ������ ����Ʈ ��û�� ����
    { // ����Ʈ ��û���� ����� ���̰� ��ġ ����
        //����Ʈ �Ϸ�� if������ ���� >> ����Ʈ�� �Ϸ��ߴٴ� ���� üũ
        GameObject RQwindow = NPCchk ? Instantiate(Resources.Load("Prefabs/RequestWindow"), GameObject.Find("QuestWindowArea").transform) as GameObject : // �������
            //���谡
            hostobj.GetComponent<Host>().IsFinishQuest ? Instantiate(Resources.Load("Prefabs/QuestReportWindow"), GameObject.Find("QuestWindowArea").transform) as GameObject :
            Instantiate(Resources.Load("Prefabs/QuestWindow"), GameObject.Find("QuestWindowArea").transform) as GameObject; // ���̸� ������� �����̸� ���谡 >> ���谡���� ���̸� ����, �����̸� ��û��
        RQwindow.GetComponent<QuestInformation>().People = hostobj.GetComponent<Host>().People;
        RQwindow.GetComponent<QuestInformation>().IsQuestchk = hostobj.GetComponent<Host>().IsFinishQuest;
        RQwindow.GetComponent<QuestInformation>().myQuest = npcQuest;
        //NPCchk= tru or false / Ʈ���ϰ�� â���������� �ڽ����� ��û�� ���� or �޽��� ���  IsFinishQuest�� bool�� üũ �� ���ǿ� �´� ���๮�� ����

        if (NPCchk)
        {
            //RQwindow ������Ʈ�� QuestInformation ��ũ��Ʈ�� myQuest ������ npcQuest�� �ִ´�.
            RQwindow.GetComponent<QuestInformation>().myNpc = hostobj;
        }
        //UiCanvas �ؿ� ����
        RQwindow.transform.SetAsFirstSibling();
        RQwindow.SetActive(true);
        myButton.SetActive(false);
    }

    public void onDestroyIcon()
    {
        Destroy(gameObject);
    }

    //����Ʈâ �巡�� �� ���
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)QuestWindow.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        QuestWindow.position = eventData.position + dragOffset;
    }
}
