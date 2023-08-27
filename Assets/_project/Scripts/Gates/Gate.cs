using System.Collections;
using UnityEngine;

namespace _project.Scripts.Gates
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private Wall _topWall;
        [SerializeField] private Wall _botWall;
        
        private Transform _selfTransform;
        private Transform _startParentTransform;
        private float _lifetime;

        private void Awake()
        {
            _selfTransform = transform;
            _startParentTransform = _selfTransform.parent;
        }

        public void Set(Vector3 position, float wallsDistance, float lifetime)
        {
            gameObject.SetActive(true);
            
            _selfTransform.parent = null;
            _selfTransform.position = position;
            _lifetime = lifetime;
            
            SetDistanceBetweenWalls(wallsDistance);
            StartCoroutine(LifetimeDelay());
        }

        private IEnumerator LifetimeDelay()
        {
            yield return new WaitForSeconds(_lifetime);
            
            transform.parent = _startParentTransform;
            gameObject.SetActive(false);
        }

        private void SetDistanceBetweenWalls(float wallsDistance)
        {
            var topWallPosition = _topWall.transform.localPosition;
            topWallPosition.y = wallsDistance;
            _topWall.transform.localPosition = topWallPosition;
            
            var botWallPosition = _botWall.transform.localPosition;
            botWallPosition.y = -wallsDistance;
            _botWall.transform.localPosition = botWallPosition;
        }
    }
}