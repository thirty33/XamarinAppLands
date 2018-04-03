﻿namespace AppPaises.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Models;
    public class PaisViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<Border> borders;

        #endregion
        #region Propiedades
        public ObservableCollection<Border> Borders
        {
            get { return this.borders; }
            set { SetValue(ref this.borders, value); }
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
        }
        #endregion

        #region Metodos
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
