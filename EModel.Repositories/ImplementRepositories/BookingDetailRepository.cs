using EModel.Contracts.Repository;
using EModel.DataAccess.Context;
using EModel.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EModel.Repositories.ImplementRepositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly MySqlContext _context;

        public BookingDetailRepository()
        {
            _context = new();
        }

        public async Task<Tuple<BookingDetail, bool>> AddAsync(BookingDetail entity)
        {
            try
            {
                var result = _context.BookingDetails.Add(entity);
                await _context.SaveChangesAsync();
                return Tuple.Create(result.Entity, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<BookingDetail>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookingDetail>> GetByFilterAsync(Expression<Func<BookingDetail, bool>> expressionFilter = null)
        {
            try
            {
                return await _context.BookingDetails.Where<BookingDetail>(expressionFilter).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<BookingDetail> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<BookingDetail, bool>> UpdateAsync(BookingDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
