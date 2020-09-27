using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Eyelove
{
    public class SetSettingPage : ContentPage
    {
        public SetSettingPage(string l)
        {
            BackgroundColor = Color.White;

            StackLayout layout = new StackLayout
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(15),
                Spacing = 15
            };

            Grid grid = new Grid
            {
                BackgroundColor = Color.Silver,
                RowDefinitions =
                {
                    new RowDefinition { Height = 50 },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) }
                }
            };

            ImageButton back = new ImageButton
            {
                Source = "back.png",
                BackgroundColor = Color.Silver,
                Aspect = Aspect.AspectFit
            };
            grid.Children.Add(back, 0, 0);
            back.Clicked += OnBackClicked;

            async void OnBackClicked(object sender, System.EventArgs e)
            {
                await Navigation.PushModalAsync(new SettingPage(l));
            }

            Label message = new Label
            {
                Text = "  НАСТРОЙКИ",
                TextColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                HeightRequest = 30,
                FontSize = 15
            };
            grid.Children.Add(message, 1, 0);

            layout.Children.Add(grid);

            List<Users> users = DB.Setting(l);

            Label search_sexl = new Label
            {
                Text = " Показывать",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(search_sexl);

            Picker search_sex = new Picker
            {
                Title = "Выберите пол"
            };
            search_sex.Items.Add("Женщин");
            search_sex.Items.Add("Мужчин");
            layout.Children.Add(search_sex);

            string s = users[0].Search_sex.Substring(0, 6);
            search_sex.SelectedItem = s;

            Label search_agel = new Label
            {
                Text = " Возрастной диапазон",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(search_agel);

            Grid grid1 = new Grid
            {
                BackgroundColor = Color.White,
                RowDefinitions =
                {
                    new RowDefinition {},
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            Entry search_age1 = new Entry
            {
                Text = Convert.ToString(users[0].Search_age1),
                Placeholder = "От",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 160,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            grid1.Children.Add(search_age1, 0, 0);

            Entry search_age2 = new Entry
            {
                Text = Convert.ToString(users[0].Search_age2),
                Placeholder = "До",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 160,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            grid1.Children.Add(search_age2, 1, 0);

            layout.Children.Add(grid1);

            Button start = new Button
            {
                Text = "Сохранить",
                BorderColor = Color.DeepPink,
                BorderWidth = 1,
                BackgroundColor = Color.DeepPink,
                TextColor = Color.White
            };
            start.Clicked += OnStartClicked;
            layout.Children.Add(start);

            Entry password = new Entry
            {
                Placeholder = "Старый пароль",
                IsPassword = true,
                Keyboard = Keyboard.Default
            };
            layout.Children.Add(password);

            Entry password1 = new Entry
            {
                Placeholder = "Новый пароль",
                IsPassword = true,
                Keyboard = Keyboard.Default
            };
            layout.Children.Add(password1);

            Entry password2 = new Entry
            {
                Placeholder = "Подтвердите пароль",
                IsPassword = true,
                Keyboard = Keyboard.Default
            };
            layout.Children.Add(password2);

            Button change = new Button
            {
                Text = "Сменить пароль",
                BorderColor = Color.DeepPink,
                BorderWidth = 1,
                BackgroundColor = Color.DeepPink,
                TextColor = Color.White
            };
            change.Clicked += OnChangeClicked;
            layout.Children.Add(change);

            Button exit = new Button
            {
                Text = "Выйти",
                BorderColor = Color.DeepPink,
                BorderWidth = 1,
                BackgroundColor = Color.DeepPink,
                TextColor = Color.White
            };
            exit.Clicked += OnExitClicked;
            layout.Children.Add(exit);

            Button delete = new Button
            {
                Text = "Удалить аккаунт",
                BorderColor = Color.DeepPink,
                BorderWidth = 1,
                BackgroundColor = Color.DeepPink,
                TextColor = Color.White
            };
            delete.Clicked += OnDeleteClicked;
            layout.Children.Add(delete);

            async void OnStartClicked(object sender, System.EventArgs e)
            {
                int ssearch_age1 = Convert.ToInt32(search_age1.Text);
                int ssearch_age2 = Convert.ToInt32(search_age2.Text);

                if ((ssearch_age1 == 0) || (ssearch_age2 == 0) || (search_sex.SelectedIndex == -1))
                    await DisplayAlert("Ошибка", "Значения не введены", "OK");
                else
                {
                    string ssearch_sex = search_sex.Items[search_sex.SelectedIndex] + "а";
                    if (ssearch_age1 <= ssearch_age2)
                    {
                        bool fl = DB.SettingSave(l, ssearch_age1, ssearch_age2, ssearch_sex);
                        if (fl == true)
                            await DisplayAlert("Уведомление", "Изменения сохранены", "OK");
                        else
                            await DisplayAlert("Ошибка", "Ошибка сохранения", "OK");
                    }
                    else
                        await DisplayAlert("Ошибка", "Возрастной диапазон указан неверно", "OK");
                }
            }

            async void OnChangeClicked(object sender, System.EventArgs e)
            {
                if ((password.Text == null) || (password1.Text == null) || (password2.Text == null))
                    await DisplayAlert("Ошибка", "Значения не введены", "OK");
                else
                {
                    if (password.Text == users[0].Password)
                    {
                        if (password1.Text == password2.Text)
                        {
                            bool fl = DB.Password(l, password1.Text);
                            if (fl == true)
                                await DisplayAlert("Уведомление", "Пароль изменен", "OK");
                            else
                                await DisplayAlert("Ошибка", "Ошибка сохранения", "OK");
                        }
                        else
                            await DisplayAlert("Ошибка", "Пароль не подтвержден", "OK");
                    }
                    else
                        await DisplayAlert("Ошибка", "Неверный пароль", "OK");
                }
            }

            async void OnExitClicked(object sender, System.EventArgs e)
            {
                bool answer = await DisplayAlert("Подтверждение действия", "Выйти из аккаунта?", "ДА", "НЕТ");
                if (answer)
                    await Navigation.PushModalAsync(new MainPage());
            }

            async void OnDeleteClicked(object sender, System.EventArgs e)
            {
                bool answer = await DisplayAlert("Подтверждение действия", "Удалить аккаунт?", "ДА", "НЕТ");
                if (answer)
                {
                    bool fl = DB.Delete(l);
                    if (fl == true)
                    {
                        await DisplayAlert("Уведомление", "Аккаунт удален", "ОК");
                        await Navigation.PushModalAsync(new MainPage());
                    }
                    else
                        await DisplayAlert("Ошибка", "Ошибка удаления", "OK");
                }
            }

            this.Content = layout;
        }
    }
}