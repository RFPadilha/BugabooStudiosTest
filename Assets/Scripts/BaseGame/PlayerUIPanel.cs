using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIPanel : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerScore;
    public int localScore = 0;

    PlayerController player;
    public void AssignPlayer(int index)
    {
        StartCoroutine(AssignPlayerDelay(index));
    }

    IEnumerator AssignPlayerDelay(int index)
    {
        yield return new WaitForSeconds(.5f);
        player = GameManager.instance.playerList[index].GetComponent<PlayerInputHandler>().playerController;

        SetUpInfoPanel();
    }
    void SetUpInfoPanel()
    {
        if (player != null)
        {
            player.OnScoreChanged += UpdateScore;
            playerName.text = ($"Player: {player.thisPlayersName.ToString()}");
        }
    }
    void UpdateScore(int score)
    {
        localScore = score;
        playerScore.text = ($"Score: {score}");
    }
}
