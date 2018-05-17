using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EColorCodeResistanceCalculator.Calculator;
using EColorCodeResistanceCalculator.Controllers;
using Microsoft.AspNetCore.Mvc;
using EColorCodeResistanceCalculator.Models;


namespace EColorCodeResistanceCalculatorTests
{
    [TestClass]
    public class ColorCodeCalculatorTests
    {
        [TestMethod]
        public void Argument_Null_Exception_Test()
        {
            //Arrange
            OhmValueCalculator ohmValueCalculator = new OhmValueCalculator();
            HomeController controller = new HomeController(ohmValueCalculator);

            //Act
            JsonResult result = (JsonResult)controller.ResistanceValue(null);

            //Assert
            Assert.AreEqual("Exception ocurred while calculating resistance value: Value cannot be null.", result.Value);
        }

        [TestMethod]
        public void Index_Not_Null_Result_Test()
        {
            // Arrange
            OhmValueCalculator ohmValueCalculator = new OhmValueCalculator();
            HomeController controller = new HomeController(ohmValueCalculator);
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Zero_Band_Resister_Test()
        {
            Exception exceptionResult = null;

            try
            {    // Arrange 
                IOhmValueCalculator ohmValueCalculator = new OhmValueCalculator();

                // Act
                ohmValueCalculator.CalculateOhmValue(null, null, null, null);
            }
            catch (Exception exception)
            {
                exceptionResult = exception;
            }

            // Assert
            Assert.IsNotNull(exceptionResult);
            Assert.IsInstanceOfType(exceptionResult, typeof(ArgumentException));
        }

        [TestMethod]
        public void Four_Band_Register_Test()
        {
            //Arrange
            IOhmValueCalculator ohmValueCalculator = new OhmValueCalculator();

            //Act
            CalculatorResult result = ohmValueCalculator.CalculateOhmValue("yellow", "violet", "red", "gold");

            //Assert
            Assert.AreEqual("4935", result.MaxResistance);
        }

        [TestMethod]
        public void Large_Resistance_Tests()
        {
            //Arrange
            IOhmValueCalculator ohmValueCalculator = new OhmValueCalculator();

            //Act
            CalculatorResult result = ohmValueCalculator.CalculateOhmValue("white", "white", "white", "silver");

            //Assert
            Assert.AreEqual("108,900M", result.MaxResistance);
        }

        [TestMethod]
        public void Small_Resistance_Test()
        {
            //Arrange
            IOhmValueCalculator ohmValueCalculator = new OhmValueCalculator();

            //Act
            CalculatorResult result = ohmValueCalculator.CalculateOhmValue("brown", "black", "black", "brown");

            //Assert
            Assert.AreEqual("10.1", result.MaxResistance);
        }
    } 
}
