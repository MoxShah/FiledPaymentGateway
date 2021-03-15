namespace Filed.PaymentGateway.API.Controllers
{
    using Filed.PaymentGateway.BusinessInterface;
    using Filed.PaymentGateway.BusinessModel;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Payment Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Produces("application/json")]
    [Route("api/Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        /// <summary>
        /// The payment processor
        /// </summary>
        IPaymentProcessManager paymentProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        /// <param name="paymentProcessor">The payment processor.</param>
        public PaymentController(IPaymentProcessManager paymentProcessor)
        {
            this.paymentProcessor = paymentProcessor;
        }

        /// <summary>
        /// Processes the payment.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProcessPayment")]
        public PaymentResponse ProcessPayment(PaymentInformationModel paymentInformation)
        {
            return paymentProcessor.ProcessPayment(paymentInformation);
        }
    }
}
