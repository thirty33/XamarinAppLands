namespace AppPaises.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class PaisItemViewModel : Land
    {
        #region Comandos
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }
        #endregion


        #region Metodos
        private async void SelectLand()
        {
            //Instancio la propiedad del locator y paso el item (this)
            MainViewModel.GetInstance().Land = new PaisViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new PaisPage());
        }
        #endregion
    }
}
