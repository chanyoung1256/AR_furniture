using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowMe : MonoBehaviour
{
    Transform target;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        Vector3 posTemp = target.position;
        Vector3 posTemp2 = transform.position;
        posTemp.y = 0;
        posTemp2.y = 0;
        float distance = Math.Abs(Vector3.Distance(posTemp2, posTemp));
        if (distance > 2f)
        {
            speed = 0.5f;
        }
        else if (distance > 0.5f)
        {
            speed = 0.25f;
        }
        else
        {
            speed = 0f;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
    }
}
