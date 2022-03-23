using ClassLibrary.Models;

// RFID: Lav i testprojektet en klasse: RFIDUnitTest --> bliver eventet rejst på det rigtige tidspunkt?

// Stationcontrol: Tjek om eventet fra RFID bliver modtaget. (bliver det rejst på det rigtige tidspunkt?) Test om den gør det rigtige.
// Flere test på door
// USB charger simulator --> lav test på denne klasse.
// Lav chargecontrol klassen, tag udgangspunkt i stationscontrol.
// Chargecontrol skal testes (


namespace SWT_Assign2
{

    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes

            Door door = new Door();
            RfidReader rfidReader = new RfidReader();
            UsbChargerSimulator usbChargerSimulator = new UsbChargerSimulator();
            ChargeControl chargeControl = new ChargeControl(usbChargerSimulator);
            StationControl stationControl = new StationControl(door, rfidReader, chargeControl);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
