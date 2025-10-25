using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Entities;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;
        public DiscountService(DapperContext context)
        {
            _context= context;
        }
        public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            string query = "insert into Coupons(Code,Rate,IsActive,ValidDate) values (@code,@rate,@isActive,@validDate)";

            var paramaters = new DynamicParameters();
            paramaters.Add("@code", createCouponDto.Code);
            paramaters.Add("@rate", createCouponDto.Rate);
            paramaters.Add("@isActive", createCouponDto.IsActive);
            paramaters.Add("@validDate", createCouponDto.ValidDate);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }

        }

        public async Task DeleteCouponAsync(int id)
        {
            string query = "delete from Coupons where CouponId=@couponId";
            var paramaters = new DynamicParameters();
            paramaters.Add("couponId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }

        public async Task<List<ResultCouponDto>> GetAllCouponAsync()
        {
            string query = "select * from Coupons";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdCouponDto> GetByIdCouponAsync(int id)
        {
            string query = "select * from Coupons where CouponId=@couponId";
            var paramaters = new DynamicParameters();
            paramaters.Add("@couponId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdCouponDto>(query,paramaters);
                return values;
            }
        }

        public async Task UpdateCouponAsync(UpdateCouponDto updateCouponDto)
        {
            string query = "update Coupons set Code=@code, Rate=@rate, IsActive=@isActive, ValidDate=@validDate where CouponId=@couponId";

            var paramaters = new DynamicParameters();
            paramaters.Add("@couponId", updateCouponDto.CouponId);
            paramaters.Add("@code", updateCouponDto.Code);
            paramaters.Add("@rate", updateCouponDto.Rate);
            paramaters.Add("@isActive", updateCouponDto.IsActive);
            paramaters.Add("@validDate", updateCouponDto.ValidDate);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }
    }
}
