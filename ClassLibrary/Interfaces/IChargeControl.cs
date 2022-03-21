using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IChargeControl
    {

        public bool Connected { get; set; }


       public bool Overload { get; set; }

        void StartCharge();
        void StopCharge();
    }
}
