using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{

   

    public interface IRfidReader
    {

        public event EventHandler<RFIDEvents> RFIDDetectedEvent;

        void OnRfidRead(int id);

    }


    public class RFIDEvents : EventArgs
    {
        public bool DoorIsOpen { get; set; }
    }
}

