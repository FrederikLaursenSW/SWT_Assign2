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
            if (IsDoorOpen == false)
                IsDoorLocked = true;
        }

        public void UnLockDoor()
        {
            if (IsDoorOpen == false)
                IsDoorLocked = false;
        }

        public void OnDoorOpen()
        {
            if (IsDoorOpen == false && IsDoorLocked == false )
            {
                IsDoorOpen = true;
                OnDoorChanged(new DoorEvents { DoorIsOpen = IsDoorOpen });
                
            }
        }
        public void OnDoorClose()
        {
            if (IsDoorOpen == true && IsDoorLocked == false)
            {
                IsDoorOpen = false;
                OnDoorChanged(new DoorEvents { DoorIsOpen = IsDoorOpen });
                
            }
        }
        public void OnDoorChanged(DoorEvents e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }
    }
}
