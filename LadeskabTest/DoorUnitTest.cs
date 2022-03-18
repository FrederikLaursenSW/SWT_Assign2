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
        private IDoor _uut;
        private DoorEvents _receivedEventArgs;


        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;

            _uut = new Door();

            _uut.DoorChangedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };
        }

        [Test]
        public void ChangeState_OpensDoor_EventFired()
        {
            
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void ChangeState_OpensDoor_DoorIsOpen()
        {
            
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs.DoorIsOpen, Is.True);
        }

        [Test]
        public void ChangeStateWhileLocked_OpensDoor_EventNotFired()
        {
            _uut.LockDoor();
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs, Is.Null);
        }

    }
}
