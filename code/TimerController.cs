using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float StartTime; //Timer will count down from this value
    private float TimeLeft;
    private bool soundPlayed = false;//s2

    public TMP_Text TimerText;
    public Button StartButton;

    private TextController textController;

    private int timerFinishedCount = 0;
    private VotåController voteController;

    private int nightCount = 0;
    private const int MaxNights = 6;

    AudioSource audioSource; //s2

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textController = FindFirstObjectByType<TextController>();
        voteController = FindFirstObjectByType<VotåController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLeft > 0)
        {
            //Time tick down
            TimeLeft -= Time.deltaTime;
            TimerText.text = TimeLeft.ToString("0.00");
        }
        else
        {
            //Timer Finished
            if (!soundPlayed)
            {
                audioSource.Play();
                soundPlayed = true;

                timerFinishedCount++;

                if (timerFinishedCount >= 12)
                {
                    timerFinishedCount = 0;

                    if (nightCount < MaxNights)
                    {
                        nightCount++;
                        StartCoroutine(textController.NightPhaseWithSound(audioSource));
                    }
                    else
                    {
                        textController.RoleText.text = "Â³òàþ ç ïåðåìîãîþ";
                        StartButton.interactable = false;
                    }
                }
            }

            StartButton.gameObject.SetActive(true);
            TimerText.gameObject.SetActive(false);
            //Game over screen or retry or show player stats etc can go here!
        }
    }
    public void StartClicked()
    {
        if (TimeLeft > 0)
        {
            TimeLeft = 0;
        }
        else
        {
            TimeLeft = StartTime;
            soundPlayed = false; //s2

            //StartButton.gameObject.SetActive(false);
            TimerText.gameObject.SetActive(true);
        }
    }

    IEnumerator NightSequence()
    {
        textController.RoleText.text = "Â ì³ñò³ í³÷";

        yield return new WaitForSeconds(10f);

        audioSource.Play();

        textController.RoleText.text = "";
    }
}