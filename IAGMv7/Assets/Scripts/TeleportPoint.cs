using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    //public static TeleportPoint inst = null;
    public Transform QuestPoint;

    public GameObject teleportPos;

    /*private void Awake()
    {
        inst = this;
    }*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider obj)
    {
        obj.transform.Rotate(Vector3.up * 180);
        this.Teleport(obj.gameObject);
        obj.transform.parent = QuestPoint.transform;
    }


    void Teleport(GameObject host)
    {
        host.transform.position = this.teleportPos.transform.position;
    }
}