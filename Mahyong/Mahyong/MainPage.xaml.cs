using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mahyong
{
  // Learn more about making custom code visible in the Xamarin.Forms previewer
  // by visiting https://aka.ms/xamarinforms-previewer
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class MainPage : ContentPage
  {
    Mahyong Mahyong => Mahyong.Main;

    public Label player1Label;
    public Label player2Label;
    public Label player3Label;
    public Label player4Label;

    Grid Grid { get; }  = new Grid();

    public MainPage()
    {
      InitializeComponent();

      MainStackLayout.Children.Add(Grid);

      BuildLayout();

      MessagingCenter.Subscribe<SetPlayerNamesPage>(this, "ChangeNames", (obj) => { ChangeNames(); });
      MessagingCenter.Subscribe<AddRoundPage>(this, "Refresh", (obj) => { ChangeNames(); });
    }

    private void BuildLayout()
    {
      Grid.Children.Clear();
      Grid.ColumnDefinitions.Clear();

      for (int i = 0; i < 4; i++)
        Grid.ColumnDefinitions.Add(new ColumnDefinition());

      Grid.Children.Add(player1Label = Mahyong.PlayerLabel(PlayerIndex.player1), 0, 0);
      Grid.Children.Add(player2Label = Mahyong.PlayerLabel(PlayerIndex.player2), 1, 0);
      Grid.Children.Add(player3Label = Mahyong.PlayerLabel(PlayerIndex.player3), 2, 0);
      Grid.Children.Add(player4Label = Mahyong.PlayerLabel(PlayerIndex.player4), 3, 0);

      int row = 0;
      int[] scores = new int[4]; 

      foreach(MahyongRound round in Mahyong.Rounds)
      {
        row++;
        for (int i = 0; i < 4; i++)
          Grid.Children.Add(new Label() { Text = round.Scores[i].ToString(), TextColor = Color.Gray }, row, i);
        row++;
        int[] payments = round.Payment;
        for (int i = 0; i < 4; i++)
        {
          scores[i] += payments[i]; 
          Grid.Children.Add(new Label() { Text = round.Scores[i].ToString() }, i, row);
        }
      }

      row++;
      for (int i = 0; i < 4; i++)
        Grid.Children.Add(new Label() { Text = scores[i].ToString(), FontAttributes = FontAttributes.Bold }, i, row);

    }

    private void ChangeNames()
    {      
      player1Label.Text = Mahyong.Main.Player1;
      player2Label.Text = Mahyong.Main.Player2;
      player3Label.Text = Mahyong.Main.Player3;
      player4Label.Text = Mahyong.Main.Player4;
      BuildLayout(); 
    }

    private async Task StartSetPlayerNamesPageAsync()
    {
      await Navigation.PushModalAsync(new NavigationPage(new SetPlayerNamesPage()));
    }

    private async void SetPlayerNames(object sender, EventArgs e)
    {
      await StartSetPlayerNamesPageAsync();
    }

    private async void AddRound(object sender, EventArgs e)
    {
      await StartAddRoundPageAsync(); 
    }

    private async Task StartAddRoundPageAsync()
    {
      await Navigation.PushModalAsync(new NavigationPage(new AddRoundPage()));      
    }
  }
}
