using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public PlayerController playerController;

    private void Awake()
    {
        if(playerPrefabs != null)
        {
            playerController = Instantiate(playerPrefabs[GetComponent<PlayerInput>().playerIndex], GameManager.instance.spawnPoints[GameManager.instance.playerList.Count-1].transform.position, transform.rotation).GetComponent<PlayerController>();
            transform.parent = playerController.transform;
            transform.position = playerController.transform.position;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        playerController.OnMove(context);
    }
}
