using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Unity.Jobs;
using UnityEngine.SearchService;
using static UnityEngine.GraphicsBuffer;
using System.Threading;
using UnityEngine.UI;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine.AI;

public class Host : MonoBehaviour
{
    public int ADTYPE = 0; // ADTYPE���� ������ ���� ����, �����Ŵ������� �����ǰ� ADNPC.cs���� �˻��� by����
    public static Host inst = null;

    public GameObject Quest; // ������ ���� ���� >> ����Ʈ �Ϸ� >> �Ϸ�Ǿ����� quest �������� �ı�

    //NpcClock,QuestImoticon
    public Transform spawnPoints;
    public Transform myIconZone;
    public Transform OutPoint;
    ClockIcon myUIC = null;
    QuestIcon myUIQ = null;
    AngryIcon myUIA = null;
    MealIcon myUIM = null;
    SmileIcon myUIS = null;
    SleepIcon myUISleep = null;
    public GameObject obj = null;
    public GameObject objC = null;

    GameObject IconArea = null;
    public bool hostchk; // ��������� ���谡 ���п�
    public bool Firstchk = false;
    public bool IsFinishQuest = false; // ����Ʈ �Ϸ� ���� >> ������ �����
    public bool exitchk = false;
    public bool OnLine = false;
    public bool Clockchk = false;
    public bool IsQuest = false;
    public bool GetQuesting = false;
    int count; //�迭 üũ

    public bool onAngry = false;
    public bool onSmile = false;

    public int People; // ��� ����
    //Ai
    NavMeshAgent agent;
    NavMeshObstacle agent1; //�̰ɷ� ������ �������� ��ֹ��ǵ��� �ٲܲ���
    //spawnManager ���� ħ�� ���̺� �迭���� ������ @@@@@@@ �̰� ���� �ʹ� �����Ƽ� ���� ��ũ��Ʈ���� �ű⼭ �����ϰ����
    SpawnManager bedchairvalue;
    //���� ������� ���Ͱ�
    [SerializeField] Vector3 res;
    [SerializeField] Vector3 inn;
    [SerializeField] Vector3[] gotable;
    [SerializeField] Vector3 Exit;
    int purpose;

    [SerializeField] Vector3[] gobed;
    //FirstChecker
    public Transform myHost;

    public LayerMask layerMask;
   
    public enum STATE
    {
        Create, Idle, GoSide, Moving, Quest, Eat, Eating, Sleep, Sleeping, Exit
    }
    public STATE myState = default;
    
    public CharacterStat stat;
    
    //public CharState.NPC mystat; // ĳ���� �ɷ�ġ

    public Transform Line; //�ʱ� �� ���� �� ���� >> �տ� ��ǥ Ȯ�� >> X >> ���� ��ġ�� ���� >> �ݺ�

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle: // �κ񵥽�ũ�� �ͼ� ���̵���� ����
                agent.enabled = false;//�׺�޽� ��Ȱ��ȭ
                agent1.enabled = true;//�׺� �ɽ�ŸŬ Ȱ��ȭ //�̰ɷ� ������ �������� ��ֹ��ǵ��� �ٲܲ���
                StopAllCoroutines(); // ��� �ڷ�ƾ ���߰� ���
                switch (purpose)
                {
                    case 0:
                        StartCoroutine(deskLine());
                        break;
                    case 1:
                        //GetComponent<Animator>().SetBool("IsMoving", true);
                        //agent.SetDestination(res);
                        StartCoroutine(HostLine());
                        break;
                    case 2:
                        StartCoroutine(HostLine());
                        break;
                }
                if (!Clockchk)
                {
                    IconArea = GameObject.Find("IconArea");
                    objC = Instantiate(Resources.Load("IconPrefabs/ClockIcon"), IconArea.transform) as GameObject;
                    myUIC = objC.GetComponent<ClockIcon>();
                    myUIC.myTarget = myIconZone;

                    Clockchk = true;
                }
                //NpcClock�� ����

                break;

