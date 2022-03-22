using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Interfaces;
using ClassLibrary.Models;

namespace ClassLibrary.Fakes
{
    public class FakeStationControl : IStationControl
    {
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        public bool Connected { get; set; }

        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;

        public void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (Connected)
                    {
                        _charger.StartCharge();
                        _oldId = id;
                        _state = LadeskabState.Locked;
                        //startChargeCalled = true;
                    }

                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _state = LadeskabState.Available;
                    }

                    break;
            }
        }
    }
}
