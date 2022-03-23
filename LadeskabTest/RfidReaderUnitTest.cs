using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabTest
{
    public class RfidReaderUnitTest
    {
        private RfidReader _uut; // Skal dette være et interface eller en klasse
        private RfidEvent _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;

            _uut = new RfidReader();

           
        }

        [TestCase(14)]
        public void OnRfidDetected_EventFired(int id)
        {
            _uut.RfidDetectedEvent +=
               (o, args) =>
               {
                   _receivedEventArgs = args;
               };

            _uut.OnRfidRead(id);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [TestCase(14, 20)]
        public void OnRfidDetected_EqualsId(int id, int newId)
        {
            _uut.RfidDetectedEvent +=
               (o, args) =>
               {
                   _receivedEventArgs = args;
               };

            _uut.OnRfidRead(id);
            

            _uut.RfidDetectedEvent -=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };


            Assert.That(_receivedEventArgs.RfidId, Is.EqualTo(id));
        }


        [TestCase(14)]
        public void OnRfidDetected_EventNotFired(int id)
        {

            _uut.OnRfidRead(id);

            Assert.That(_receivedEventArgs, Is.Null);
        }


        [TestCase(14)]
        [TestCase(-14)]
        public void OnRfidRead_equal_to_id(int id)
        {
            _uut.RfidDetectedEvent +=
               (o, args) =>
               {
                   _receivedEventArgs = args;
               };

            _uut.OnRfidRead(id);
            Assert.That(_receivedEventArgs.RfidId, Is.EqualTo(id));
        }
    }
}
