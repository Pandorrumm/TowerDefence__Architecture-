﻿using UnityEngine;

namespace TowerDefence.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string _path)
        {
            var prefab = Resources.Load<GameObject>(_path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string _path, Vector2 _at)
        {
            var prefab = Resources.Load<GameObject>(_path);
            return Object.Instantiate(prefab, _at, Quaternion.identity);
        }
    }
}
