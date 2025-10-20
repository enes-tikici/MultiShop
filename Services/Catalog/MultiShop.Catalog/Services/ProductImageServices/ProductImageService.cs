﻿using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<ProductImage> _productImageCollection;
        public ProductImageService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productImageCollection= database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }
        public Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            var values = _mapper.Map<ProductImage>(createProductImageDto);
            return _productImageCollection.InsertOneAsync(values);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _productImageCollection.DeleteOneAsync(x => x.ProductImageId == id);
        }

        public Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            var values = _productImageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<Task<List<ResultProductImageDto>>>(values);
        }

        public Task<GetByIdProductImageDto> GetByProductImageAsync(string id)
        {
            var values = _productImageCollection.Find<ProductImage>(x => x.ProductImageId == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<Task<GetByIdProductImageDto>>(values);
        }

        public Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            var values = _mapper.Map<ProductImage>(updateProductImageDto);
            return _productImageCollection.FindOneAndReplaceAsync
                (x => x.ProductImageId == updateProductImageDto.ProductImageId, values);
        }
    }
}
