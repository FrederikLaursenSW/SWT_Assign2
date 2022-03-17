using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    internal interface IDoor
    {
        void LockDoor();
        void UnlockDoor();

        void OnDoorOpen();

        void OnDoorClose();
    }
}
