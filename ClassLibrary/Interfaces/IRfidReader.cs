using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{

    public interface IRfidReader
    {
        public event EventHandler<RfidEvent> RfidDetectedEvent;
        void OnRfidRead(int id);
    }
    public class RfidEvent : EventArgs
    {
        public int RfidId { get; set; }
    }



}

