using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OppHealth : MonoBehaviour
{
    public Slider opHealth;
    public int minOpHP = 0;
    public int maxOpHP = 100;
    public int currentOpHP;

    private bool Dead;

    void Start ()
    {
        currentOpHP = maxOpHP;
        opHealth = GameObject.FindGameObjectWithTag("enemyhealth").GetComponent<Slider>();
        Dead = false;
	}
	

	void Update ()
    {
		if(Dead == true)
        {
            return;
        }
        if(currentOpHP < minOpHP)
        {
            currentOpHP = minOpHP;
        }
        if(currentOpHP >= 50)
        {
            SendMessage("ChangeTactics2");
        }
        if(currentOpHP == minOpHP)
        {

            Dead = true;
            GetComponent<OpponentAI>().enabled = false;
            SceneManager.LoadScene("Win");
            SendMessage("SetOpDeath", SendMessageOptions.DontRequireReceiver);
        }

        opHealth.value = currentOpHP;

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == ("LightHit"))
        {
            Debug.Log("YOU'RE HITTING");
            SendMessage("hitLight");
        }
    }

    private void hitLight()
    {
        Debug.Log("Minus health");
        currentOpHP -= 15;
    }
    private void hitheavy()
    {
        currentOpHP -= 25;
    }
}
