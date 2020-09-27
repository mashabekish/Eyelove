using Xamarin.Forms;

namespace Eyelove
{
	public class MainPage : ContentPage
	{
		public MainPage ()
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
            AbsoluteLayout.SetLayoutBounds(eyelovei, new Rectangle(0.0, 0.15, 1, 0.2));
            AbsoluteLayout.SetLayoutFlags(eyelovei, AbsoluteLayoutFlags.All);

            Label eyelove = new Label
            {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Eyelove",
                TextColor = Color.Black,
                FontSize = 30
            };
            AbsoluteLayout.SetLayoutBounds(eyelove, new Rectangle(0.0, 0.35, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(eyelove, AbsoluteLayoutFlags.All);

            Entry login = new Entry
            {
                Placeholder = "Логин",
                Keyboard = Keyboard.Default
            };
            AbsoluteLayout.SetLayoutBounds(login, new Rectangle(0.5, 0.5, 0.8, 0.1));
            AbsoluteLayout.SetLayoutFlags(login, AbsoluteLayoutFlags.All);

            Entry password = new Entry
            {
                Placeholder = "Пароль",
                IsPassword = true,
                Keyboard = Keyboard.Default
            };
            AbsoluteLayout.SetLayoutBounds(password, new Rectangle(0.5, 0.625, 0.8, 0.1));
            AbsoluteLayout.SetLayoutFlags(password, AbsoluteLayoutFlags.All);

            Button start = new Button
            {
                Text = "Вход",
                BorderColor = Color.DeepPink,
                BorderWidth = 1,
                BackgroundColor = Color.DeepPink,
                TextColor = Color.White
            };
            start.Clicked += OnStartClicked;
            AbsoluteLayout.SetLayoutBounds(start, new Rectangle(0.5, 0.75, 0.8, 0.1));
            AbsoluteLayout.SetLayoutFlags(start, AbsoluteLayoutFlags.All);

            async void OnStartClicked(object sender, System.EventArgs e)
            {
                if ((login.Text == null) || (password.Text == null))
                    await DisplayAlert("Ошибка", "Значения не введены", "OK");
                else
                {
                    string l = login.Text;
                    string p = DB.Login(l);
                    if (p == password.Text)
                        await Navigation.PushModalAsync(new СhoicePage(l));
                    else
                        await DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
                }
            }

            Button registration = new Button
            {
                Text = "Еще нет аккаунта? Зарегистрируйтесь",
                BorderWidth = 0,
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };
            registration.Clicked += OnRegistrationClicked;
            AbsoluteLayout.SetLayoutBounds(registration, new Rectangle(0, 1, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(registration, AbsoluteLayoutFlags.All);

            async void OnRegistrationClicked(object sender, System.EventArgs e)
            {
                await Navigation.PushModalAsync(new RegistrationPage());
            }

            layout.Children.Add(registration);
            layout.Children.Add(start);
            layout.Children.Add(password);
            layout.Children.Add(login);
            layout.Children.Add(eyelove);
            layout.Children.Add(eyelovei);

            Content = layout;
        }
	}
}