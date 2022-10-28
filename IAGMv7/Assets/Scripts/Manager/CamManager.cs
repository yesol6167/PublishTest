using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CamManager : Singleton<CamManager>
{
    public Transform myAxis;
    public Transform myCam;

    public float MoveSpeedK = 1.0f; // Ű���� �̵� ���ǵ�
    public float MoveSpeedM = 1.0f; // ���콺 �̵� ���ǵ�
    public float ZoomSpeed = 1.0f;
    public float RotSpeed = 1.0f;

    //��
    Vector3 zoom = Vector3.zero;
    float SetDist = 70f; // �ʱ� �� �Ÿ��� 70
    public Vector2 ZoomRange = new Vector2(10, 10); // �� ����

    //ȭ�� ȸ��
    Vector3 Rot = Vector3.zero;

    

    // Start is called before the first frame update
    void Start()
    {
        zoom = (myCam.position - myAxis.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //ȭ���̵�
        if(true)
        {
            // Ű����� �̵�
            if (Input.GetKey(KeyCode.W))
            {
                myAxis.transform.Translate(Vector3.back * MoveSpeedK * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                myAxis.transform.Translate(Vector3.forward * MoveSpeedK * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                myAxis.transform.Translate(Vector3.right * MoveSpeedK * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                myAxis.transform.Translate(Vector3.left * MoveSpeedK * Time.unscaledDeltaTime);
            }

            // ���콺�� �̵�
            if(Input.GetMouseButton(1))
            {
                Vector3 dir = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
                myAxis.transform.Translate(dir * MoveSpeedM * Time.unscaledDeltaTime);
            }
            
            //�̵�����(�ʿ��� ����� �ʰ�)
            float x = Mathf.Clamp(myAxis.transform.position.x, -68, 88); // ���� ���� ��
            float z = Mathf.Clamp(myAxis.transform.position.z, -47, 51); // ���� ���� ��

            myAxis.transform.position = new Vector3(x, myAxis.transform.position.y, z); // ����,���� ���� ����
        }

        //ȭ��ȸ��
        if (Input.GetKeyDown(KeyCode.Q))
        {
            myAxis.transform.Rotate(Vector3.up * RotSpeed); // �ν����Ϳ��� �����Ϸ��� ���ǵ�� �� //  ��            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            myAxis.transform.Rotate(Vector3.down * RotSpeed);            
        }


        //��
        SetDist -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        SetDist = Mathf.Clamp(SetDist, ZoomRange.x, ZoomRange.y);
        myCam.position = myAxis.position + myAxis.rotation * zoom * SetDist;
    }
}
