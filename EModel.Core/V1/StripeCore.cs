using EModel.Entities.DTOs;
using Stripe;

namespace EModel.Core.V1
{
    public class StripeCore
    {
        public StripeCore()
        {
            //Entrar a la plataforma de stripe para generar token
            StripeConfiguration.ApiKey = "sk_test_51LYAmCI8863D8Oqiy0pbAK6BvxeNunj0WQULoCF5byhioFSA9MiTP8ueQeXUzaC2IRU0g13GlFLR83idRsYd9Pd600Ov1wq0IL";
        }

        public PaymentMethod CreatePaymentMethod(CardInfoDto card, string idCustomer)
        {
            var options = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Number = card.CardNumber,
                    ExpMonth = card.ExpMonth,
                    ExpYear = card.ExpYear,
                    Cvc = card.CVV,
                },
            };
            var service = new PaymentMethodService();
            var result = service.Create(options);
            AttachPaymentMethodToCustomer(result.Id, idCustomer);
            return result;
        }

        public PaymentIntent PlacePayment(string methodToken, int amount, string customerToken, string currency = "usd")
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount * 100,
                Currency = currency,
                PaymentMethod = methodToken,
                Customer = customerToken,
                Confirm = true
            };
            var service = new PaymentIntentService();
            return service.Create(options);
        }

        public string CreateCustomer(string name, string email)
        {
            var options = new CustomerCreateOptions
            {
                Name = name,
                Email = email
            };
            var service = new CustomerService();
            return service.Create(options).Id;
        }

        private void AttachPaymentMethodToCustomer(string idPaymentMethod, string idCustomer)
        {
            var options = new PaymentMethodAttachOptions
            {
                Customer = idCustomer,
            };
            var service = new PaymentMethodService();
            service.Attach(idPaymentMethod, options);
        }

    }
}
