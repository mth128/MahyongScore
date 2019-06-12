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
  public partial class SetPlayerNamesPage : ContentPage
  {

    Entry Player1Box = new Entry();
    Entry Player2Box = new Entry();
    Entry Player3Box = new Entry();
    Entry Player4Box = new Entry();

    public ObservableCollection<string> Items { get; set; }

    public SetPlayerNamesPage()
    {
      InitializeComponent();

      Grid grid = new Grid();

      MainStackLayout.Children.Add(grid);

      grid.ColumnDefinitions.Add(new ColumnDefinition() );
      grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });

      grid.RowDefinitions.Add(new RowDefinition());
      grid.RowDefinitions.Add(new RowDefinition());
      grid.RowDefinitions.Add(new RowDefinition());
      grid.RowDefinitions.Add(new RowDefinition());

      grid.Children.Add(new Label() { Text = "Player 1:" }, 0, 0);
      grid.Children.Add(Player1Box, 1, 0);
      grid.Children.Add(new Label() { Text = "Player 2:" }, 0, 1);
      grid.Children.Add(Player2Box, 1, 1);
      grid.Children.Add(new Label() { Text = "Player 3:" }, 0, 2);
      grid.Children.Add(Player3Box, 1, 2);
      grid.Children.Add(new Label() { Text = "Player 4:" }, 0, 3);
      grid.Children.Add(Player4Box, 1, 3);

      Player1Box.Text = Mahyong.Main.Player1;
      Player2Box.Text = Mahyong.Main.Player2;
      Player3Box.Text = Mahyong.Main.Player3;
      Player4Box.Text = Mahyong.Main.Player4;

      Player1Box.TextChanged += Player1BoxChanged;
      Player2Box.TextChanged += Player2BoxChanged;
      Player3Box.TextChanged += Player3BoxChanged;
      Player4Box.TextChanged += Player4BoxChanged;
    }

    private void Player1BoxChanged(object sender, TextChangedEventArgs e)
    {
      Mahyong.Main.Player1 = Player1Box.Text; 
    }

    private void Player2BoxChanged(object sender, TextChangedEventArgs e)
    {
      Mahyong.Main.Player2 = Player2Box.Text;
    }

    private void Player3BoxChanged(object sender, TextChangedEventArgs e)
    {
      Mahyong.Main.Player3 = Player3Box.Text;
    }

    private void Player4BoxChanged(object sender, TextChangedEventArgs e)
    {
      Mahyong.Main.Player4 = Player4Box.Text;
    }

    private async void OK_Clicked(object sender, EventArgs e)
    {
      await Navigation.PopModalAsync();
      MessagingCenter.Send(this, "ChangeNames");
    }
  }
}
