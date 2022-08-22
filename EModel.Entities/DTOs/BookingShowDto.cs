using EModel.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModel.Entities.DTOs
{
    public class BookingShowDto : Booking
    {
        public BookingDetail[] Details { get; set; }
    }
}
