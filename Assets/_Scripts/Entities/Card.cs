using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card 
{
    private int rank;
    private int suite;
    private bool isSelected;
    public Card(int Rank, int Suite)
    {
        rank = Rank;
        suite = Suite;
    }
    public int Rank { get => rank; }
    public int Suite { get => suite; }

}
