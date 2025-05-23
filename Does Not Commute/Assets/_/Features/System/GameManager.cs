using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Vehicle;

namespace GameSystem
{
    public class GameManager : MonoBehaviour
    {
            #region Publics

            //
    
            #endregion


            #region Unity API


            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Awake()
            {
                //
            }

            private void Start()
            {
                Vector3 startPosition = new Vector3(12.44f,0.12f,2.56f);
                Quaternion startRotation = Quaternion.identity;
                SpawnPlayer(startPosition, startRotation);
            }

            // Update is called once per frame
            void Update()
            {
                //
            }

            private void FixedUpdate()
            {
                //
            }

            #endregion
    


            #region Main Methods

            private void SpawnPlayer(Vector3 startPosition, Quaternion startRotation)
            {
                if (_currentPlayer != null)
                {
                    Destroy(_currentPlayer);
                }
                
                _currentPlayer = Instantiate(_playerPrefab, startPosition, startRotation);
            }
    
            #endregion
            
            
            #region Utils
    
            /* Fonctions privées utiles */
            private void SpawnGhosts(List<List<Vector3>> allPositions, List<List<Quaternion>> allRotations)
            {
                for (int i = 0; i < allPositions.Count; i++)
                {
                    if(i >= _ghostsPrefabs.Count) continue;
                    
                    GameObject prefab = _ghostsPrefabs[i];
                    Vector3 startPos = allPositions[i][0];
                    Quaternion startRot = allRotations[i][0];
                    
                    GameObject ghost = Instantiate(prefab, startPos, startRot);
                    
                    var capturePath = ghost.GetComponent<CapturePath>();
                    if (capturePath != null)
                    {
                        capturePath.LoadReplay(allPositions[i],  allRotations[i]);
                    }
                    _activeGhosts.Add(ghost);
                }
            }
    
            #endregion
    
    
            #region Privates and Protected

            [SerializeField] private GameObject _playerPrefab;
            
            [SerializeField] private List<GameObject> _ghostsPrefabs = new List<GameObject>();

            private GameObject _currentPlayer;
            private List<GameObject> _activeGhosts = new List<GameObject>();

            #endregion
    }
}
