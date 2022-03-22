﻿using System;
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
        private UsbChargerSimulator _chargerSimulatorSource;
        //private FakeStationControl _stationControlSource;

        [SetUp]
        public void Setup()
        {
            
            _chargerSimulatorSource = new UsbChargerSimulator();
            _uut = new ChargeControl(_chargerSimulatorSource);
            
        }

       

        //[TestCase(-1)]
        //[TestCase(0)]
        //[TestCase(1)]
        //[TestCase(4)]
        //[TestCase(5)]
        //[TestCase(6)]
        //[TestCase(499)]
        //[TestCase(500)]
        //[TestCase(501)]
        //[TestCase(2147483647)]
        //public void CurrentChanged_DifferentArguments_CurrentCurrentIsCorrect(double newCurrent)
        //{
        //    _chargerSimulatorSource.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = newCurrent});
        //    Assert.That(_uut.NewCurrent, Is.EqualTo(newCurrent));
        //}

        [Test]
        public void StartCharge_Started_ConnectionIsTrue()
        {
            _uut.StartCharge();
            Assert.That(_uut.Connected, Is.EqualTo(true));
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(10)]
        [TestCase(600)]
        public void StopCharge_Started_ConnectionIsFalse(double current)
        {
            _uut.NewCurrent = current;
            _uut.StopCharge();
            Assert.That(_uut.Connected, Is.EqualTo(false));
        }

    }
}
