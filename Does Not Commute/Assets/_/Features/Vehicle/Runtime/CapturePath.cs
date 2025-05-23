using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Vehicle
{
    public class CapturePath : PersoBehaviour
    {
        #region Publics

        public bool CanCapture
        {
            get => _canCapture;
            set => _canCapture = value;
        }
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            //
        }

        private void Start()
        {
            if (_isReplayMode)
            {
                _canCapture = false;
                PlayCapture();
            }
            else
            {
                _canCapture = true;
                _timer = _timerDuration;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(_isReplayMode) return;
            
            if (_canCapture)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    CapturePosition();
                    _timer = _timerDuration;
                }
            }
        }

        private void FixedUpdate()
        {
            //
        }

        #endregion
        

        #region Main Methods

        public void LoadReplay(List<Vector3> positions, List<Quaternion> rotations)
        {
            if (positions.Count != rotations.Count)
            {
                Error("La longueur de posistions et de rotations n'est pas la même.");
                return;
            }
            _posList = new List<Vector3>(positions);
            _rotationList = new List<Quaternion>(rotations);
        }
    
        #endregion
            
            
        #region Utils
    
        /* Fonctions privées utiles */

        private IEnumerator ReplayPath()
        {
            for (int i = 0; i < _posList.Count - 1; i++)
            {
                Vector3 start =  _posList[i];
                Vector3 end = _posList[i + 1];
                float time = 0f;
                while (time < 1f)
                {
                    time += Time.deltaTime / _timerDuration;
                    Transform.position =  Vector3.Lerp(start, end, time);
                    Transform.rotation = _rotationList[i];
                    yield return null;
                }
            }
            gameObject.SetActive(false);
        }
        
        private void CapturePosition()
        {
            _posList.Add(Transform.position);
            _rotationList.Add(transform.rotation);
        }

        private void PlayCapture()
        {
            StartCoroutine(ReplayPath());
        }
    
        #endregion
    
    
        #region Privates and Protected
        
        private float _timer;
        private bool _canCapture = true;
        [SerializeField] private bool _isReplayMode = false;

        [SerializeField] private float _timerDuration = 3.0f;
        [SerializeField] private List<Vector3> _posList;
        [SerializeField] private List<Quaternion> _rotationList;
        
        #endregion
    }
}