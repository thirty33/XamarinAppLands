namespace AppPaises
{
    using AppPaises.Views;
    using Xamarin.Forms;

    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //MainPage = new AppPaises.MainPage();
            this.MainPage = new NavigationPage(new LoginPage());

            // Probando inicializacion en PaisesPage
            //this.MainPage = new NavigationPage(new PaisesPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
