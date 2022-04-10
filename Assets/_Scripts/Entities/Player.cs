using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    private int id;
    private string playerName;
    private List<Card> hand;
    private bool isMaster;

    public Player(string playerName)
    {
        this.playerName = playerName;
    }

    public int Id { get => id; set => id = value; }
    public string PlayerName { get => playerName; set => playerName = value; }
    public List<Card> Hand { get => hand; set => hand = value; }
    public bool IsMaster { get => isMaster; set => isMaster = value; }

}
