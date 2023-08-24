using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerCollectCoin_IncreaseScore()
    {
        //Ambiente do teste: 1 moeda, 1 player, executa fun��o de coletar moeda
        CollectibleInternal _newCollectible = new CollectibleInternal();
        IPlayerController playerController = new PlayerController();

        _newCollectible.HandleCollect(playerController);

        //Ap�s coletar uma moeda, verifica se resultado esperado(1) � igual ao obtido(score)
        Assert.AreEqual(1, playerController.score);
    }

}

