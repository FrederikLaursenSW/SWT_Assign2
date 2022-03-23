using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabTest
{
    public class StationControlUnitTest
    {
        private IDoor _doorSource; // Måske skal dette være en Fake class?
        private IRfidReader _RfidReaderSource;
        private IChargeControl _chargeControl;
        private StationControl _uut;
       
        [SetUp]
        public void Setup()
        {
            _doorSource = Substitute.For<IDoor>();
            _RfidReaderSource = Substitute.For<IRfidReader>();
            _chargeControl = Substitute.For<IChargeControl>();
            _chargeControl.Connected = true;
            _uut = new StationControl(_doorSource, _RfidReaderSource, _chargeControl);
        }

        [Test]
        public void ChangedState_DoorOpens_CurrentDoorIsOpenTrue()
        {
            _doorSource. DoorChangedEvent += Raise.EventWith(new DoorEvents { DoorIsOpen = true});
            Assert.That(_uut.CurrentDoorIsOpen, Is.True);
        }


        [TestCase(14)]
        [TestCase(-14)]
        public void ChargerNotConnected_RfidDetected_ConnectedPropertyIsFalse(int id) 
        {
            _chargeControl.Connected = false;
            _uut.RfidDetected(id);

            Assert.That(_uut.Connected, Is.False);

        }


        [TestCase(14)]
        [TestCase(-14)]
        public void StateAvaileble_RfidDetected_OldIdEqualsNewId(int id)
        {
            _uut.RfidDetected(id);

            Assert.That(_uut._oldId, Is.EqualTo(id));
        }


        
        [TestCase(15, 15)]
        [TestCase(-14, -14)]
        public void StateLoceked_RfidDetected_CurrentDoorIsOpenTrue(int id, int newId)
        {
            _uut.RfidDetected(id);
            _uut.RfidDetected(newId);
            Assert.That(_uut.CurrentDoorIsOpen, Is.True);
        }
        //public void OnRfidDetected_Rfid(int id)
        //{
        //    _RfidReaderSource. RfidDetectedEvent += Raise.EventWith(new RfidEvent { RfidId = id });
        //    Assert.That(_uut.CurrentDoorIsOpen, Is.True);
        //}


        [TestCase(15, 16)]
        [TestCase(-15, -16)]
        public void StateLoceked_RfidDetected_CurrentDoorIsOpenFalse(int id, int newId)
        {
            _uut.RfidDetected(id);
            _uut.RfidDetected(newId);
            Assert.That(_uut.CurrentDoorIsOpen, Is.False);
        }



    }
}
