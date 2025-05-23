using UnityEngine;

namespace Utils
{
    public class PersoBehaviour : MonoBehaviour
    {
        #region Publics
    
        [Header("Debug")]
        [SerializeField] protected bool m_isVerbose;
    
        #endregion
        
        #region DEBUG
        
        protected void Info(string message)
        {
            if (!m_isVerbose) return;
            Debug.Log(message, this);
        }

        protected void Error(string message)
        {
            if (!m_isVerbose) return;
            Debug.LogError(message, this);
        }

        protected void Warning(string message)
        {
            if (!m_isVerbose) return;
            Debug.LogWarning(message, this);
        }
        
        #endregion
        
        #region GETTERS
        
        private GameObject _gameObject;
        private Rigidbody _rigidbody;
        private Transform _transform;

        public GameObject GameObject => _gameObject ? _gameObject : _gameObject = gameObject;
        public Rigidbody Rigidbody => _rigidbody ? _rigidbody : _rigidbody = GetComponent<Rigidbody>();
        public Transform Transform => _transform ? _transform : _transform = GetComponent<Transform>();
        
        #endregion
        
        #region Game Loop

        //looping game

        #endregion
    }
}
