using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IChargeControl
    {
        //event EventHandler<CurrentEventArgs> CurrentValueEvent;
        bool Connected { get; set; }

        double NewCurrent { get; set; }
        
        void StartCharge();
        void StopCharge();
    }
}
