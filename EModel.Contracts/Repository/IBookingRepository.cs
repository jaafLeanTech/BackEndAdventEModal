using EModel.Contracts.Generics;
using EModel.Entities.Entities;


namespace EModel.Contracts.Repository
{
    public interface IBookingRepository : IGenericActionDbAddUpdate<Booking>, IGenericActionDbQuery<Booking>
    {
    }
}
