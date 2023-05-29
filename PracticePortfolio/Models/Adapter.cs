using System.Text.RegularExpressions;

namespace PracticePortfolio.Models {
    
    public interface INewPaymentDataValidator
    {
        public bool IsPaymentDataValid(decimal amount, string cardNumber, string cvv);
    }

    public interface ILegacyPaymentDataValidator
    {
        public bool IsPaymentDataValid(string paymentData);
    }

    public class LegacyPaymentDataValidator : ILegacyPaymentDataValidator
    {

        public bool IsPaymentDataValid(string paymentData)
        {
            string pattern = @"^[0-9]+([.][0-9]{0,2})?,[0-9]{16},[0-9]{3}$";
            Regex regexPattern = new(pattern);

            return regexPattern.IsMatch(paymentData);

        }

    }

    public class NewPaymentDataValidator : INewPaymentDataValidator
    {
        public bool IsPaymentDataValid(decimal amount, string cardNumber, string cvv)
        {
            string amountPattern = @"^[0-9]+([.][0-9]{0,2})?$";
            string cardNumberPattern = @"^[0-9]{16}$";
            string cvvPattern = @"^[0-9]{3}$";

            Regex amountRegex = new(amountPattern);
            Regex cardNumberRegex = new(cardNumberPattern);
            Regex cvvRegex = new(cvvPattern);

            return amountRegex.IsMatch(amount.ToString()) &&
                   cardNumberRegex.IsMatch(cardNumber) &&
                   cvvRegex.IsMatch(cvv);
        }

    }

    
    public interface IPaymentGateway
    {
        string ProcessPayment(string paymentData);
    }

    public class LegacyPaymentGateway : IPaymentGateway
    {
        private readonly ILegacyPaymentDataValidator _validator;

        public LegacyPaymentGateway()
        {
            _validator = new LegacyPaymentDataValidator();
        }

        public LegacyPaymentGateway(ILegacyPaymentDataValidator validator)
        {
            _validator = validator;
        }

        public string ProcessPayment(string paymentData)
        {
            if (_validator.IsPaymentDataValid(paymentData))
            {
                return $"A payment was made using the legacy system with the following data: {paymentData}.";
            }
            else
            {
                return "Invalid payment data.";
            }
        }

    }

    public interface INewPaymentGateway
    {
        string MakePayment(decimal amount, string cardNumber, string cvv);
    }

    public class NewPaymentGateway : INewPaymentGateway
    {
        private readonly INewPaymentDataValidator _validator;

        public NewPaymentGateway()
        {
            _validator = new NewPaymentDataValidator();
        }

        public NewPaymentGateway(INewPaymentDataValidator validator)
        {
            _validator = validator;
        }

        public string MakePayment(decimal amount, string cardNumber, string cvv)
        {
            if (_validator.IsPaymentDataValid(amount, cardNumber, cvv))
            {
                return $"A ${amount:F2} payment was made using a credit card with the credit card number {cardNumber} and a CVV of {cvv} using the new payment gateway.";
            }
            else
            {
                return "Invalid payment data.";
            }
        }

    }

    public class PaymentService
    {
        private readonly IPaymentGateway _paymentGateway;

        public PaymentService(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }

        public string ProcessPaymentRequest(string paymentData)
        {
            return _paymentGateway.ProcessPayment(paymentData);
        }
    }

    public class NewPaymentGatewayAdapter : IPaymentGateway
    {
        public NewPaymentGatewayAdapter(INewPaymentGateway newPaymentGateway)
        {

        }

        public string ProcessPayment(string paymentDetails)
        {
            return string.Empty;
        }
    }


    public class Adapter
    {
        public string Run(string paymentData)
        {
            IPaymentGateway legacyPaymentGateway = new LegacyPaymentGateway();
            PaymentService paymentService = new(legacyPaymentGateway);
            return paymentService.ProcessPaymentRequest(paymentData);
        }

        public string Run(decimal amount, string cardNumber, string cvv)
        {
            // Create an instance of the legacy payment gateway
            IPaymentGateway legacyPaymentGateway = new LegacyPaymentGateway();

            // Create an instance of the payment service using the legacy payment gateway
            PaymentService paymentService = new(legacyPaymentGateway);

            // Process payment using the existing payment gateway
            paymentService.ProcessPaymentRequest("test");

            /*            // Now, let's introduce the new payment gateway

                        // Create an instance of the new payment gateway
                        INewPaymentGateway newPaymentGateway = new NewPaymentGateway();

                        // Create an adapter for the new payment gateway
                        IPaymentGateway newPaymentGatewayAdapter = new NewPaymentGatewayAdapter(newPaymentGateway);

                        // Update the payment service to use the new payment gateway through the adapter
                        paymentService = new PaymentService(newPaymentGatewayAdapter);*/

            // Process payment using the new payment gateway
            return paymentService.ProcessPaymentRequest("test");
        }
    }
}
