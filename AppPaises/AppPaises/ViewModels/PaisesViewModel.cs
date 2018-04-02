namespace AppPaises.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;
    using Services;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class PaisesViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<Land> lands;
        private bool isRefreshing;
        private string filter;
        private string prueba;
        #endregion

        // ObservableCollection para usar en una ListView
        #region Properties

        public string Prueba
        {
            get { return this.prueba; }
            set { SetValue(ref this.prueba, value); }
        }

        public ObservableCollection<Land> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }
        #endregion

        #region Constructors
        public PaisesViewModel()
        {
            this.Prueba = "Esta bindiando la data";
            this.apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Methods
        private async void LoadLands()
        {
            // verificando conexion
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                //sacar del stack la navegacion
                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var list = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(list);
            this.Prueba = "esta enlazando la data";
        }
        #endregion
    }
}
