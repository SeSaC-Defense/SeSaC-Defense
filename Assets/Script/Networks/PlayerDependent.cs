using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerDependent : NetworkBehaviour
{
    protected Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

}
