namespace AppPaises.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Models;
    public class PaisViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<Border> borders;

        private ObservableCollection<Currency> currencies;

        private ObservableCollection<Language> languages;
        #endregion

        #region Propiedades
        public ObservableCollection<Border> Borders
        {
            get { return this.borders; }
            set { SetValue(ref this.borders, value); }
        }
        public ObservableCollection<Currency> Currencies
        {
            get { return this.currencies; }
            set { SetValue(ref this.currencies, value); }
        }

        public ObservableCollection<Language> Languages
        {
            get { return this.languages; }
            set { this.SetValue(ref this.languages, value); }
        }
        public Land Land
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public PaisViewModel(Land land)
        {
            this.Land = land;
            this.LoadBorders();
            this.LoadCurrencies();
            this.LoadLanguages();
        }




        #endregion

        #region Metodos
        private void LoadLanguages()
        {
            this.Languages = new ObservableCollection<Language>(
                  this.Land.Languages);
        }
        private void LoadCurrencies()
        {
            this.Currencies = new ObservableCollection<Currency>(
                this.Land.Currencies);
        }
        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Border>();
            foreach (var border in this.Land.Borders)
            {
                var land = MainViewModel.GetInstance().LandsList.
                                        Where(l => l.Alpha3Code == border).
                                        FirstOrDefault();
                if (land != null)
                {
                    this.Borders.Add(new Border
                    {
                        Code = land.Alpha3Code,
                        Name = land.Name,
                    });
                }
            }
        }
        #endregion
    }
}
