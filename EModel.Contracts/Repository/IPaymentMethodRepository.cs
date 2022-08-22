using EModel.Contracts.Generics;
using EModel.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModel.Contracts.Repository
{
    public interface IPaymentMethodRepository : IGenericActionDbAddUpdate<PaymentMethod>, IGenericActionDbQuery<PaymentMethod>
    {
    }
}
