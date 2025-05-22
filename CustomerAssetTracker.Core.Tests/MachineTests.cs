using System;
using CustomerAssetTracker.Core;
using NUnit.Framework;

namespace CustomerAssetTracker.Core.Tests
{
    [TestFixture]
    public class MachineTests
    {
        [Test]
        public void Constructor_InitializesCollections()
        {
            // Act
            var machine = new Machine();

            // Assert
            Assert.That(machine.Licenses, Is.Not.Null);
            Assert.That(machine.ServiceRecords, Is.Not.Null);
            Assert.That(machine.Licenses, Is.Empty);
            Assert.That(machine.ServiceRecords, Is.Empty);
        }

        [Test]
        public void GetFullName_ReturnsExpectedFormat()
        {
            // Arrange
            var machine = new Machine
            {
                MachineType = Machine.MachineTypes.Cmm,
                Name = "TestName",
                SerialNumber = "SN123",
                Manufacturer = "TestManufacturer"
            };

            // Act
            var fullName = machine.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("Cmm: TestName, SN: SN123 (TestManufacturer)"));
        }

        [Test]
        public void GetFullName_HandlesNullProperties()
        {
            // Arrange
            var machine = new Machine();

            // Act
            var fullName = machine.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("Cmm: , SN:  ()"));
        }
    }
}
