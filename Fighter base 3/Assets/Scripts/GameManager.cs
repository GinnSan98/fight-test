using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script made by Connor-Sama 22/08/2018
public class GameManager : MonoBehaviour
{

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
	
	void Start ()
    {
        DontDestroyOnLoad(this);

	}
	

	void Update ()
    {
		

	}
}
