using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Interfaces;

namespace ClassLibrary.Models
{
    public class ChargeControl : IChargeControl
    {
        public bool Connected { get; set; }

        public event EventHandler<CurrentEventArgs> CurrentChangedEvent;
        public double NewCurrent { get; set; }

        private IUsbCharger _usbCharger;
        public ChargeControl(IUsbCharger usbCharger)
        {
            _usbCharger = usbCharger;
            _usbCharger.CurrentValueEvent += HandleChargingEvent;
            Connected = true;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }

        public void IsConnected(bool connectState)
        {
            Connected = connectState;
        }

        private void HandleChargingEvent(Object o, CurrentEventArgs chargingEvent)
        {
            CurrentChanged(chargingEvent.Current);
        }

        public void CurrentChanged(double current)
        {

            NewCurrent = current;

            switch (NewCurrent)
            {
                case 0:
                    //nothing happens
                    break;

                case double x when (x > 0 && x < 5):
                    Console.WriteLine("Telefonen er fuldt opladt");
                    _usbCharger.SimulateConnected(false);
                    StopCharge();

                    break;

                case double x when (x > 500):
                    Console.WriteLine("Kortslutning: Fjern STRAKS telefonen fra oplader");
                    _usbCharger.SimulateConnected(false);
                    StopCharge();
                    _usbCharger.SimulateOverloaded(true);
                    break;
                case double x when (x > 5 && x <= 500):
                    Console.WriteLine("Mobilen lader");
                    break;
            }
        }
    }
}
