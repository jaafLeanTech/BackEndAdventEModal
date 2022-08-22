using AutoMapper;
using EModel.Contracts.Repository;
using EModel.Entities.Utils;
using Microsoft.Extensions.Logging;
using EModel.Entities.Entities;

namespace EModel.Core.V1
{
    public class PaymentCore
    {
        private readonly PaymentMethodCore _paymentMethodCore;
        private readonly StripeCore _stripe;

        public PaymentCore(IPaymentMethodRepository context, IMapper mapper, ILogger<PaymentMethod> logger)
        {
            _paymentMethodCore = new(context, mapper, logger);
            _stripe = new();
        }

        public async Task<ResponseService<List<PaymentMethod>>> GetAllMethods(int userId)
        {
            return await _paymentMethodCore.GetAllMethods(userId);
        }

        public async Task<string> PayBooking(int paymentMethodId, int total, string customerToken)
        {
            PaymentMethod paymentMethod = await _paymentMethodCore.GetById(paymentMethodId);
            var result = _stripe.PlacePayment(paymentMethod.Token, total, customerToken);
            return result.Id;
        }
    }
}
