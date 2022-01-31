using ModelExample.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelExample
{
    public abstract class Person : DTOBase
    {
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        #region Residence Lazy Loading Property

        private int residenceId { get; set; }

        public int ResidenceId
        {
            get
            {
                return residenceId;
            }
            set
            {
                residenceId = value;
                residence = null;
            }
        }

        private Country residence { get; set; }

        public Country Residence
        {
            get
            {
                if (residence == null)
                    residence = CountryManager.Get(residenceId);
                return residence;
            }
            set
            {
                residence = value;
                residenceId = value.Id;
            }
        }

        #endregion
    }
}
