namespace AppPaises.ViewModels
{
    using Models;
    public class PaisViewModel
    {
        #region Propiedades
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
        }
        #endregion
    }
}
