using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Vehicle
{
    public class VehicleComportement : PersoBehaviour
    {
            #region Publics

            //
    
            #endregion


            #region Unity API


            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Awake()
            {
                _capturePath = GetComponent<CapturePath>();
            }

            private void Start()
            {
                //
            }

            private void Update()
            {
                if (_capturePath.CanCapture)
                {
                    if (_inputDir.sqrMagnitude < 0.1f) return;
                    
                    Vector3 targetDir = new Vector3(0, _inputDir.x, 0);
                    Transform.Rotate(targetDir * _rotationSpeed);
                }
            }
            
            // Update is called once per frame
            void FixedUpdate()
            {
                if (_capturePath.CanCapture)
                {
                    float maxSpeed = _vehicleMaxSpeed;
                    if (Rigidbody.linearVelocity.magnitude < maxSpeed)
                    {
                        Rigidbody.linearVelocity = transform.forward * (_acceleration);
                    }
                }
            }
        

            #endregion
            

            #region Main Methods

            public void OnMove(InputAction.CallbackContext context)
            {
                _inputDir = context.ReadValue<Vector2>();
            }
    
            #endregion

    
            #region Utils
    
            /* Fonctions privées utiles */
    
            #endregion
    
    
            #region Privates and Protected
            
            private bool _canSavePosition = true;
            private Vector2 _inputDir;
            private CapturePath _capturePath;
            
            [SerializeField] private float _acceleration = 50.0f;
            [SerializeField] private float _vehicleMaxSpeed = 100f;
            [SerializeField] private float _rotationSpeed = 200f;

            #endregion
    }
}
