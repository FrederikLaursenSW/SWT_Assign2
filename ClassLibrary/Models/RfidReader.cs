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

        public event EventHandler<RFIDEvents> RFIDDetectedEvent;

        public void OnRfidRead(int id)
        {
            throw new NotImplementedException();
        }
    }
}
