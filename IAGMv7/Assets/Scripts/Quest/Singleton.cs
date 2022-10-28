using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class GameManager : Singleton<GameManager> ���·� ���
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance; // �̱��� ������ ����ϱ� ���� �ν��Ͻ� ����

    public static T Instance // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    {
        get 
        {
            if(instance == null) // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            {
                instance = (T)FindObjectOfType(typeof(T)); // �Ȱ��� ���� �ִ��� �ѹ��� Ȯ��

                if(instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T)); // ���ٸ� ����
                    instance = obj.GetComponent<T>(); 
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(transform.parent != null && transform.root != null) // �θ� �ֻ��� �����ȿ� ���� ���
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
