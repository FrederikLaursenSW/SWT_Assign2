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

        public bool Overload { get; set; }

        private UsbChargerSimulator testSimulator;

        public double NewCurrent { get; set; }

        private IUsbCharger _usbCharger;

        public ChargeControl(IUsbCharger usbCharger)
        {
            Connected = true;
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
                testSimulator.SimulateConnected(false);
                testSimulator.StopCharge(); // Kan stoppes?
            }
            else if (NewCurrent > 500)
            {
                Console.WriteLine("Kortslutning: Fjern STRAKS telefonen fra oplader");
                testSimulator.SimulateConnected(false);
                testSimulator.StopCharge(); // Skal stoppes?
                testSimulator.SimulateOverload(true); 

            }
            else if (NewCurrent > 5 && NewCurrent <= 500)
            {
                Console.WriteLine("Mobilen lader");
            }
            else
            {
                Console.WriteLine("Sker der noget eller hvad");
                testSimulator.SimulateConnected(false);
                testSimulator.StopCharge();
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
