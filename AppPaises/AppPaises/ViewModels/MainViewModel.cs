namespace AppPaises.ViewModels
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MainViewModel
    {

        #region Properties
        public List<Land> LandsList { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }

        public PaisesViewModel Lands
        {
            get;
            set;
        }

        public PaisViewModel Land { get; set; }
        #endregion


        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
