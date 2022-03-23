using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabTest
{
    public class DoorUnitTest
    {
        private Door _uut; // Skal dette være et interface eller en klasse
        private DoorEvents _receivedEventArgs;


        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;

            _uut = new Door(); 

            
        }

        [Test]
        public void ChangeState_OpensDoor_EventFired()
        {
            _uut.DoorChangedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };

            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void ChangeState_OpensDoor_DoorIsOpen()
        {
            _uut.DoorChangedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };

            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs.DoorIsOpen, Is.True);
        }

        [Test]
        public void ChangeStateDoorClosedAndLocked_OpensDoor_DoorIsOpen()
        {
            _uut.DoorChangedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };

            _uut.IsDoorOpen = true;
            _uut.IsDoorLocked = false;
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs, Is.Null);
        }



        [Test]
        public void ChangeStateFromOpen_OnCloseDoor_DoorIsClosed()
        {
            _uut.DoorChangedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };

            _uut.IsDoorOpen = true;
            _uut.OnDoorClose();
            Assert.That(_receivedEventArgs.DoorIsOpen, Is.False);
        }


        [Test]
        public void ChangeStateWhileLocked_OpensDoor_EventNotFired()
        {
            _uut.DoorChangedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };
           
            _uut.LockDoor();
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs, Is.Null);
        }

        [Test]
        public void NotSubscribed_OpensDoor_EventNotFired()
        {
            _uut.LockDoor();
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs, Is.Null);
        }

        [Test]
        public void LockDoorWhileClosed_LockDoor_Locked()
        {
            _uut.LockDoor();

            Assert.That(_uut.IsDoorLocked, Is.True);
        }

        [Test]
        public void UnLockDoorWhileClosed_UnLockDoor_UnLocked()
        {
            _uut.IsDoorLocked = true;
            _uut.UnLockDoor();

            Assert.That(_uut.IsDoorLocked, Is.False);
        }


        [Test]
        public void LockDoorWhileOpen_LockDoor_NotLocked()
        {
            _uut.DoorChangedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };

            _uut.OnDoorOpen();
            _uut.LockDoor();

            Assert.That(_uut.IsDoorLocked, Is.False);
        }

        [Test]
        public void SendSameState_OnCloseDoor_eventNotFired()
        {
            _uut.DoorChangedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };

            _uut.OnDoorClose();
            Assert.That(_receivedEventArgs, Is.Null);
        }
    }
}
