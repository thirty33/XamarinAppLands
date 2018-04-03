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
    using System;

    public class PaisesViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        //private ObservableCollection<Land> lands;
        private ObservableCollection<PaisItemViewModel> lands;
        private bool isRefreshing;
        private string filter;

        //Lista de paises como atributo de clase
        //Se movio a la MainViewModel
        //private List<Land> landsList;
        #endregion

        // ObservableCollection para usar en una ListView
        #region Properties
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string  Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }

        public ObservableCollection<PaisItemViewModel> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }


        #endregion

        #region Constructors
        public PaisesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Methods
        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<PaisItemViewModel>(this.ToPaisItemViewModel());
            }
            else
            {
                this.Lands = new ObservableCollection<PaisItemViewModel>(
                    this.ToPaisItemViewModel().Where(
                       l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }

        }

        private async void LoadLands()
        {
            // verificando conexion
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
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
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            //var list = (List<Land>)response.Result;
            MainViewModel.GetInstance().LandsList = (List<Land>)response.Result;
            //this.landsList = (List<Land>)response.Result;
            //this.Lands = new ObservableCollection<Land>(this.landsList);

            //Cambio coleccion observable a PaisItemViewModel
            //this.Lands = new ObservableCollection<PaisItemViewModel>(
            //    this.ToPaisItemViewModel());


            this.Lands = new ObservableCollection<PaisItemViewModel>(
                this.ToPaisItemViewModel());
            this.IsRefreshing = false;
            
        }


        #endregion

        #region Methods
        private IEnumerable<PaisItemViewModel> ToPaisItemViewModel()
        {
            return MainViewModel.GetInstance().LandsList.Select(l => new PaisItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        }

        #endregion
    }
}
