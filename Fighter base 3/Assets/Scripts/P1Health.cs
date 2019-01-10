using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class P1Health : MonoBehaviour
{
    public Slider health;
    public int minPOneHP = 0;
    public int maxPOneHP = 100;
    public int currentPOneHP;

    private bool Dead;

    void Start()
    {
        currentPOneHP = maxPOneHP;
        health = GameObject.FindGameObjectWithTag("playerhealth").GetComponent<Slider>();
        Dead = false;
    }



    void Update()
    {
        if (Dead == true)
        {
            return;
        }
        if (currentPOneHP < minPOneHP)
        {
            currentPOneHP = minPOneHP;
        }
        if (currentPOneHP == minPOneHP)
        {
            Dead = true;
            GetComponent<P1Movement>().enabled = false;
            SceneManager.LoadScene("Lose");
            SendMessage("SetPOneDead", SendMessageOptions.DontRequireReceiver);
        }
        health.value = currentPOneHP;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == ("LightHit"))
        {
            Debug.Log("YOU'RE HITTING PLAYER");
            SendMessage("hitLightPlayer");
        }
    }
    private void hitLightPlayer()
    {
        Debug.Log("Minus health");
        currentPOneHP -= 1;
    }
}



