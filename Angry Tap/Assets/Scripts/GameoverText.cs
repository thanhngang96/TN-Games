using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameoverText : MonoBehaviour
{
    Text gameover;

    void OnEnable()
    {
        int scoreRank = PlayerPrefs.GetInt("ScoreRank");
        gameover = GetComponent<Text>();
        if (scoreRank == 0)
            gameover.text = "Wake up!!!";
        else if (scoreRank <= 5)
            gameover.text = "LULLLLLL";
        else if (scoreRank <= 10)
            gameover.text = "C'mon!";
        else if (scoreRank <= 20)
            gameover.text = "You can do better!";
        else if (scoreRank <= 30)
            gameover.text = "Not bad!";
        else if (scoreRank <= 50)
            gameover.text = "Nice try!";
        else if (scoreRank <= 100)
            gameover.text = "You are insane!";
        else if (scoreRank <= 200)
            gameover.text = "Mother$%@#";
        else
            gameover.text = "Holy&*$#^$^@";
    }
}
