using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            //
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _canCapture = false;
                PlayCapture();
            }
            
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

        // 
    
        #endregion
            
            
        #region Utils
    
        /* Fonctions privées utiles */

        private IEnumerator ReplayPath()
        {
            for (int i = 0; i < posList.Count - 1; i++)
            {
                Vector3 start =  posList[i];
                Vector3 end = posList[i + 1];
                float time = 0f;
                while (time < 1f)
                {
                    time += Time.deltaTime / _timerDuration;
                    Transform.position =  Vector3.Lerp(start, end, time);
                    Transform.rotation = rotationList[i];
                    yield return null;
                }
            }
        }
        
        private void CapturePosition()
        {
            posList.Add(Transform.position);
            rotationList.Add(transform.rotation);
        }

        private void PlayCapture()
        {
            StartCoroutine(ReplayPath());
        }
    
        #endregion
    
    
        #region Privates and Protected
        
        private float _timer;
        private bool _canCapture = true;

        [SerializeField] private float _timerDuration = 3.0f;
        [SerializeField] private List<Vector3> posList;
        [SerializeField] private List<Quaternion> rotationList;
        
        #endregion
    }
}