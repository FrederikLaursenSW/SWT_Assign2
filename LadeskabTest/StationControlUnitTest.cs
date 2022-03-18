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
        private StationControl _uut;
       
        [SetUp]
        public void Setup()
        {
            _doorSource = Substitute.For<IDoor>();
            _uut = new StationControl(_doorSource);
        }

        [Test]
        public void ChangedState_DoorOpens_CurrentDoorIsOpenTrue()
        {
            _doorSource. DoorChangedEvent += Raise.EventWith(new DoorEvents { DoorIsOpen = true});
            Assert.That(_uut.CurrentDoorIsOpen, Is.True);
        }
    }
}
