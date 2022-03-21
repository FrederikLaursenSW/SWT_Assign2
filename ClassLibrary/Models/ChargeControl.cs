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
        private bool Connected { get; set; }

        private UsbChargerSimulator testSimulator;

        public double NewCurrent { get; set; }

        private IUsbCharger _usbCharger;

        public ChargeControl(IUsbCharger usbCharger)
        {
            _usbCharger = usbCharger;
            _usbCharger.CurrentValueEvent += HandleChargingEvent;
            testSimulator = new UsbChargerSimulator();
        }

        public void StartCharge()
        {
            testSimulator.SimulateConnected(true);
            testSimulator.StartCharge();
        }

        public void StopCharge()
        {
            if (NewCurrent > 0 && NewCurrent <= 5)
            {
                Console.WriteLine("Telefonen er fuldt opladt");
                testSimulator.StopCharge();
            }
            else if (NewCurrent > 500)
            {
                Console.WriteLine("Kortslutning: Fjern STRAKS telefonen fra oplader");
                testSimulator.StopCharge();
            }
        }

        private void HandleChargingEvent(Object o, CurrentEventArgs chargingEvent)
        {
            NewCurrent = chargingEvent.Current;
        }
    }
}
