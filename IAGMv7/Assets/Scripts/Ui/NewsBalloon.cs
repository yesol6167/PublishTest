using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NewsBalloon : MonoBehaviour
{
    float NewsTime;
    
    void Update()
    {
        NewsTime += Time.deltaTime;
        if (NewsTime > 1.5f) // 1.5�� ���� ���̰� �����
        {
            Destroy(gameObject);
        }
    }
}
