using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountdownText : MonoBehaviour
{
    public delegate void CountdownFinished();
    public static event CountdownFinished OnCountdownFinished;

    //public AudioSource countdownAudio;
    public AudioSource threeAudio;
    public AudioSource twoAudio;
    public AudioSource oneAudio;
  

    Text countdown;

    void OnEnable()
    {
        countdown = GetComponent<Text>();
        countdown.text = "3";
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        //int count = 3;
        //for (int i = 0; i < count; i++)
        //{
        //    countdown.text = (count - i).ToString();
        //    countdownAudio.Play();
        //    yield return new WaitForSeconds(1);
        //}
        threeAudio.Play();
        yield return new WaitForSeconds(1);

        countdown.text = "2";
        twoAudio.Play();
        yield return new WaitForSeconds(1);

        countdown.text = "1";
        oneAudio.Play();  
        yield return new WaitForSeconds(1);
        OnCountdownFinished();
    }
}
