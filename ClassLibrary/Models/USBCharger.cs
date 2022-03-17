using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Interfaces;

namespace ClassLibrary.Models
{
    public class USBCharger : IUsbCharger
    {
        public double CurrentValue => throw new NotImplementedException();

        public bool Connected => throw new NotImplementedException();

        public event EventHandler<CurrentEventArgs> CurrentValueEvent;

        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }
    }
}
