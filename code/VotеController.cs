using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VotеController : MonoBehaviour
{
    public GameObject[] VoteButtons;

    private List<int> votedNumbers = new List<int>();
    public TMP_Text RoleText;

    private TextController textController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RoleText = GameObject.Find("text").GetComponent<TMP_Text>();
        HideButtons();
        textController = FindFirstObjectByType<TextController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartVoting()
    {
        Debug.Log("StartVoting викликаний");

        RoleText.text = "Хвилина голосування, натисніть на номер ймовірної мафії";

        foreach (GameObject button in VoteButtons)
        {
            button.SetActive(true);
        }
    }

    public void ShowButtons()
    {
        foreach (GameObject button in VoteButtons)
        {
            button.SetActive(true);
        }
    }

    public void HideButtons()
    {
        foreach (GameObject button in VoteButtons)
        {
            button.SetActive(false);
        }
    }


    /*
        public void Vote(int number)
        {
            votedNumbers.Add(number);

            VoteButtons[number - 1].SetActive(false);

            textController.RemovePlayer(number);

            Debug.Log("Проголосували за номер " + number);
        }

        public void Vote(GameObject button, int number)
        {
            votedNumbers.Add(number);

            button.SetActive(false);

            Debug.Log("Проголосували за номер " + number);
        }
    */
}