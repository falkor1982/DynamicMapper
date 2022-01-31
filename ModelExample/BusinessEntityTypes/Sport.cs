using ModelExample.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelExample
{
    public class Sport : DTOBase
    {
        public string Name { get; set; }

        private List<Customer> playedBy { get; set; }

        public List<Customer> PlayedBy
        {
            get
            {
                if (playedBy == null)
                    playedBy = CustomerManager.PlayedBy(Id);
                return playedBy;
            }
        }
    }
}
