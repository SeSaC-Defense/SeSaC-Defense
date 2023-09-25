using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerPossession : NetworkBehaviour
{
    private Player player;

    public int PlayerNo => player.PlayerNo;
    protected Player Player => player;

    protected void SetPlayer(Player player)
    {
        this.player = player;
    }
}
