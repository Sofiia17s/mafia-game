using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using TMPro;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class TextController : MonoBehaviour
{
    public TMP_Text RoleText;

    private int playerNumber;
    private string playerRole;

    private Dictionary<int, string> rolesByNumber = new Dictionary<int, string>();

    AudioSource audioSource;

    private TimerController timerController;

    private int currentPlayer = 1;

    private float mafiaTime = 60f;

    private VotеController voteController;

    public void StartNightPhase()
    {
        StartCoroutine(NightPhase());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        RoleText = GameObject.Find("text").GetComponent<TMP_Text>();
        timerController = FindFirstObjectByType<TimerController>();

        GenerateRolesForAllPlayers();
        StartCoroutine(ShowRoles());

        voteController = FindFirstObjectByType<VotеController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateRolesForAllPlayers()
    {
        List<string> roles = new List<string>()
        {
            "Мирний", "Мирний", "Мирний", "Мирний", "Мирний", "Мирний",
            "Мафія", "Мафія",
            "Шериф",
            "Дон"
        };

        // mix
        for (int i = 0; i < roles.Count; i++)
        {
            int rand = Random.Range(i, roles.Count);
            (roles[i], roles[rand]) = (roles[rand], roles[i]);
        }

        // call
        for (int i = 0; i < roles.Count; i++)
        {
            rolesByNumber[i + 1] = roles[i];
        }
    }

    IEnumerator ShowRoles()
    {
        // Start
        RoleText.text = "Роздача ролей";
        yield return new WaitForSeconds(5f);

        // Role
        for (int i = 1; i <= rolesByNumber.Count; i++)
        {
            RoleText.text = i + " - " + rolesByNumber[i];

            yield return new WaitForSeconds(4f);

            audioSource.Play();

            yield return new WaitForSeconds(1f);
        }

        // Mafia minute

        RoleText.text = "Хвилина мафії";

        for (int i = 0; i < 3; i++)
        {
            audioSource.PlayOneShot(audioSource.clip);
            yield return new WaitForSeconds(audioSource.clip.length + 0.1f);
        }

        yield return new WaitForSeconds(5f);

        // minute
        yield return new WaitForSeconds(60f);

        //voteController.ShowButtons();

        // Day
        RoleText.text = "Промова міста, щоб говорити натисніть \"Таймер\"";

    }

    IEnumerator NightPhase()
    {
        RoleText.text = "В місті ніч";

        yield return new WaitForSeconds(10f);

        RoleText.text = "";
    }

    public IEnumerator NightPhaseWithSound(AudioSource source)
    {
        RoleText.text = "В місті ніч";

        yield return new WaitForSeconds(10f);

        source.Play();

        RoleText.text = "";
    }
    /*
    public void RemovePlayer(int playerNumber)
    {
        if (rolesByNumber.ContainsKey(playerNumber))
        {
            Debug.Log("Вибув гравець " + playerNumber + " роль: " + rolesByNumber[playerNumber]);
            rolesByNumber.Remove(playerNumber);
        }
    }

    public void StartVotingPhase()
    {
        StartCoroutine(VotingPhase());
    }

    IEnumerator VotingPhase()
    {
        RoleText.text = "Хвилина голосування, натисніть на номер ймовірної мафії";

        voteController.ShowButtons();

        timerController.StartClicked();

        yield return new WaitForSeconds(60f);

        voteController.HideButtons();

        StartNightPhase();
    }
    */
}