            case STATE.Moving: // - ����
                agent1.enabled = false; //�׺� �ɽ�ŸŬ ��Ȱ��ȭ
                agent.enabled = true; //�׺� �׺�޽� Ȱ��ȭ
                agent.ResetPath();
                switch (purpose)
                {
                    case 0:
                        ChangeState(STATE.Quest);
                        break;
                    case 1:
                        GetComponent<Animator>().SetBool("IsMoving", true);
                        agent.SetDestination(res);
                        StartCoroutine(HostLine());
                        StartCoroutine(WalkTo(STATE.Eat));
                        break;
                    case 2:
                        GetComponent<Animator>().SetBool("IsMoving", true);
                        agent.SetDestination(inn);
                        StartCoroutine(HostLine());
                        StartCoroutine(WalkTo(STATE.Sleep)); //�����ϸ�
                        break;
                }
                break;
            case STATE.Quest: //����ũ���� �̵��ϴ� ����
                //StartCoroutine(deskmoving()); - ����
                GetQuesting = true;
                StartCoroutine(deskLine());
                StartCoroutine(deskmoving());
                break;
            case STATE.Eat:
                myHost.rotation = Quaternion.Euler(0, 180, 0);
                StopAllCoroutines();
                GetComponent<Animator>().SetBool("IsMoving", false);

