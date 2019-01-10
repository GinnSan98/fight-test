using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class artManager : MonoBehaviour
{

    public string BG = "";
    public int BGCount;

    public string[] BGStages = new string[]
    {
        "Training",
        "WIP"
    };

	void Start ()
    {
        DontDestroyOnLoad(this);                        //You want this script active when you load a new scene
        BGCount = 0;           
        
	}
	

	void Update ()    
    {
		
	}

    private void LevelBGManager()
    {
        Debug.Log("Level Managed");
        if(BGCount < BGStages.Length)
        {
            BGCount++;
        }
        if (BGCount == BGStages.Length)
        {
            BGCount = 0;
        }

        BG = BGStages[BGCount];
    }

    private void BGLoad()
    {
        Debug.Log("BGLoad");
        LevelBGManager();

        SceneManager.LoadScene(BG);
    }
}
