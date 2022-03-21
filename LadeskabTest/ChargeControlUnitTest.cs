using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using NSubstitute;
using NUnit.Framework;

namespace LadeskabTest
{
    public class ChargeControlUnitTest
    {
        private ChargeControl _uut;
        private IUsbCharger _chargerSimulatorSource;
        private StationControl _stationControlSource; // Bør være en fake?

        [SetUp]
        public void Setup()
        {
            _chargerSimulatorSource = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_chargerSimulatorSource);
        }

        [Test]
        public void ChangedState_DoorOpens_CurrentDoorIsOpenTrue()
        {
            _doorSource.DoorChangedEvent += Raise.EventWith(new DoorEvents { DoorIsOpen = true });
            Assert.That(_uut.CurrentDoorIsOpen, Is.True);
        }
        
}
