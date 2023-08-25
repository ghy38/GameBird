using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs
{
    public static int bestScore
    {
        //Lay diem so cao nhat o trong bo nho
        get => PlayerPrefs.GetInt(GameConst.BEST_SCORE, 0);
        set
        {
            int curScore = PlayerPrefs.GetInt(GameConst.BEST_SCORE, 0);

            //Dat lai diem so neu lon hon diem so trong bo nho
            if(value > curScore)
            {
                PlayerPrefs.SetInt(GameConst.BEST_SCORE, value);
            }
        }
    }
}
