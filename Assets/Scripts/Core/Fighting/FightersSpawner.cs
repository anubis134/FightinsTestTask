using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightersSpawner : MonoBehaviour
{
    private GridManager _gridManager;
    [SerializeField]
    private Fighter[] _fighterPrefabs;
    [Header("Spawner Settings")]
    [Space]
    [SerializeField]
    [Range(1,30)]
    private int _fightersCount;

    private void Start()
    {
        _gridManager = Services.Singletone.GridManager;
        SpawnFighters();
    }

    private void SpawnFighters() 
    {
        for (int i = 0; i < _fightersCount; i++) 
        {
            Repeat:
            int randomCell = Random.Range(0, _gridManager.CellsList.Count);
            if (!_gridManager.CellsList[randomCell].IsBusy)
            {
                _gridManager.CellsList[randomCell].IsBusy = true;
                SpawnFighter(_fighterPrefabs[Random.Range(0, _fighterPrefabs.Length)], _gridManager.CellsList[randomCell].transform);
            }
            else 
            {
                goto Repeat;
            }
            
        }
    }

    private void SpawnFighter(Fighter fighter, Transform spawnTransform) 
    {
       Transform fighterTransform = Instantiate(fighter).transform;
        fighterTransform.position = spawnTransform.position;
    }
}
