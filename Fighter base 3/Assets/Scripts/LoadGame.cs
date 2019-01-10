using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public bool onlyOnce = false;


	void Start ()
    {
     
            GameObject.FindGameObjectWithTag("OnePlayerManager").GetComponent<LoadingChar>().SendMessage("LoadPOne");

     
            GameObject.FindGameObjectWithTag("OpManager").GetComponent<Opponent>().SendMessage("LoadCurrentOp");
            
        
       
                
    }
	

	void Update ()
    {
		
	}
}
