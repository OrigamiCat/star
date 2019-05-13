using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerator : MonoBehaviour
{
    public int star, maxstar;
    public float distance, speed, addSpeedTimer;
    public int musicCnt;
    [SerializeField]
    private float[] objectSpeed;
    public int[] musicChangeCount
    {
        get; set;
    }
    public static int Star
    {
        get; set;
    }
    public static float StageTimer
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        maxstar = 0;
        star = 0;
        musicCnt = 0;
        musicChangeCount = new int[7];
        musicChangeCount[0] = 0;
        musicChangeCount[1] = 30;
        musicChangeCount[2] = 60;
        musicChangeCount[3] = 100;
        musicChangeCount[4] = 150;
        musicChangeCount[5] = 200;
        musicChangeCount[6] = 250;
        addSpeedTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        Controller();

        if (addSpeedTimer > 0)
        {
            addSpeedTimer -= 1 / 60f;
        }
        else
        {
            addSpeedTimer = 0; 
        }
                    
        speed *= SpeedPoint(1 - addSpeedTimer / 3.0f);

        TimeGenerator timeGenerator = GetComponent<TimeGenerator>();

        StageTimer += Time.deltaTime;
        if (maxstar <= star)
        {
            maxstar = star;
            Star = maxstar;
        }
    }
    private void Controller()
    {
        if (star >= musicChangeCount[0] && star < musicChangeCount[1])
        {
            musicCnt = 0;
            speed = objectSpeed[0];
        }
        else if (star < musicChangeCount[2])
        {
            speed = objectSpeed[1];
            musicCnt = 1;
        }
        else if (star < musicChangeCount[3])
        {
            speed = objectSpeed[2];
            musicCnt = 2;
        }
        else if (star < musicChangeCount[4])
        {
            speed = objectSpeed[3];
            musicCnt = 3;
        }
        else if (star < musicChangeCount[5])
        {
            speed = objectSpeed[4];
            musicCnt = 4;
        }
        else if (star < musicChangeCount[6])
        {
            speed = objectSpeed[5];
            musicCnt = 5;
        }
        else
        {
            speed = objectSpeed[6];
        }
    }
    private float SpeedPoint (float addTimer)
    {
        var point1 = Vector3.Lerp(new Vector2(0, 1), new Vector2(0.3f, 2f), addTimer);
        var point2 = Vector3.Lerp(new Vector2(0.3f, 2f), new Vector2(1, 1), addTimer);
        var point3 = Vector3.Lerp(point1, point2, addTimer);
 
        return point3.y;
    }
}
