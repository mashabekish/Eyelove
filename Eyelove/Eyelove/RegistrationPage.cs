using Xamarin.Forms;

namespace Eyelove
{
    public class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            AbsoluteLayout layout = new AbsoluteLayout
            {
                BackgroundColor = Color.White
            };

            Image eyelovei = new Image
            {
                Source = "icon.png",
                Aspect = Aspect.AspectFit
            };
            AbsoluteLayout.SetLayoutBounds(eyelovei, new Rectangle(0.0, 0.1, 1, 0.2));
            AbsoluteLayout.SetLayoutFlags(eyelovei, AbsoluteLayoutFlags.All);

            Label eyelove = new Label
            {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Eyelove",
                TextColor = Color.Black,
                FontSize = 30
            };
            AbsoluteLayout.SetLayoutBounds(eyelove, new Rectangle(0.0, 0.3, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(eyelove, AbsoluteLayoutFlags.All);

            Entry login = new Entry
            {
                Placeholder = "Логин",
                Keyboard = Keyboard.Default
            };
            AbsoluteLayout.SetLayoutBounds(login, new Rectangle(0.5, 0.425, 0.8, 0.1));
            AbsoluteLayout.SetLayoutFlags(login, AbsoluteLayoutFlags.All);

            Entry password = new Entry
            {
                Placeholder = "Пароль",
                IsPassword = true,
                Keyboard = Keyboard.Default
            };
            AbsoluteLayout.SetLayoutBounds(password, new Rectangle(0.5, 0.55, 0.8, 0.1));
            AbsoluteLayout.SetLayoutFlags(password, AbsoluteLayoutFlags.All);

            Entry password2 = new Entry
            {
                Placeholder = "Подтвердите пароль",
                IsPassword = true,
                Keyboard = Keyboard.Default
            };
            AbsoluteLayout.SetLayoutBounds(password2, new Rectangle(0.5, 0.675, 0.8, 0.1));
            AbsoluteLayout.SetLayoutFlags(password2, AbsoluteLayoutFlags.All);

            Button start = new Button
            {
                Text = "Регистрация",
                BorderColor = Color.DeepPink,
                BorderWidth = 1,
                BackgroundColor = Color.DeepPink,
                TextColor = Color.White
            };
            start.Clicked += OnStartClicked;
            AbsoluteLayout.SetLayoutBounds(start, new Rectangle(0.5, 0.8, 0.8, 0.1));
            AbsoluteLayout.SetLayoutFlags(start, AbsoluteLayoutFlags.All);

            async void OnStartClicked(object sender, System.EventArgs e)
            {
                if ((login.Text == null) || (password.Text == null) || (password2.Text == null))
                    await DisplayAlert("Ошибка", "Значения не введены", "OK");
                else
                {
                    if (password.Text == password2.Text)
                    {
                        string l = login.Text;
                        string p = password.Text;
                        bool fl = DB.Registration(l);
                        if (fl == true)
                            await Navigation.PushModalAsync(new ProfileRegPage(l, p));
                        else
                            await DisplayAlert("Ошибка", "Пользователь с таким логином уже существует", "OK");
                    }
                    else
                        await DisplayAlert("Ошибка", "Пароль не подтвержден", "OK");
                }
            }

            Button registration = new Button
            {
                Text = "Есть аккаунт? Вход",
                BorderWidth = 0,
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };
            registration.Clicked += OnRegistrationClicked;
            AbsoluteLayout.SetLayoutBounds(registration, new Rectangle(0, 1, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(registration, AbsoluteLayoutFlags.All);

            async void OnRegistrationClicked(object sender, System.EventArgs e)
            {
                await Navigation.PushModalAsync(new MainPage());
            }

            layout.Children.Add(registration);
            layout.Children.Add(start);
            layout.Children.Add(password2);
            layout.Children.Add(password);
            layout.Children.Add(login);
            layout.Children.Add(eyelove);
            layout.Children.Add(eyelovei);

            Content = layout;
        }
    }
}