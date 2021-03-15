namespace Filed.PaymentGateway.Test
{
    using Filed.PaymentGateway.API.Controllers;
    using Filed.PaymentGateway.BusinessInterface;
    using Filed.PaymentGateway.BusinessManager;
    using Filed.PaymentGateway.BusinessModel;
    using Filed.PaymentGateway.Data;
    using Filed.PaymentGateway.DataImplementation;
    using Filed.PaymentGateway.DataInterface;
    using Filed.PaymentGateway.PaymentProcessor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    /// <summary>
    /// Payment process test methods
    /// </summary>
    [TestClass]
    public class PaymentProcessTest
    {
        /// <summary>
        /// The service provider
        /// </summary>
        IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var services = new ServiceCollection();
            services.AddTransient<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddTransient<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddTransient<IPremiumPaymentGateway, PremiumPaymentGateway>();
            services.AddTransient<IPaymentProcessManager, PaymentProcessManager>();
            services.AddTransient<DataInterface.IPaymentInformation, PaymentInformationData>();
            services.AddTransient<IPaymentState, PaymentStateData>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<FiledDBContext>(opt => opt.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Filed;Trusted_Connection=True;"));
            serviceProvider = services.BuildServiceProvider();
            var unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            var data1 = unitOfWork.paymentState.GetAllIncludeAll().Result;
        }

        /// <summary>
        /// Tests the payment process.
        /// </summary>
        [TestMethod]
        public void TestPaymentProcess()
        {
            var paymentProcessor = (IPaymentProcessManager)serviceProvider.GetService(typeof(IPaymentProcessManager));
            PaymentController controller = new PaymentController(paymentProcessor);
            var paymentRequest = GetPaymentRequest();
            ValidateCheapPaymentMethod(paymentRequest, controller);
            ValidatePremiumPaymentMethod(paymentRequest, controller);
            ValidateExpensivePaymentMethod(paymentRequest, controller);
            ValidateRetryCountForPremium(controller);
        }

        /// <summary>
        /// Tests the eager loading.
        /// </summary>
        [TestMethod]
        public void TestEagerLoading() 
        {
            var paymentProcessor = (IPaymentProcessManager)serviceProvider.GetService(typeof(IPaymentProcessManager));
            PaymentController controller = new PaymentController(paymentProcessor);
            var paymentRequest = GetPaymentRequest();
            controller.ProcessPayment(paymentRequest);
            var unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            var paymentStates = unitOfWork.paymentState.GetAllIncludeAll().Result.Take(50);
            foreach (var paymentState in paymentStates)
            {
                Assert.IsNotNull(paymentState.PaymentInformation);
            }
        }
        /// <summary>
        /// Validates the cheap payment method.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <param name="controller">The controller.</param>
        private void ValidateCheapPaymentMethod(PaymentInformationModel paymentInformation, PaymentController controller)
        {
            paymentInformation.Amount = BusinessConstants.CHEAPPAYMENT_LIMIT;
            var response = controller.ProcessPayment(paymentInformation);
            ValidatePaymentDetailsInDB(response.PaymentDetail);
            Assert.AreEqual(PaymentMode.Cheap, response.PaymentType);
            Assert.AreNotEqual(PaymentMode.Expensive, response.PaymentType);
            Assert.AreNotEqual(PaymentMode.Premium, response.PaymentType);
        }
        /// <summary>
        /// Validates the expensive payment method.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <param name="controller">The controller.</param>
        private void ValidateExpensivePaymentMethod(PaymentInformationModel paymentInformation, PaymentController controller)
        {
            paymentInformation.Amount = BusinessConstants.EXPENSIVEPAYMENT_LIMIT;
            var response = controller.ProcessPayment(paymentInformation);
            ValidatePaymentDetailsInDB(response.PaymentDetail);
            Assert.AreNotEqual(PaymentMode.Cheap, response.PaymentType);
            Assert.AreEqual(PaymentMode.Expensive, response.PaymentType);
            Assert.AreNotEqual(PaymentMode.Premium, response.PaymentType);
        }
        /// <summary>
        /// Validates the premium payment method.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <param name="controller">The controller.</param>
        private void ValidatePremiumPaymentMethod(PaymentInformationModel paymentInformation, PaymentController controller)
        {
            paymentInformation.Amount = BusinessConstants.PREMIUMPAYMENT_LIMIT;
            var response = controller.ProcessPayment(paymentInformation);
            ValidatePaymentDetailsInDB(response.PaymentDetail);
            Assert.AreNotEqual(PaymentMode.Cheap, response.PaymentType);
            Assert.AreNotEqual(PaymentMode.Expensive, response.PaymentType);
            Assert.AreEqual(PaymentMode.Premium, response.PaymentType);
        }

        /// <summary>
        /// Validates the retry count for premium.
        /// </summary>
        /// <param name="controller">The controller.</param>
        private void ValidateRetryCountForPremium(PaymentController controller)
        {
            var paymentRequest = GetPaymentRequest();
            paymentRequest.Amount = BusinessConstants.PREMIUM_FAILURE_AMOUNT;
            var response = controller.ProcessPayment(paymentRequest);
            ValidatePaymentDetailsInDB(response.PaymentDetail);
            Assert.AreEqual(BusinessConstants.PREMIUM_RETRY_COUNT, response.RetryCount);
            Assert.AreEqual(false, response.IsSuccess);
        }

        /// <summary>
        /// Validates the payment details in database.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        private void ValidatePaymentDetailsInDB(PaymentInformationModel paymentInformation)
        {
            var unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            var dbPaymentDetails = unitOfWork.paymentInformation.Get(paymentInformation.Id).Result;
            Assert.IsNotNull(dbPaymentDetails);
            Assert.AreEqual(dbPaymentDetails.Amount, paymentInformation.Amount);
            Assert.AreEqual(dbPaymentDetails.CardHolder, paymentInformation.CardHolder);
            Assert.AreEqual(dbPaymentDetails.CreditCardNumber, paymentInformation.CreditCardNumber);
            Assert.AreEqual(dbPaymentDetails.ExpirationDate, paymentInformation.ExpirationDate);
        }

        /// <summary>
        /// Gets the payment request.
        /// </summary>
        /// <returns></returns>
        private PaymentInformationModel GetPaymentRequest()
        {
            return new PaymentInformationModel()
            {
                CardHolder = "Mox Shah",
                CreditCardNumber = "8933893389338933",
                SecurityCode = "123",
                ExpirationDate = DateTime.Today.AddDays(1),
                Amount = 10
            };
        }
    }
}
