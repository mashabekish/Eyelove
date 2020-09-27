using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Eyelove
{
    public class СhoicePage : ContentPage
    {
        public СhoicePage(string l)
        {
            BackgroundColor = Color.White;

            StackLayout layout = new StackLayout
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(15, 30),
                Spacing = 20
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
                Source = "achoice.png",
                Aspect = Aspect.AspectFit,
                BackgroundColor = Color.White
            };
            grid.Children.Add(choice, 1, 0);

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

            Image photo = new Image
            {
                Aspect = Aspect.AspectFill,
                HeightRequest = 380
            };

            Label description = new Label
            {
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                HeightRequest = 140,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            Grid grid1 = new Grid
            {
                ColumnSpacing = 20,
                BackgroundColor = Color.White,
                RowDefinitions =
                {
                    new RowDefinition { Height = 50 },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            Button dislike = new Button
            {
                Text = "Dislike",
                TextColor = Color.White,
                BackgroundColor = Color.DeepPink,
                BorderWidth = 0
            };
            grid1.Children.Add(dislike, 0, 0);

            Button like = new Button
            {
                Text = "Like",
                TextColor = Color.White,
                BackgroundColor = Color.DeepPink,
                BorderWidth = 0
            };
            grid1.Children.Add(like, 1, 0);

            List<Users> users = DB.Search(l);

            if (users[0].ID != 0)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string phh = Path.Combine(path, users[0].ID + "photo.jpg");
                photo.Source = ImageSource.FromFile(phh);
                FormattedString formattedString = new FormattedString();
                formattedString.Spans.Add(new Span
                {
                    Text = users[0].Name + ", " + users[0].Age + "\n",
                    FontSize = 23
                });
                formattedString.Spans.Add(new Span
                {
                    Text = "\n",
                    FontSize = 10
                });
                formattedString.Spans.Add(new Span
                {
                    Text = users[0].Comments,
                    FontSize = 15
                });
                description.FormattedText = formattedString;
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Уведомление", "В данный момент нет подходящих пользователей", "OK");
                });
                dislike.IsEnabled = false;
                like.IsEnabled = false;
            }

            layout.Children.Add(photo);
            layout.Children.Add(description);

            dislike.Clicked += OnDislikeClicked;
            async void OnDislikeClicked(object sender, System.EventArgs e)
            {
                DB.Dislike(l, users[0].ID);
                await Navigation.PushModalAsync(new СhoicePage(l));
            }

            like.Clicked += OnLikeClicked;
            async void OnLikeClicked(object sender, System.EventArgs e)
            {
                DB.Like(l, users[0].ID);
                await Navigation.PushModalAsync(new СhoicePage(l));
            }

            layout.Children.Add(grid1);

            Content = layout;
        }
    }
}