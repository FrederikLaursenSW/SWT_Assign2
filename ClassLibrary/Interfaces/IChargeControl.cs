using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IChargeControl
    {
        bool Connected { get; }

        void StartCharge();
        void StopCharge();
    }
}
