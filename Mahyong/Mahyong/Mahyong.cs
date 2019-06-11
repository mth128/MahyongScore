using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mahyong
{

  public enum PlayerIndex
  {
    player1 = 0, player2 = 1, player3 = 2, player4 = 3
  }

  public class Mahyong
  {
    public static Mahyong Main = new Mahyong(); 

    public string Player1 { get; set; } = "Player 1";
    public string Player2 { get; set; } = "Player 2";
    public string Player3 { get; set; } = "Player 3";
    public string Player4 { get; set; } = "Player 4";

    public List<MahyongRound> Rounds { get; } = new List<MahyongRound>();

    internal Label PlayerLabel(PlayerIndex player)
    {
      string name = "";
      switch(player)
      {
        case PlayerIndex.player1:
          name = Player1;
          break;
        case PlayerIndex.player2:
          name = Player2;
          break;
        case PlayerIndex.player3:
          name = Player3;
          break;
        case PlayerIndex.player4:
          name = Player4;
          break;
      }

      return new Label()
      {
        Text = name,
        FontAttributes = FontAttributes.Bold,
        BackgroundColor = Color.LightGreen
      };
    }
  }

  public class MahyongRound
  {
    public PlayerIndex Wind { get; set; }  = PlayerIndex.player1;
    public PlayerIndex Mahyong { get; set; } = PlayerIndex.player1;
    public int[] Scores { get; set; } = new int[] { 0, 0, 0, 0 };

    public int[] Payment
    {
      get
      {
        int[] payment = new int[] { 0, 0, 0, 0 };
        for (int to = 0; to < 4; to++)
          for (int from = 0; from < 4; from++)
            payment[to] += Pay((PlayerIndex)to, (PlayerIndex)from);
        return payment; 
      }
    }

    /// <summary>
    /// A positive value of pay from from to to + a negative value from to to from. 
    /// </summary>
    /// <param name="to"></param>
    /// <param name="from"></param>
    /// <returns></returns>
    public int Pay(PlayerIndex to, PlayerIndex from)
    {
      if (to == from)
        return 0;
      int multiplyer = ((Wind == to) || (Wind == from)) ? 2 : 1;

      //In case of mahyong you don't have to pay:
      if (Mahyong == to)
        return Scores[(int)to] * multiplyer;
      if (Mahyong == from)
        return -Scores[(int)from] * multiplyer;

      //Just exchange. 
      return (Scores[(int)to] - Scores[(int)from]) * multiplyer;
    }
  }

}
