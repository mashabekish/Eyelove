using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Eyelove
{
    public class MessagePage : ContentPage
    {
        public MessagePage(string l)
        {
            BackgroundColor = Color.White;

            StackLayout layout = new StackLayout
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(15, 30),
                Spacing = 25
            };

            Grid grid = new Grid
            {
                BackgroundColor = Color.White,
                RowDefinitions =
                {
                    new RowDefinition { Height = 50 },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            ImageButton setting = new ImageButton
            {
                Source = "setting.png",
                Aspect = Aspect.AspectFit,
                BackgroundColor = Color.White
            };
            grid.Children.Add(setting, 0, 0);
            setting.Clicked += OnSettingClicked;

            async void OnSettingClicked(object sender, System.EventArgs e)
            {
                await Navigation.PushModalAsync(new SettingPage(l));
            }

            ImageButton choice = new ImageButton
            {
                Source = "choice.png",
                Aspect = Aspect.AspectFit,
                BackgroundColor = Color.White
            };
            grid.Children.Add(choice, 1, 0);
            choice.Clicked += OnChoiceClicked;

            async void OnChoiceClicked(object sender, System.EventArgs e)
            {
                await Navigation.PushModalAsync(new СhoicePage(l));
            }

            ImageButton message = new ImageButton
            {
                Source = "amessage.png",
                Aspect = Aspect.AspectFit,
                BackgroundColor = Color.White
            };
            grid.Children.Add(message, 2, 0);

            layout.Children.Add(grid);

            Entry search = new Entry
            {
                Placeholder = "Поиск диалога",
                PlaceholderColor = Color.DarkGray,
                Keyboard = Keyboard.Text
            };
            layout.Children.Add(search);

            List<Search> users = DB.Dialog(l);

            for (int i = 0; i < users.Count; i++)
            {
                Users User = DB.Info(users[i].ID_2);
                Frame frame = new Frame
                {
                    Padding = 5,
                    BorderColor = Color.DarkGray,
                    CornerRadius = 5,
                    HasShadow = true
                };

                Grid grid2 = new Grid
                {
                    BackgroundColor = Color.White,
                    RowDefinitions =
                {
                    new RowDefinition { Height = 70 },
                },
                    ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) }
                }
                };

                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string phh = Path.Combine(path, users[i].ID_2 + "photo.jpg");
                Frame photo = new Frame
                {
                    Margin = 2,
                    BorderColor = Color.Black,
                    CornerRadius = 50,
                    HeightRequest = 25,
                    WidthRequest = 25,
                    IsClippedToBounds = true,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Content = new Image
                    {
                        Source = ImageSource.FromFile(phh),
                        Aspect = Aspect.AspectFill,
                        Margin = -20,
                        HeightRequest = 60,
                        WidthRequest = 60
                    }
                };
                grid2.Children.Add(photo, 0, 0);

                Button name = new Button
                {
                    Text = User.Name + ", " + User.Age,
                    TextColor = Color.Black,
                    BorderWidth = 0,
                    BackgroundColor = Color.White
                };
                grid2.Children.Add(name, 1, 0);

                frame.Content = grid2;
                layout.Children.Add(frame);
            }

            Content = layout;
        }
    }
}