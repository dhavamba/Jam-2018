using UnityEngine;
using Rewired;
using System;

//The class is an interface between Rewired and the Game
public class InterfaceInput : Singleton<InterfaceInput>
{
    private Player player;

    public Player Player
    {
        get
        {
            return player;
        }
    }

    // Use this for initialization
    private void Awake ()
    {
        player = ReInput.players.GetPlayer(0);
    }

}
