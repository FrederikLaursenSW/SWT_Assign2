using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Interfaces;

namespace ClassLibrary.Models
{

    public class StationControl : IStationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger; // Denne linker pt. til et interface til control klassen. Er det ikke forkert? Bør det ikke være til IUsbCharger? Vi kalder jo startCharge på _charger.
        public int _oldId { get; set; }
        private IDoor _door;
        private IRfidReader _rfidReader;


        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public bool CurrentDoorIsOpen { get; set; } // Dum property for at teste om der er åbnet eller lukket
        public bool Connected { get; set; } = true; // dum property for at teste at der ikke er connection

        public StationControl(IDoor door, IRfidReader reader, IChargeControl chargeControl)
        {
            _charger = chargeControl;
            _door = door;
            _door.DoorChangedEvent += HandleDoorChangedEvent;
            _rfidReader = reader;
            _rfidReader.RfidDetectedEvent += HandleRfidDetectedEvent;
        }

        // Her mangler constructor

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        public void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Connected = false;
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }
                    break;

                //case LadeskabState.DoorOpen:
                //    // Ignore
                //    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        // Console.WriteLine("Current burde være: {0}", _charger.NewCurrent);

                        _charger.StopCharge();
                        _door.UnLockDoor();
                        CurrentDoorIsOpen = true;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }
                    break;
            }
        }

        private void HandleDoorChangedEvent(Object o, DoorEvents doorEvent)
        {
            CurrentDoorIsOpen = doorEvent.DoorIsOpen;
        }

        private void HandleRfidDetectedEvent(Object o, RfidEvent RfidDetectedEvent)
        {
            RfidDetected(RfidDetectedEvent.RfidId);
        }

    }
}
