using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Made by Connor
public class CCManager : MonoBehaviour
{
    public static bool bee;
    public static bool badger;
    public static bool dragon;
    public static bool bat;
    public static bool unknown1;
    public static bool unknown2;
    public static bool unknown3;
    public static bool unknown4;
    public static bool unknown5;
    public static bool unknown6;

    void Awake()
    {
        bee = false;
        badger = false;
        dragon = false;
        bat = false;
        unknown1 = false;
        unknown2 = false;
        unknown3 = false;
        unknown4 = false;
        unknown5 = false;
        unknown6 = false;

    }
    void Start ()
    {
        DontDestroyOnLoad(this);

	}
	
	void Update ()
    {
		

	}
}
