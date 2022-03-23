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
    public class StationControlUnitTest
    {
        private IDoor _doorSource; // Måske skal dette være en Fake class?
        private IRfidReader _RfidReaderSource;
        private StationControl _uut;
       
        [SetUp]
        public void Setup()
        {
            _doorSource = Substitute.For<IDoor>();
            _RfidReaderSource = Substitute.For<IRfidReader>();
            _uut = new StationControl(_doorSource, _RfidReaderSource);
        }

        [Test]
        public void ChangedState_DoorOpens_CurrentDoorIsOpenTrue()
        {
            _doorSource. DoorChangedEvent += Raise.EventWith(new DoorEvents { DoorIsOpen = true});
            Assert.That(_uut.CurrentDoorIsOpen, Is.True);
        }

        [TestCase(14)]
        [TestCase(-14)]
        public void StateAvaileble_RfidDetected_OldIdEqualsNewId(int id)
        {
            _uut.RfidDetected(id);

            Assert.That(_uut._oldId, Is.EqualTo(id));
        }

        //[TestCase(15,15)]
        //[TestCase(-14,-14)]
        //public void StateLoceked_RfidDetected_ConsoleSaysUnlocked(int id, int newId)
        //{
        //    _uut.RfidDetected(id);
        //    _uut.RfidDetected(newId);
        //    Assert.That(_uut.CurrentDoorIsOpen, Is.True);
        //}
        //public void OnRfidDetected_Rfid(int id)
        //{
        //    _RfidReaderSource. RfidDetectedEvent += Raise.EventWith(new RfidEvent { RfidId = id });
        //    Assert.That(_uut.CurrentDoorIsOpen, Is.True);
        //}




    }
}
