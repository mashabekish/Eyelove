using System;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace Eyelove
{
    public class ProfileRegPage : ContentPage
    {
        public ProfileRegPage(string l, string p)
        {
            BackgroundColor = Color.White;

            StackLayout layout = new StackLayout
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(15, 30),
                Spacing = 15
            };

            Label vsp1 = new Label
            {
                Text = "НАСТРОЙКИ ПРОФИЛЯ",
                TextColor = Color.White,
                BackgroundColor = Color.Silver,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                HeightRequest = 50,
                FontSize = 15
            };
            layout.Children.Add(vsp1);

            Label namel = new Label
            {
                Text = " Имя пользователя",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(namel);

            Entry name = new Entry
            {
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

            Label agel = new Label
            {
                Text = " Возраст",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(agel);

            Entry age = new Entry
            {
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

            Label emaill = new Label
            {
                Text = " Электронная почта",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(emaill);

            Entry email = new Entry
            {
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

            Image photo = new Image
            {
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
                    MediaFile phh = await CrossMedia.Current.PickPhotoAsync();
                    if (phh != null)
                    {
                        photo.Source = ImageSource.FromFile(phh.Path);
                        DB.ph = phh.Path;
                        byte[] imageData;
                        using (FileStream fs = new FileStream(phh.Path, FileMode.Open))
                        {
                            imageData = new byte[fs.Length];
                            fs.Read(imageData, 0, imageData.Length);
                        }
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        string ph = Path.Combine(path, l + "photo.jpg");
                        using (FileStream fs = new FileStream(ph, FileMode.OpenOrCreate))
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
                Placeholder = "Введите информацию о себе",
                HeightRequest = 150,
                Keyboard = Keyboard.Default,
                MaxLength = 200
            };
            layout.Children.Add(comments);

            Label vsp2 = new Label
            {
                Text = "НАСТРОЙКИ ПОИСКА",
                TextColor = Color.White,
                BackgroundColor = Color.Silver,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                HeightRequest = 50,
                FontSize = 15
            };
            layout.Children.Add(vsp2);

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

            Label search_agel = new Label
            {
                Text = " Возрастной диапазон",
                TextColor = Color.Black,
                FontSize = 17
            };
            layout.Children.Add(search_agel);

            Grid grid = new Grid
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
                Placeholder = "От",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 160,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            grid.Children.Add(search_age1, 0, 0);

            Entry search_age2 = new Entry
            {
                Placeholder = "До",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 160,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            grid.Children.Add(search_age2, 1, 0);

            layout.Children.Add(grid);

            Button start = new Button
            {
                Text = "Зарегистрироваться",
                BorderColor = Color.DeepPink,
                BorderWidth = 1,
                BackgroundColor = Color.DeepPink,
                TextColor = Color.White
            };
            start.Clicked += OnStartClicked;
            layout.Children.Add(start);

            async void OnStartClicked(object sender, System.EventArgs e)
            {
                string sname = name.Text;
                int sage = Convert.ToInt32(age.Text);
                string semail = email.Text;
                string scomments = comments.Text;
                int ssearch_age1 = Convert.ToInt32(search_age1.Text);
                int ssearch_age2 = Convert.ToInt32(search_age2.Text);

                if ((sname == null) || (sex.SelectedIndex == -1) || (sage == 0) || (location.SelectedIndex == -1) || (semail == null) || (photo.Source == null) || (scomments == null) || (ssearch_age1 == 0) || (ssearch_age2 == 0) || (search_sex.SelectedIndex == -1))
                    await DisplayAlert("Ошибка", "Значения не введены", "OK");
                else
                {
                    string ssex = sex.Items[sex.SelectedIndex];
                    string slocation = location.Items[location.SelectedIndex];
                    string ssearch_sex = search_sex.Items[search_sex.SelectedIndex];
                    ssearch_sex = ssearch_sex + "а";
                    if (ssearch_age1 <= ssearch_age2)
                    {
                        bool fl = DB.Registration(l);
                        if (fl == true)
                        {
                            fl = DB.ProfileReg(l, p, sname, ssex, sage, slocation, semail, scomments, ssearch_age1, ssearch_age2, ssearch_sex);
                            if (fl == true)
                                await Navigation.PushModalAsync(new СhoicePage(l));
                            else
                                await DisplayAlert("Ошибка", "Ошибка регистрации", "OK");
                        }
                        else
                            await DisplayAlert("Ошибка", "Пользователь с таким логином уже существует", "OK");
                    }
                    else
                        await DisplayAlert("Ошибка", "Возрастной диапазон указан неверно", "OK");
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