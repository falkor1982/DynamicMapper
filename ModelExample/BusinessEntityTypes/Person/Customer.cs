using ModelExample.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelExample
{
    public class Customer : Person
    {
        #region FavouriteSport Lazy Loading Property

        private int favouriteSportId { get; set; }

        public int FavouriteSportId
        {
            get
            {
                return favouriteSportId;
            }
            set
            {
                favouriteSportId = value;
                favouriteSport = null;
            }
        }

        private Sport favouriteSport { get; set; }

        public Sport FavouriteSport
        {
            get
            {
                if (favouriteSport == null)
                    favouriteSport = SportManager.Get(favouriteSportId);
                return favouriteSport;
            }
            set
            {
                favouriteSport = value;
                favouriteSportId = value.Id;
            }
        }

        #endregion
    }
}
