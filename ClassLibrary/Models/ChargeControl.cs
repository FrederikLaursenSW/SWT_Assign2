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

        private bool Overload { get; set; }

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
                testSimulator.StopCharge(); // Kan stoppes?
                testSimulator.SimulateConnected(false);
            }
            else if (NewCurrent > 500)
            {
                Console.WriteLine("Kortslutning: Fjern STRAKS telefonen fra oplader");
                testSimulator.StopCharge(); // Skal stoppes?
                testSimulator.SimulateOverload(true); 
                testSimulator.SimulateConnected(false);

            }
            else if (NewCurrent > 5 && NewCurrent <= 500)
            {
                Console.WriteLine("Mobilen lader");
            }
            else
            {
                testSimulator.StopCharge();
                testSimulator.SimulateConnected(false);
            }
        }

        private void HandleChargingEvent(Object o, CurrentEventArgs chargingEvent)
        {
            NewCurrent = chargingEvent.Current;
            Overload = chargingEvent.Overload; // Giver det mening at have denne property? Bruger vi den til noget?
            Connected = chargingEvent.Connected;
        }
    }
}
