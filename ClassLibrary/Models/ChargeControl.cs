using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Interfaces;

namespace ClassLibrary.Models
{
    public class ChargeControl
    {

        public double ChargingIsChanged { get; set; }

        private IUsbCharger _usbCharger;

        public ChargeControl(IUsbCharger usbCharger)
        {
            _usbCharger = usbCharger;
            _usbCharger.CurrentValueEvent += HandleChargingEvent;
        }

        private void HandleChargingEvent(Object o, CurrentEventArgs chargingEvent)
        {
            ChargingIsChanged = chargingEvent.Current;
        }
    }
}
