using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour
{
    public GameGenerator gameGenerator;
    public GameObject model;
    public int constant;
    public int percentage;
    public int protectTime;
    private GameObject[] littlePlanets;
    private bool dangerHit;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        dangerHit = false;
    }
    private void Update()
    {
        if (count < protectTime)
        {
            count++;
            model.GetComponent<MyAnimator>().anime.SetBool("IsDamage", false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DangerObject")
        {
            if (count == protectTime)
            {
                if (SystemInfo.supportsVibration)
                {
                    Handheld.Vibrate();
                }
                if (gameGenerator.star <= 0)
                {
                    SceneManager.LoadScene("ResultScene");
                }
                littlePlanets = GameObject.FindGameObjectsWithTag("MiniStar");
                int length = constant + littlePlanets.Length / percentage;
                if (length > littlePlanets.Length)
                {
                    length = littlePlanets.Length;
                }

                for (int i = 0; i < length; i++)
                {
                    Destroy(littlePlanets[i]);
                    gameGenerator.star--;
                }
                count = 0;
                dangerHit = true;
                //gameObject.GetComponent<PlayerMove>().anime.SetBool("IsDamage", true);
                model.GetComponent<MyAnimator>().anime.SetBool("IsDamage", true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GirigiriZone")
        {
            if (!dangerHit)
            {
                if (count == protectTime)
                {
                    gameGenerator.addSpeedTimer = 3;
                }
            }
            else
            {
                dangerHit = false;
            }
        }
    }
}
