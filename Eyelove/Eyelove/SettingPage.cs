using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Eyelove
{
    public class SettingPage : ContentPage
    {
        public SettingPage(string l)
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
                Source = "asetting.png",
                Aspect = Aspect.AspectFit,
                BackgroundColor = Color.White
            };
            grid.Children.Add(setting, 0, 0);

            ImageButton choice = new ImageButton
            {
                Source = "choice.png",
                Aspect = Aspect.AspectFit,
                BackgroundColor = Color.White
            };
            grid.Children.Add(choice, 1, 0);
            choice.Clicked += OnСhoiceClicked;

            async void OnСhoiceClicked(object sender, System.EventArgs e)
            {
                await Navigation.PushModalAsync(new СhoicePage(l));
            }

            ImageButton message = new ImageButton
            {
                Source = "message.png",
                Aspect = Aspect.AspectFit,
                BackgroundColor = Color.White
            };
            grid.Children.Add(message, 2, 0);
            message.Clicked += OnMessageClicked;

            async void OnMessageClicked(object sender, System.EventArgs e)
            {
                await Navigation.PushModalAsync(new MessagePage(l));
            }

            layout.Children.Add(grid);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string phh = Path.Combine(path, l + "photo.jpg");

            Frame photo = new Frame
            {
                Margin = 10,
                BorderColor = Color.Black,
                HasShadow = true,
                CornerRadius = 150,
                HeightRequest = 200,
                WidthRequest = 200,
                IsClippedToBounds = true,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            if (File.Exists(phh))
            {
                photo.Content = new Image
                {
                    Source = ImageSource.FromFile(phh),
                    Aspect = Aspect.AspectFill,
                    Margin = -20,
                    HeightRequest = 100,
                    WidthRequest = 100
                };
            }
            layout.Children.Add(photo);

            List<Users> users = DB.Description(l);

            Label description = new Label
            {
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                HeightRequest = 170,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            FormattedString formattedString = new FormattedString();
            formattedString.Spans.Add(new Span
            {
                Text = users[0].Name + ", " + users[0].Age + "\n",
                FontSize = 30
            });
            formattedString.Spans.Add(new Span
            {
                Text = "\n",
                FontSize = 15
            });
            formattedString.Spans.Add(new Span
            {
                Text = users[0].Comments,
                FontSize = 15
            });

            description.FormattedText = formattedString;
            layout.Children.Add(description);

            StackLayout layout2 = new StackLayout
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Button profile = new Button
            {
                Text = "Профиль",
                TextColor = Color.White,
                BackgroundColor = Color.DeepPink,
                BorderWidth = 0
            };
            layout2.Children.Add(profile);
            profile.Clicked += OnProfileClicked;

            async void OnProfileClicked(object sender, System.EventArgs e)
            {
                await Navigation.PushModalAsync(new SetProfilePage(l));
            }

            layout.Children.Add(layout2);

            Button option = new Button
            {
                Text = "Настройки",
                TextColor = Color.White,
                BackgroundColor = Color.DeepPink,
                BorderWidth = 0
            };
            layout2.Children.Add(option);
            option.Clicked += OnOptionClicked;

            async void OnOptionClicked(object sender, System.EventArgs e)
            {
                await Navigation.PushModalAsync(new SetSettingPage(l));
            }

            Content = layout;
        }
    }
}