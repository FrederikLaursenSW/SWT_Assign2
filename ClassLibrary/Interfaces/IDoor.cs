using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IDoor
    {
        event EventHandler<DoorEvents> DoorIsOpenEvent;

        void LockDoor();
        void UnlockDoor();

        void OnDoorOpen();

        void OnDoorClose();
    }

    public class DoorEvents : EventArgs
    {
        public bool DoorIsOpen { get; set; }
    }
}
