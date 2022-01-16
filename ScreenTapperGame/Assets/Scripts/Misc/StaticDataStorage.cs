using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticDataStorage
{
    public static int Highscore
    {
        get { return highscore; }
        set 
        { 
            if(value > highscore)
            {
                highscore = value;
                PlayerPrefs.SetInt("Highscore", highscore);
                PlayerPrefs.Save();
            }
        }
    }
    static int highscore;
}
