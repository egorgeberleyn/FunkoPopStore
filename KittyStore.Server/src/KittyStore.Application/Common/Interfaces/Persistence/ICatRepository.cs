﻿using KittyStore.Domain.CatAggregate;

namespace KittyStore.Application.Common.Interfaces.Persistence
{
    public interface ICatRepository
    {
        Task<List<Cat>> GetAllCatsAsync();
        Task<Cat?> GetCatByIdAsync(Guid id);
        Task CreateCatAsync(Cat newCat);
        void UpdateCat(Cat cat);
        void DeleteCat(Cat cat);
    
    }
}