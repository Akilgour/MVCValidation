using MVCValidation.Validation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCValidation.Test.Validation
{
    internal class TestObjectOne
    {
        [DisplayName("Property One")]
        public string PropertyOne { get; set; }

        [RequiredIfOtherFieldIsValue("PropertyOne", "TestValue")]
        [DisplayName("Property Two")]
        public string PropertyTwo { get; set; }

        [RequiredIfOtherFieldIsValue("PropertyOne", "TestValue")]
        public string PropertyThree { get; set; }
    }

    internal class TestObjectTwo
    {
        public string PropertyFour { get; set; }

        [DisplayName("Property Five")]
        [RequiredIfOtherFieldIsValue("PropertyFour", "OtherTestValue")]
        public string PropertyFive { get; set; }

        [RequiredIfOtherFieldIsValue("PropertyFour", "OtherTestValue")]
        public string PropertySix { get; set; }
    }

    internal class TestObjectThree
    {
        [RequiredIfOtherFieldIsValue("PropertyEight", "OtherTestValue")]
        public DateTime PropertySeven { get; set; }

        public string PropertyEight { get; set; }
    }

    [TestFixture]
    public class RequiredIfOtherFieldIsValueTest
    {
        [TestCase("a", "b")]
        [TestCase("aa", "bb")]
        public void IsValidWhenPropertyOneIsTestValue(string propertyTwo, string propertyThree)
        {
            //Arrange
            var testObject = new TestObjectOne();
            testObject.PropertyOne = "TestValue";
            testObject.PropertyTwo = propertyTwo;
            testObject.PropertyThree = propertyThree;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(0, validationResults.Count());
        }

        [TestCase("NotTestValue", "a", "b")]
        [TestCase("NotTestValue", "aa", "bb")]
        [TestCase("NotTestValue", "", "")]
        [TestCase("NotTestValue", " ", "")]
        [TestCase("NotTestValue", "", " ")]
        [TestCase("NotTestValue", " ", " ")]
        [TestCase("NotTestValue", null, "")]
        [TestCase("NotTestValue", null, " ")]
        [TestCase("NotTestValue", null, "a")]
        [TestCase("NotTestValue", null, null)]
        [TestCase("NotTestValue", "", null)]
        [TestCase("NotTestValue", " ", null)]
        [TestCase("NotTestValue", "a", null)]
        [TestCase("NotTestValue", null, null)]
        [TestCase("NotTestValueTwo", "a", "b")]
        [TestCase("NotTestValueTwo", "aa", "bb")]
        [TestCase("NotTestValueTwo", "", "")]
        [TestCase("NotTestValueTwo", " ", "")]
        [TestCase("NotTestValueTwo", "", " ")]
        [TestCase("NotTestValueTwo", " ", " ")]
        [TestCase("NotTestValueTwo", null, "")]
        [TestCase("NotTestValueTwo", null, " ")]
        [TestCase("NotTestValueTwo", null, "a")]
        [TestCase("NotTestValueTwo", null, null)]
        [TestCase("NotTestValueTwo", "", null)]
        [TestCase("NotTestValueTwo", " ", null)]
        [TestCase("NotTestValueTwo", "a", null)]
        [TestCase("NotTestValueTwo", null, null)]
        [TestCase(" ", "a", "b")]
        [TestCase(" ", "aa", "bb")]
        [TestCase(" ", "", "")]
        [TestCase(" ", " ", "")]
        [TestCase(" ", "", " ")]
        [TestCase(" ", " ", " ")]
        [TestCase(" ", null, "")]
        [TestCase(" ", null, " ")]
        [TestCase(" ", null, "a")]
        [TestCase(" ", null, null)]
        [TestCase(" ", "", null)]
        [TestCase(" ", " ", null)]
        [TestCase(" ", "a", null)]
        [TestCase(" ", null, null)]
        [TestCase("", "a", "b")]
        [TestCase("", "aa", "bb")]
        [TestCase("", "", "")]
        [TestCase("", " ", "")]
        [TestCase("", "", " ")]
        [TestCase("", " ", " ")]
        [TestCase("", null, "")]
        [TestCase("", null, " ")]
        [TestCase("", null, "a")]
        [TestCase("", null, null)]
        [TestCase("", "", null)]
        [TestCase("", " ", null)]
        [TestCase("", "a", null)]
        [TestCase("", null, null)]
        [TestCase(null, "a", "b")]
        [TestCase(null, "aa", "bb")]
        [TestCase(null, "", "")]
        [TestCase(null, " ", "")]
        [TestCase(null, "", " ")]
        [TestCase(null, " ", " ")]
        [TestCase(null, null, "")]
        [TestCase(null, null, " ")]
        [TestCase(null, null, "a")]
        [TestCase(null, null, null)]
        [TestCase(null, "", null)]
        [TestCase(null, " ", null)]
        [TestCase(null, "a", null)]
        [TestCase(null, null, null)]
        public void IsValidWhenPropertyOneIsNotTestValue(string propertyOne, string propertyTwo, string propertyThree)
        {
            //Arrange
            var testObject = new TestObjectOne();
            testObject.PropertyOne = propertyOne;
            testObject.PropertyTwo = propertyTwo;
            testObject.PropertyThree = propertyThree;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(0, validationResults.Count());
        }

        [TestCase("", "", 2)]
        [TestCase(null, null, 2)]
        [TestCase(" ", "", 2)]
        [TestCase("", " ", 2)]
        [TestCase(" ", " ", 2)]
        [TestCase("", "a", 1)]
        [TestCase(null, "a", 1)]
        [TestCase(" ", "a", 1)]
        [TestCase("a", "", 1)]
        [TestCase("a", null, 1)]
        [TestCase("a", " ", 1)]
        public void IsNotValidWhenBothBlankAndPropertyOneIsTestValue_ExpectedCount(string propertyTwo, string propertyThree, int expected)
        {
            //Arrange
            var testObject = new TestObjectOne();
            testObject.PropertyOne = "TestValue";
            testObject.PropertyTwo = propertyTwo;
            testObject.PropertyThree = propertyThree;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(expected, validationResults.Count());
        }

        [TestCase("", "a", "The Property Two field is required.")]
        [TestCase(null, "a", "The Property Two field is required.")]
        [TestCase(" ", "a", "The Property Two field is required.")]
        [TestCase("a", "", "The PropertyThree field is required.")]
        [TestCase("a", null, "The PropertyThree field is required.")]
        [TestCase("a", " ", "The PropertyThree field is required.")]
        public void IsNotValidWhenBothBlankAndPropertyOneIsTestValue_ExpectedMessage(string propertyTwo, string propertyThree, string expected)
        {
            //Arrange
            var testObject = new TestObjectOne();
            testObject.PropertyOne = "TestValue";
            testObject.PropertyTwo = propertyTwo;
            testObject.PropertyThree = propertyThree;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(expected, validationResults[0].ErrorMessage);
        }

        [TestCase("", "a", "The Property Five field is required.")]
        [TestCase(null, "a", "The Property Five field is required.")]
        [TestCase(" ", "a", "The Property Five field is required.")]
        [TestCase("a", "", "The PropertySix field is required.")]
        [TestCase("a", null, "The PropertySix field is required.")]
        [TestCase("a", " ", "The PropertySix field is required.")]
        public void IsNotValidWhenBothBlankAndPropertyOneIsOtherTestValue_ExpectedMessage(string propertyFive, string propertySix, string expected)
        {
            //Arrange
            var testObject = new TestObjectTwo();
            testObject.PropertyFour = "OtherTestValue";
            testObject.PropertyFive = propertyFive;
            testObject.PropertySix = propertySix;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(expected, validationResults[0].ErrorMessage);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("aa")]
        [TestCase("bb bb ")]
        [TestCase(" ccc ")]
        public void TestObjectThree_TestProepertyNotSetToTriggerText(string propertyEight)
        {
            //Arrange
            var testObject = new TestObjectThree();
            testObject.PropertyEight = propertyEight;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(0, validationResults.Count());
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void TestObjectThree_TestProepertySetToTrigerMessageButOtherValueSet(int dayCount)
        {
            //Arrange
            var testObject = new TestObjectThree();
            testObject.PropertySeven = DateTime.Now.AddDays(dayCount);
            testObject.PropertyEight = "OtherTestValue";

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(0, validationResults.Count());
        }

        [Test]
        public void TestObjectThree_DateBlankOther_ExpectedMessage()
        {
            //Arrange
            //Arrange
            var testObject = new TestObjectThree();
            testObject.PropertyEight = "OtherTestValue";

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual("The PropertySeven field is required.", validationResults[0].ErrorMessage);
        }
    }
}