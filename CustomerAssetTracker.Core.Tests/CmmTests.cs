using System;
using CustomerAssetTracker.Core;
using NUnit.Framework;

namespace CustomerAssetTracker.Core.Tests
{
    [TestFixture]
    public class CmmTests
    {
        // Komentář: Testuje, zda konstruktor správně nastaví MachineType.
        [Test]
        public void Constructor_ShouldSetMachineTypeToCmm()
        {
            // Arrange
            var cmm = new Cmm();

            // Act (již proběhlo v Arrange)

            // Assert
            Assert.That(cmm.MachineType, Is.EqualTo(Machine.MachineTypes.Cmm), "MachineType should be Cmm.");
        }

        // Komentář: Testuje, zda nastavení kladné hodnoty pro SizeX funguje.
        [Test]
        public void SizeX_ShouldSetPositiveValue()
        {
            // Arrange
            var cmm = new Cmm();
            int expectedSize = 100;

            // Act
            cmm.SizeX = expectedSize;

            // Assert
            Assert.That(cmm.SizeX, Is.EqualTo(expectedSize), "SizeX should be set to the provided positive value.");
        }

        // Komentář: Testuje, zda nastavení záporné hodnoty pro SizeX vyvolá výjimku.
        // Atribut [TestCase] umožňuje spouštět test s různými vstupními daty.
        // Atribut [Throws] ověřuje, že metoda vyvolá specifickou výjimku.
        [TestCase(-1)]
        [TestCase(-100)]
        public void SizeX_ShouldThrowArgumentOutOfRangeException_WhenNegativeValue(int negativeSize)
        {
            // Arrange
            var cmm = new Cmm();

            // Act & Assert: Používáme Assert.Throws pro ověření výjimky.
            // Lambda výraz () => cmm.SizeX = negativeSize; je kód, který očekáváme, že vyvolá výjimku.
            Assert.Throws<ArgumentOutOfRangeException>(() => cmm.SizeX = negativeSize,
                "Setting SizeX to a negative value should throw an ArgumentOutOfRangeException.");
        }

        // Komentář: Podobné testy bys napsal/a i pro SizeY a SizeZ.
        // [Test]
        // public void SizeY_ShouldSetPositiveValue() { ... }
        // [TestCase(-1)]
        // public void SizeY_ShouldThrowArgumentOutOfRangeException_WhenNegativeValue(int negativeSize) { ... }
        // atd.

        // Komentář: Testovací metoda pro ověření GetFullName() na základní třídě Machine.
        // Můžeme ji testovat zde, protože Cmm dědí z Machine.
        [Test]
        public void GetFullName_ShouldReturnCorrectFormat()
        {
            // Arrange
            var cmm = new Cmm();
            cmm.Name = "CMM_XYZ";
            cmm.SerialNumber = "SN12345";
            cmm.Manufacturer = "LK";
            // MachineType je již nastaven konstruktorem Cmm() na MachineTypes.Cmm

            string expectedFullName = "Cmm: CMM_XYZ, SN: SN12345 (LK)";

            // Act
            string actualFullName = cmm.GetFullName();

            // Assert
            Assert.That(actualFullName, Is.EqualTo(expectedFullName), "GetFullName should return the correct formatted string.");
        }
    }
}
