using TowerDefence.Infrastructure.Services;
using UnityEngine;

namespace TowerDefence.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string _path);
        GameObject Instantiate(string _path, Vector2 _at);
    }
}