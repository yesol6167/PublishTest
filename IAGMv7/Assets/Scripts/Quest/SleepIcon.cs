using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class SleepIcon : MonoBehaviour
{
    public GameObject Host;
    public GameObject Icon;
    public Transform myTarget;
    public Transform myIconZone;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
        transform.position = pos;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
}
