using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _project.Scripts.Utilities
{
    public class ItemsPool<T> where T : Object
    {
        private T _itemPrefab;
        private Transform _parentTransform;
        private List<T> _items;

        public ItemsPool(T itemPrefab, int itemsCount, Transform parentTransform)
        {
            _itemPrefab = itemPrefab;
            _parentTransform = parentTransform;
            _items = new List<T>();
            
            for (int i = 0; i < itemsCount; i++)
            {
                CreateItem();
            }
        }

        public T GetItem()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].GameObject().activeSelf == false)
                {
                    return _items[i];
                }
            }

            return CreateItem();
        }

        private T CreateItem()
        {
            var item = Object.Instantiate(_itemPrefab, _parentTransform);
            _items.Add(item);
            item.GameObject().SetActive(false);

            return item;
        }
    }
}