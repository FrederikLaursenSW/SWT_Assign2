using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Fakes;
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
        private FakeStationControl _stationControlSource;

        [SetUp]
        public void Setup()
        {
            _chargerSimulatorSource = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_chargerSimulatorSource);
            _stationControlSource = new FakeStationControl();
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(499)]
        [TestCase(500)]
        [TestCase(501)]
        [TestCase(2147483647)]
        public void CurrentChanged_DifferentArguments_CurrentCurrentIsCorrect(int newCurrent)
        {
            _chargerSimulatorSource.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = newCurrent});
            Assert.That(_uut.NewCurrent, Is.EqualTo(newCurrent));
        }

        [TestCase(true)]
        public void StartCharge_Started_ConnectionIsTrue(bool isConnected)
        {
            _stationControlSource.Connected = isConnected;
            
            Assert.That(_uut.Connected, Is.EqualTo(isConnected));
        }

    }
}
