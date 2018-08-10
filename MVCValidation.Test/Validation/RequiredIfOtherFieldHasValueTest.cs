using MVCValidation.Validation;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCValidation.Test.Validation
{
    [TestFixture]
    public class RequiredIfOtherFieldHasValueTest
    {
        internal class TestObjectOne
        {
            [RequiredIfOtherFieldHasValue("PropertyTwo")]
            public decimal? PropertyOne { get; set; }

            public string PropertyTwo { get; set; }
        }

        internal class TestObjectTwo
        {
            [DisplayName("Property Three")]
            [RequiredIfOtherFieldHasValue("PropertyFour")]
            public decimal? PropertyThree { get; set; }

            [DisplayName("Property Four")]
            public string PropertyFour { get; set; }
        }

        [TestCase(null, null)]
        [TestCase(null, "")]
        [TestCase(-1, "cm")]
        [TestCase(0, "cm")]
        [TestCase(1, "cm")]
        [TestCase(2, "cm")]
        [TestCase(2.2, "cm")]
        [TestCase(-1, "mm")]
        [TestCase(0, "mm")]
        [TestCase(1, "mm")]
        [TestCase(2, "mm")]
        [TestCase(2.2, "mm")]
        [TestCase(-1, "")]
        [TestCase(0, "")]
        [TestCase(1, "")]
        [TestCase(2, "")]
        [TestCase(2.2, "")]
        public void TestObjectOne_ValidCases(decimal? propertyOne, string propertyTwo)
        {
            //Arrange
            var testObject = new TestObjectOne();
            testObject.PropertyOne = propertyOne;
            testObject.PropertyTwo = propertyTwo;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(0, validationResults.Count());
        }

        [TestCase(null, null)]
        [TestCase(null, "")]
        [TestCase(-1, "cm")]
        [TestCase(0, "cm")]
        [TestCase(1, "cm")]
        [TestCase(2, "cm")]
        [TestCase(2.2, "cm")]
        [TestCase(-1, "mm")]
        [TestCase(0, "mm")]
        [TestCase(1, "mm")]
        [TestCase(2, "mm")]
        [TestCase(2.2, "mm")]
        [TestCase(-1, "")]
        [TestCase(0, "")]
        [TestCase(1, "")]
        [TestCase(2, "")]
        [TestCase(2.2, "")]
        public void TestObjectTwo_ValidCases(decimal? propertyThree, string propertyFour)
        {
            //Arrange
            var testObject = new TestObjectTwo();
            testObject.PropertyThree = propertyThree;
            testObject.PropertyFour = propertyFour;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(0, validationResults.Count());
        }

        [TestCase(null, "mm")]
        [TestCase(null, "cm")]
        public void TestObjectOne_InValidCases(decimal? propertyOne, string propertyTwo)
        {
            //Arrange
            var testObject = new TestObjectOne();
            testObject.PropertyOne = propertyOne;
            testObject.PropertyTwo = propertyTwo;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(1, validationResults.Count());
        }

        [TestCase(null, "mm")]
        [TestCase(null, "cm")]
        public void TestObjectTwo_InValidCases(decimal? propertyThree, string propertyFour)
        {
            //Arrange
            var testObject = new TestObjectTwo();
            testObject.PropertyThree = propertyThree;
            testObject.PropertyFour = propertyFour;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual(1, validationResults.Count());
        }

        [TestCase(null, "mm")]
        [TestCase(null, "cm")]
        public void TestObjectOne_InValidCasesMessage(decimal? propertyOne, string propertyTwo)
        {
            //Arrange
            var testObject = new TestObjectOne();
            testObject.PropertyOne = propertyOne;
            testObject.PropertyTwo = propertyTwo;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual("PropertyOne must have value, when PropertyTwo has value", validationResults[0].ErrorMessage);
        }

        [TestCase(null, "mm")]
        [TestCase(null, "cm")]
        public void TestObjectTwo_InValidCasesMessage(decimal? propertyThree, string propertyFour)
        {
            //Arrange
            var testObject = new TestObjectTwo();
            testObject.PropertyThree = propertyThree;
            testObject.PropertyFour = propertyFour;

            //Act
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(testObject, null, null);
            Validator.TryValidateObject(testObject, ctx, validationResults, true);

            //Assert
            Assert.AreEqual("Property Three must have value, when Property Four has value", validationResults[0].ErrorMessage);
        }
    }
}