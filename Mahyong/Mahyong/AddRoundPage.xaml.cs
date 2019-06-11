using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mahyong
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AddRoundPage : ContentPage
  {
    private Picker mahyongPicker;
    private Picker windPicker; 

    private Entry[] entries = new Entry[4]; 
    private Grid Grid { get; } = new Grid();

    private int Index { get; }

    private Mahyong Mahyong => Mahyong.Main; 

    public ObservableCollection<string> Items { get; set; }

    public AddRoundPage(int modifyIndex = -1)
    {
      Index = modifyIndex; 
      InitializeComponent();

      MainStackLayout.Children.Add(Grid);

      for (int i = 0; i < 4; i++)
        Grid.ColumnDefinitions.Add(new ColumnDefinition());

      Grid.Children.Add(Mahyong.PlayerLabel(PlayerIndex.player1), 0, 0);
      Grid.Children.Add(Mahyong.PlayerLabel(PlayerIndex.player2), 1, 0);
      Grid.Children.Add(Mahyong.PlayerLabel(PlayerIndex.player3), 2, 0);
      Grid.Children.Add(Mahyong.PlayerLabel(PlayerIndex.player4), 3, 0);

      for (int i =0; i<4;i++)
        Grid.Children.Add(entries[i] = new Entry() { Keyboard = Keyboard.Numeric }, i, 1);

      windPicker = new Picker();
      windPicker.Items.Add(Mahyong.Player1);
      windPicker.Items.Add(Mahyong.Player2);
      windPicker.Items.Add(Mahyong.Player3);
      windPicker.Items.Add(Mahyong.Player4);
      windPicker.SelectedIndex = 0;

      mahyongPicker = new Picker();
      mahyongPicker.Items.Add(Mahyong.Player1);
      mahyongPicker.Items.Add(Mahyong.Player2);
      mahyongPicker.Items.Add(Mahyong.Player3);
      mahyongPicker.Items.Add(Mahyong.Player4);

      mahyongPicker.SelectedIndex = 0;
      MainStackLayout.Children.Add(new Label() { Text = "Mahyong:" });
      MainStackLayout.Children.Add(mahyongPicker);
      MainStackLayout.Children.Add(new Label() { Text = "Wind:" });
      MainStackLayout.Children.Add(windPicker);

     
      if (Index>=0 && Index < Mahyong.Rounds.Count)
      {
        MahyongRound changeRound = Mahyong.Rounds[Index];
        for (int i = 0; i < 0; i++)
          entries[i].Text = changeRound.Scores[i].ToString();
        mahyongPicker.SelectedIndex = (int) changeRound.Mahyong;
        windPicker.SelectedIndex = (int)changeRound.Wind; 
      }

    }

    private int ReadInt(object entry)
    {
      try
      {
        return Convert.ToInt32(((Entry)entry).Text); 
      }
      catch
      {
        return 0; 
      }
    }

    private async void OK_Clicked(object sender, EventArgs e)
    {
      MahyongRound round = new MahyongRound();
      for (int i = 0; i < 4; i++)
        round.Scores[i] = ReadInt(entries[i]);
      round.Mahyong = (PlayerIndex) mahyongPicker.SelectedIndex;
      round.Wind = (PlayerIndex) windPicker.SelectedIndex;

      if (Index >= 0 && Index < Mahyong.Rounds.Count)
        Mahyong.Rounds[Index] = round;
      else 
        Mahyong.Rounds.Add(round); 

      MessagingCenter.Send(this, "Refresh");
      await Navigation.PopModalAsync();
    }
    private async void Cancel_Clicked(object sender, EventArgs e)
    {
      await Navigation.PopModalAsync();
    }
  }
}
