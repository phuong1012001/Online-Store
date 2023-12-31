﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.DataAccess.DbContexts;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts();

        Task<ProductDto> GetProduct(int id);

        Task<List<ProductDto>> SearchProduct(string keyword);

        Task<ProductResultDto> AddCart(ProductInCartDto productInCartDto);
    }

    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly OnlineStoreDbContext _context;

        public ProductService(IMapper mapper, OnlineStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = new List<ProductDto>();

            var productDb = _context.Products
                .Where(i => i.isDeleted == false)
                .ToList();

            products = _mapper.Map<List<ProductDto>>(productDb);

            return products;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var result = new ProductDto();

            var product = await _context.Products
                .Include (c => c.Category)
                .FirstOrDefaultAsync(x => x.Id == id && x.isDeleted == false);

            if(product == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundProduct;
                return result;
            }

            var stock = await _context.Stocks
                .FirstOrDefaultAsync(x => x.ProductId == id);

            result = _mapper.Map<ProductDto>(product);
            result.Quantity = stock.Quantity;

            return result;
        }

        public async Task<List<ProductDto>> SearchProduct(string keyword)
        {
            var products = new List<ProductDto>();

            var productDb = _context.Products
                .Include(c => c.Category)
                .Where(i => i.isDeleted == false && i.Category.Name!.Contains(keyword));

            products = _mapper.Map<List<ProductDto>>(productDb);

            return products;
        }

        public async Task<ProductResultDto> AddCart(ProductInCartDto productInCartDto)
        {
            var result = new ProductResultDto();

            var userDb = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id == productInCartDto.UserId);

            if (userDb == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundUser;
                return result;
            }

            var productDb = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == productInCartDto.ProductId);

            if (productDb == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundProduct;
                return result;
            }

            var stockDb = await _context.Stocks
                    .FirstOrDefaultAsync(x => x.Id == productInCartDto.ProductId);

            if (stockDb == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundStock;
                return result;
            }

            if (productInCartDto.Quantity <= 0 || productInCartDto.Quantity > stockDb.Quantity)
            {
                result.ErrorMessage = ErrorCodes.InvalidQuantity;
                return result;
            }

            var cartDb = await _context.Carts
                .FirstOrDefaultAsync(x => x.CustomerId == productInCartDto.UserId
                    && x.isDeleted == false);

            if (cartDb == null)
            {
                cartDb = new Cart
                {
                    CustomerId = productInCartDto.UserId
                };

                _context.Carts.Add(cartDb);
            } else {
                var cartDetailDb = await _context.CartDetails
                    .FirstOrDefaultAsync(x => x.CartId == cartDb.Id
                        && x.ProductId == productDb.Id);

                if (cartDetailDb != null)
                {
                    cartDetailDb.Quantity = productInCartDto.Quantity;
                    cartDetailDb.Total = productInCartDto.Quantity * productDb.UnitPrice;
                }

                _context.CartDetails.Update(cartDetailDb);
                await _context.SaveChangesAsync();
            }

            var cartDetail = new CartDetail
            {
                Cart = cartDb,
                ProductId = productInCartDto.ProductId,
                Quantity = productInCartDto.Quantity,
                Total = productInCartDto.Quantity * productDb.UnitPrice
            };

            _context.CartDetails.Add(cartDetail);
            await _context.SaveChangesAsync();

            result.Success = true;

            return result;
        }
    }
}
