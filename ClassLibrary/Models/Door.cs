using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Interfaces;

namespace ClassLibrary.Models
{
    public class Door : IDoor
    {

        public event EventHandler<DoorEvents> DoorIsOpenEvent;


        public void LockDoor()
        {
            throw new NotImplementedException();
        }

        public void UnlockDoor()
        {
            throw new NotImplementedException();
        }

        public void OnDoorOpen()
        {
            throw new NotImplementedException();
        }

        public void OnDoorClose()
        {
            throw new NotImplementedException();
        }
    }
}
