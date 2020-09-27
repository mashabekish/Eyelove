using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Eyelove
{
    public class SetProfilePage : ContentPage
    {
        public SetProfilePage(string l)
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
                Text = "  ПРОФИЛЬ",
                TextColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                HeightRequest = 30,
                FontSize = 15
            };
            grid.Children.Add(message, 1, 0);

            layout.Children.Add(grid);

            List<Users> users = DB.Profile(l);

            Label namel = new Label
            {
                Text = " Имя пользователя",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(namel);

            Entry name = new Entry
            {
                Text = users[0].Name,
                Placeholder = "Введите имя",
                Keyboard = Keyboard.Text
            };
            layout.Children.Add(name);

            Label sexl = new Label
            {
                Text = " Пол",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(sexl);

            Picker sex = new Picker
            {
                Title = "Выберите пол"
            };
            sex.Items.Add("Женщина");
            sex.Items.Add("Мужчина");
            layout.Children.Add(sex);

            sex.SelectedItem = users[0].Sex;

            Label agel = new Label
            {
                Text = " Возраст",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(agel);

            Entry age = new Entry
            {
                Text = Convert.ToString(users[0].Age),
                Placeholder = "Введите возраст",
                Keyboard = Keyboard.Numeric
            };
            layout.Children.Add(age);

            Label locationl = new Label
            {
                Text = " Место проживания",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(locationl);

            Picker location = new Picker
            {
                Title = "Выберите город"
            };
            location.Items.Add("Брест");
            location.Items.Add("Витебск");
            location.Items.Add("Гомель");
            location.Items.Add("Гродно");
            location.Items.Add("Минск");
            location.Items.Add("Могилев");
            layout.Children.Add(location);

            location.SelectedItem = users[0].Location;

            Label emaill = new Label
            {
                Text = " Электронная почта",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(emaill);

            Entry email = new Entry
            {
                Text = users[0].Email,
                Placeholder = "Введите email",
                Keyboard = Keyboard.Email
            };
            layout.Children.Add(email);

            Label photol = new Label
            {
                Text = " Фотография профиля",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(photol);

            string p = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string hp = Path.Combine(p, l + "photo.jpg");
            DB.ph = hp;

            Image photo = new Image()
            {
                Source = ImageSource.FromFile(hp),
                HeightRequest = 380
            };
            layout.Children.Add(photo);

            Button addphoto = new Button
            {
                Text = "Выбрать фото",
                BorderWidth = 0,
                BackgroundColor = Color.DeepPink,
                TextColor = Color.White
            };
            layout.Children.Add(addphoto);

            addphoto.Clicked += Addphoto;

            async void Addphoto(object o, EventArgs e)
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile ph = await CrossMedia.Current.PickPhotoAsync();
                    if (ph != null)
                    {
                        photo.Source = ImageSource.FromFile(ph.Path);
                        DB.ph = ph.Path;
                        byte[] imageData;
                        using (FileStream fs = new FileStream(ph.Path, FileMode.Open))
                        {
                            imageData = new byte[fs.Length];
                            fs.Read(imageData, 0, imageData.Length);
                        }
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        string phh = Path.Combine(path, l + "photo.jpg");
                        using (FileStream fs = new FileStream(phh, FileMode.OpenOrCreate))
                        {
                            fs.Write(imageData, 0, imageData.Length);
                        }
                    }
                }
            };

            Label commentsl = new Label
            {
                Text = " О пользователе",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(commentsl);

            Editor comments = new Editor
            {
                Text = users[0].Comments,
                Placeholder = "Введите информацию о себе",
                HeightRequest = 150,
                Keyboard = Keyboard.Default,
                MaxLength = 200
            };
            layout.Children.Add(comments);

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

            async void OnStartClicked(object sender, EventArgs e)
            {
                string sname = name.Text;
                int sage = Convert.ToInt32(age.Text);
                string semail = email.Text;
                string scomments = comments.Text;

                if ((sname == null) || (sex.SelectedIndex == -1) || (sage == 0) || (location.SelectedIndex == -1) || (semail == null) || (photo.Source == null) || (scomments == null))
                    await DisplayAlert("Ошибка", "Значения не введены", "OK");
                else
                {
                    string ssex = sex.Items[sex.SelectedIndex];
                    string slocation = location.Items[location.SelectedIndex];
                    bool fl = DB.ProfileSave(l, sname, ssex, sage, slocation, semail, scomments);
                    if (fl == true)
                        await DisplayAlert("Уведомление", "Изменения сохранены", "OK");
                    else
                        await DisplayAlert("Ошибка", "Ошибка сохранения", "OK");
                }
            }

            ScrollView scrollView = new ScrollView
            {
                Content = layout
            };
            this.Content = scrollView;
        }
    }
}