                IconArea = GameObject.Find("IconArea");
                Destroy(objC);
                obj = Instantiate(Resources.Load("IconPrefabs/MealIcon"), IconArea.transform) as GameObject;
                myUIM = obj.GetComponent<MealIcon>();
                myUIM.myTarget = myIconZone;
                obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoTable);
                break;
            case STATE.Eating:
                StopAllCoroutines();
                GetComponent<Animator>().SetTrigger("IsSeating");
                Invoke("GoExit", 5);
                //Invoke("SeatToWalk",11);
                break;

            case STATE.Sleep:
                myHost.rotation = Quaternion.Euler(0, 180, 0);
                StopAllCoroutines();
                GetComponent<Animator>().SetBool("IsMoving", false);

                IconArea = GameObject.Find("IconArea");
                Destroy(objC);
                obj = Instantiate(Resources.Load("IconPrefabs/BedIcon"), IconArea.transform) as GameObject;
                myUISleep = obj.GetComponent<SleepIcon>();
                myUISleep.myTarget = myIconZone;
                obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoBed);

                break;

            case STATE.Sleeping:
                StopAllCoroutines();
                GetComponent<Animator>().SetTrigger("IsLaying");
                Invoke("GoExit", 5);
                break;

            case STATE.Exit:
                transform.SetAsLastSibling();
                switch (purpose)
                {
                    case 0:
                        OnLine = false;
                        GetComponent<Animator>().SetBool("IsMoving", true);
                        agent1.enabled = false;
                        agent.enabled = true;
                        break;
                    case 1:
                        bedchairvalue.chairSloat[count] = SpawnManager.ChairSloat.NONE;
                        GetComponent<Animator>().SetBool("SitToStand", true);
                        agent1.enabled = false;
                        agent.enabled = true;
                        agent.ResetPath();
                        break;
                    case 2:
                        bedchairvalue.bedSloat[count] = SpawnManager.BedSloat.None;
                        GetComponent<Animator>().SetBool("LayToStand", true);
                        agent1.enabled = false;
                        agent.enabled = true;
                        agent.ResetPath();
                        break;
                }
                agent.SetDestination(Exit); // outpoint�� ���� �ڷ�ƾ
                break;  
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.Quest:
                if (IsQuest) transform.SetAsFirstSibling();
                break;
            case STATE.Eat:
                break;
            case STATE.Sleep:
                break;
            case STATE.Exit:
                break;
        }
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent1 = GetComponent<NavMeshObstacle>();


        bedchairvalue = FindObjectOfType<SpawnManager>();//GetComponent<SpawnManager>();
        inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        purpose = Random.Range(0, 3);
        ChangeState(STATE.Moving); 
    }
    public void GoBed() //�ֹ��ϰ� ħ��� �̵�
    {
        for (count = 0; count < 6; count++)
        {
            if (bedchairvalue._bedsloat[count] == SpawnManager.BedSloat.None)
            {
                GetComponent<Animator>().SetBool("IsMoving", true);
                agent.SetDestination(gobed[count]);
                StartCoroutine(BedToSleeping());
                bedchairvalue.bedSloat[count] = SpawnManager.BedSloat.Check;
                break;
            }
        }
    }
    public void GoTable() //�ֹ��ϰ� ���̺�� �̵�
    {
        for (count = 0; count < 12; count++)
        {
            if (bedchairvalue._chairsloat[count] == SpawnManager.ChairSloat.NONE)
            {
                GetComponent<Animator>().SetBool("IsMoving", true);
                agent.SetDestination(gotable[count]);
                StartCoroutine(EatToEating());
                bedchairvalue.chairSloat[count] = SpawnManager.ChairSloat.Check;
                break;
            }
        }
    }
    IEnumerator EatToEating() //���������� �����ϸ� Eat���¿��� Eating���·�
    {
        while (true)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= 1.0f)
                {
                    agent.velocity = Vector3.zero;
                    ChangeState(STATE.Eating);
                    agent.ResetPath();
                    if (count % 2 == 0)
                    {
                        myHost.rotation = Quaternion.Euler(0, 270, 0);
                    }
                    else if (count % 2 == 1)
                    {
                        myHost.rotation = Quaternion.Euler(0, 90, 0);
                    }
                }
            }
            yield return null;
        }
    }
    IEnumerator deskmoving()
    {
        OnLine = true;
        Vector3 dir = Line.position - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        GetComponent<Animator>().SetBool("IsMoving", true);
        while (dist > 0.0f)
        {
            float delta = stat.moveSpeed * Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }
    }
    IEnumerator BedToSleeping() //���������� �����ϸ� Eat���¿��� Eating���·�
    {
        while (true)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= 1.0f)
                {
                    agent.velocity = Vector3.zero;
                    ChangeState(STATE.Sleeping);
                    agent.ResetPath();
                    myHost.rotation = Quaternion.Euler(0, 270, 0);
                }
            }
            yield return null;
        }
    }
    IEnumerator WalkTo(STATE HostState) // �����ϸ� Walk���¿��� Eat�̳� ���� ���·�
    {
        while (true)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance == 0)
                {
                    agent.velocity = Vector3.zero;
                    ChangeState(HostState);
                    agent.ResetPath();
                }
            }
            yield return null;
        }
    }
    /*IEnumerator OutMoving() //������ �ڷ�ƾ
    {
        Vector3 rot = (OutPoint.position - transform.position).normalized;
        float Angle = Vector3.Angle(transform.forward, rot);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, rot) < 0.0f)
        {
            rotDir = -rotDir;
        }
        while (Angle > 0.0f)
        {
            float delta = stat.rotSpeed * Time.deltaTime;
            if (delta > Angle)
            {
                delta = Angle;
            }
            Angle -= delta;
            transform.Rotate(Vector3.up * rotDir * delta, Space.World);
        }

        Vector3 dir = OutPoint.position - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        GetComponent<Animator>().SetBool("IsMoving", true);
        while (dist > 0.0f)
        {
            float delta = stat.moveSpeed * Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }
    }*/


    // Update is called once per frame
    void Update()
    {
        StateProcess();

        /*if (OnLine)
        {
            // �ټ���
            if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.back * 10, out RaycastHit hitinfo, 10.0f, layerMask)) // �տ� ���谡�� ������
            {
                GetComponent<Animator>().SetBool("IsMoving", false); // �����
                ChangeState(STATE.Idle);
                
            }
            else
            {
                ChangeState(STATE.Quest);
            }
        }*/

        // ����Ʈ,�Ҹ� ������ ����
        if (myState == STATE.Idle)
        {
            if (myUIC.myTarget != null && myUIC == null && myUIC.TimeOut == false) // �ð踦 ���� �ð��ȿ� ������ ����Ʈ ������ ����
            {
                StartCoroutine(onQuestIcon());
                myUIC.myTarget = null;
            }
            else if (myUIC.myTarget != null && myUIC == null && myUIC.TimeOut == true) // �ð踦 ���� �ð��ȿ� �ȴ����� �Ҹ� ������ ����
            {
                StartCoroutine(onAngryIcon());
                myUIC.myTarget = null;
            }
            else if(onAngry == true) // ����Ʈ�� �����ϸ� �Ҹ� ������ ����
            {
                StartCoroutine(onAngryIcon());
                onAngry = false;
            }
            else if(onSmile == true)
            {
                StartCoroutine(onSmileIcon());
                onSmile = false;
            }
        }
    }
    IEnumerator deskLine()//���̾�� �ټ����
    {
        while (true)
        {
            if (OnLine)
            {
                Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.black);
                if (Physics.SphereCast(transform.position, 5.0f, transform.forward , out RaycastHit hitinfo, 5.0f, layerMask))
                {
                    GetComponent<Animator>().SetBool("IsMoving", false);
                    ChangeState(STATE.Idle);
                }
                else
                {
                    ChangeState(STATE.Quest);
                } 
            }
            yield return null;
        }
    }

    IEnumerator HostLine() //�ټ���
    {

        while (true)
        {
            if (Physics.SphereCast(transform.position, 5.0f, transform.forward, out RaycastHit hitinfo1, 10.0f, layerMask))
            {
                GetComponent<Animator>().SetBool("IsMoving", false);
                agent.velocity = Vector3.zero;
                ChangeState(STATE.Idle);
                //agent.ResetPath();
            }
            else
            {
                ChangeState(STATE.Moving);
            }
            yield return null;
        }
    }

    IEnumerator onQuestIcon()
    {
        yield return new WaitForSeconds(1.5f);

        GameObject objQ = Instantiate(Resources.Load("IconPrefabs/QuestIcon"), IconArea.transform) as GameObject;
        myUIQ = objQ.GetComponent<QuestIcon>();
        myUIQ.myTarget = myIconZone;
    }
    IEnumerator onAngryIcon()
    {
        yield return new WaitForSeconds(1.0f);

        GameObject objA = Instantiate(Resources.Load("IconPrefabs/AngryIcon"), IconArea.transform) as GameObject;
        myUIA = objA.GetComponent<AngryIcon>();
        myUIA.myTarget = myIconZone;
        IsFinishQuest = true; // �Ҹ��� �����Բ�
        StopAllCoroutines();
        IsQuest = false;
        ChangeState(STATE.Exit);
    }

    IEnumerator onSmileIcon() // ������ ������
    {
        yield return new WaitForSeconds(1.0f);

        GameObject objS = Instantiate(Resources.Load("IconPrefabs/SmileIcon"), IconArea.transform) as GameObject; 
        myUIS = objS.GetComponent<SmileIcon>();
        myUIS.myTarget = myIconZone;
        if (hostchk)
        {
            IsFinishQuest = true;
            
        }
        StopCoroutine(deskLine());
        IsQuest = false;
        ChangeState(STATE.Exit);
    }
    IEnumerator FinishQuest(float t) //����Ʈ ���� ���� ����Ʈ�� �ϵ���
    {
        yield return new WaitForSeconds(t);
        SpawnManager.Instance.Teleport(gameObject);
        IsFinishQuest = true;
        OnLine = false;
        Clockchk = false;
        ChangeState(STATE.Quest);
    }
    public void RemoveNotouch()
    {
        myUIC.myNotouch.SetActive(false);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (!OnLine)
        {

            if (IsFinishQuest) // �Ϸ���¿��� Ʈ���ſ� ������ �ı� ī��Ʈ ����
            {
                Destroy(gameObject);
                SpawnManager.Instance.hostCount--;
            }
            else
            {
                StartCoroutine(FinishQuest(3.0f)); //����Ʈ �Ϸ� �ð�
            }
        }
        else { IsQuest = true; }
    }

    public void GoExit() //�Դ»��¿��� ������ ���·�
    {
        ChangeState(STATE.Exit);
        IsFinishQuest = true;
    }
}