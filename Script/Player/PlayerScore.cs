using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    static int score;
    static int death;
    public static void AddPoint()
    {
        PlayerScore.score ++;
    }
    public static void AddDeath()
    {
        PlayerScore.death ++;
    }
    public static int GetScore()
    {
        return PlayerScore.score;
    }
    public static int GetDeath()
    {
        return PlayerScore.death;
    }
    public static int GetTotal()
    {
        float total = PlayerScore.score  - PlayerScore.death ;
        return (int)Mathf.Clamp(total, 0, Mathf.Infinity)*5;
    }
    public static void Reset()
    {
        PlayerScore.death = 0;
        PlayerScore.score = 0;
    }
    
}
