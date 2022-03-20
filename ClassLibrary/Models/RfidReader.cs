using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Interfaces;

namespace ClassLibrary.Models
{
    public class RfidReader : IRfidReader
    {

        public event EventHandler<RfidEvent> RfidDetectedEvent;
        
        public void OnRfidRead(int id)
        {
            OnRfidDetected(new RfidEvent { RfidId = id });
        }

        public void OnRfidDetected(RfidEvent e)
        {
            RfidDetectedEvent?.Invoke(this, e);
        }
    }
}
