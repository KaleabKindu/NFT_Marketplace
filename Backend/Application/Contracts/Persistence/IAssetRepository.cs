using System;
using Application.Contracts.Persistance;
using Domain;
using Domain.Asset;

namespace Application.Contracts.Presistence
{
    public interface IAssetRepository: IRepository<Asset>
    {
        
    }
}