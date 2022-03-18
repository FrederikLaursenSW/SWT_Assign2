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
        public bool IsDoorOpen { get; set; }
        public bool IsDoorLocked { get; set; }
        public event EventHandler<DoorEvents> DoorChangedEvent;

        public void LockDoor()
        {
            IsDoorLocked = true;
        }

        public void UnlockDoor()
        {
            IsDoorLocked = false;
        }

        public void OnDoorOpen()
        {
            if (IsDoorOpen == false && IsDoorLocked == false )
            {
                OnDoorChanged(new DoorEvents { DoorIsOpen = true });
                IsDoorOpen = true;
            }
        }
        public void OnDoorClose()
        {
            if (IsDoorOpen == true && IsDoorLocked == false)
            {
                OnDoorChanged(new DoorEvents { DoorIsOpen = false });
                IsDoorOpen = false;
            }
        }
        public void OnDoorChanged(DoorEvents e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }
    }
}
