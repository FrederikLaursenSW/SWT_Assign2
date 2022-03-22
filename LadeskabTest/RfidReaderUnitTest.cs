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

            _uut.RfidDetectedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };
        }


        [TestCase(14)]
        public void OnRfidRead_equal_to_id(int id)
        {
            _uut.OnRfidRead(id);
            Assert.That(_receivedEventArgs.RfidId, Is.EqualTo(id));
        }



    }
}
