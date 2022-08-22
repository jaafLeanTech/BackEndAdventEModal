using EModel.Contracts.Generics;
using EModel.Entities.Entities;


namespace EModel.Contracts.Repository
{
    public interface IBookingDetailRepository : IGenericActionDbAddUpdate<BookingDetail>, IGenericActionDbQuery<BookingDetail>
    {
    }
}
