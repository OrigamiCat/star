﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 gyro = Vector3.zero, gyroSet = Vector3.zero, moveVec, velocitySet;
    private Quaternion rotation, inclination, inclinationSet = Quaternion.identity;
    private Rigidbody playerRB;
    public float speed, slowdown;
    public DebugText debugText;

    public float MySpeed;
    private Vector3 target;

    public List<Vector3> PosList
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        //ジャイロセンサーOn！
        Input.gyro.enabled = true;
        gyroSet = Input.gyro.gravity;
        playerRB = GetComponent<Rigidbody>();
        moveVec = Vector3.zero;
        playerRB = GetComponent<Rigidbody>();
        target = Vector3.zero;
        PosList = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        //////重力感知
        //gyro.x = Mathf.Clamp(Input.gyro.gravity.x, -1.0f, 1.0f);
        //gyro.y = Mathf.Clamp(Input.gyro.gravity.y - gyroSet.y, -1.0f, 1.0f);
        //velocitySet = playerRB.velocity;
        //if (moveVec.x < 0 && gyro.x > 0 || moveVec.x > 0 && gyro.x < 0)
        //{
        //    velocitySet.x /= slowdown;
        //}
        //if (moveVec.y < 0 && gyro.y > 0 || moveVec.y > 0 && gyro.y < 0)
        //{
        //    velocitySet.y /= slowdown;
        //}
        //playerRB.velocity = velocitySet;
        //playerRB.AddForce(gyro * speed);
        //moveVec = gyro;
        //debugText.GetComponent<DebugText>().debugVec3 = gyro;

        //傾き感知
        //inclination = Input.gyro.attitude;
        //inclination = new Quaternion(-inclination.x, -inclination.y, inclination.z, inclination.w);

        //inclination.x = Mathf.Floor(inclination.x * 100) / 100;
        //inclination.y = Mathf.Floor(inclination.y * 100) / 100;
        //inclination.z = Mathf.Floor(inclination.z * 100) / 100;
        //inclination.w = Mathf.Floor(inclination.w * 100) / 100;

        //rotation = Quaternion.Euler(inclination.eulerAngles + (inclination.eulerAngles - inclinationSet.eulerAngles));
        //rotation.y *= 0.0f;
        //transform.rotation = rotation;
        ////transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f) * (inclination);

        //debugText.GetComponent<DebugText>().debugQuater = inclination;
        //if (0 < Input.touchCount)
        //{
        //    //transform.rotation = Quaternion.identity;
        //}
        if (Input.GetMouseButton(0))
        {
            target = GetMousePosition();
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;

        }
        Vector3 pos = target - transform.position;
        pos = pos.normalized * speed;
        playerRB.AddForce(pos);
        if (count != 10)
        {
            count++;
        }
        else
        {
            PosList.Add(transform.position);
            if (PosList.Count > 200)
            {
                PosList.RemoveAt(0);
            }
            count = 0;
        }
    }
    int count = 0;
    Vector3 GetMousePosition()
    {
        Vector3 v3 = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(v3);
        RaycastHit hit;
        int layerMask = (1 << LayerMask.NameToLayer("Water"));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            v3 = hit.point;
        }

        return v3;
    }
}
