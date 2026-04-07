using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.FeatureDtos;

namespace MultiShop.Catalog.Services.FeatureServices
{
    public interface IFeatureService
    {
        Task<List<ResultFeatureDto>> GetAllFeatureAsync();
        Task CreateFeaturAsync(CreateFeatureDto createCategoryDto);
        Task UpdateFeaturAsync(UpdateFeatureDto updateCategoryDto);
        Task DeleteFeatureAsync(string id);
        Task<GetByIdFeatureDto> GetByFeaturAsync(string id);
    }
}
