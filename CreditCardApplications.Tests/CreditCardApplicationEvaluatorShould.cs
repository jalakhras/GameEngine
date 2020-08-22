using Moq;
using Xunit;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {

            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mocking Property Hierarchies 
            //mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { Age = 19 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator =
                new Mock<IFrequentFlyerNumberValidator>();
            //mocking Property Hierarchies 
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            mockValidator.Setup(x => x.IsValid("x")).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.Is<string>(num => num.StartsWith("y")))).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsInRange("a", "z", Range.Inclusive))).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsIn("z", "y", "x"))).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsRegex("[a-z]"))).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication
            {
                GrossAnnualIncome = 19_999,
                Age = 42,
                FrequentFlyerNumber = "x"
                //FrequentFlyerNumber = "y"
            };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }


        [Fact]
        public void ReferInvalidFrequentFlyerApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator =
                new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);
            //mocking Property Hierarchies 
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            //Mock<IFrequentFlyerNumberValidator> mockValidator =
            //    new Mock<IFrequentFlyerNumberValidator>();

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication();

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        //out
        [Fact]
        public void DeclineLowIncomeApplicationsOutDemo()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            // commit this 
            bool isValid = true;
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>(), out isValid));

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication
            {
                GrossAnnualIncome = 19_999,
                Age = 42
            };

            CreditCardApplicationDecision decision = sut.EvaluateUsingOut(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void ReferWhenLicenseKeyExpired()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //commit this moking proprty
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
           // mockValidator.Setup(x => x.LicenseKey).Returns("EXPIRED");
            //mockValidator.Setup(x => x.LicenseKey).Returns(GetLicenseKeyExpiryString);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { Age = 42 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        } 
        
        //[Fact]
        ////mocking Property Hierarchies 
        //public void HierarchiesReferWhenLicenseKeyExpired()
        //{
        //    //var mockLicenseData = new Mock<ILicenseData>();
        //    //mockLicenseData.Setup(x => x.LicenseKey).Returns("EXPIRED");
        //    //var mockServiceInfo = new Mock<IServiceInformation>();
        //    //mockServiceInfo.Setup(x => x.License).Returns(mockLicenseData.Object);
        //    //var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
        //    //mockValidator.Setup(x => x.ServiceInformation).Returns(mockServiceInfo.Object);
        //    var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
        //    mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("EXPIRED");

        //    mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        //    var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

        //    var application = new CreditCardApplication { Age = 42 };

        //    CreditCardApplicationDecision decision = sut.Evaluate(application);

        //    Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        //}

        string GetLicenseKeyExpiryString()
        {
            return "EXPIRED";
        }
    }
}
