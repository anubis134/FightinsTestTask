using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightingManager : MonoBehaviour
{
    [SerializeField]
    private List<Fighter> _allFighters = new List<Fighter>();
    [SerializeField]
    private List<Fighter> _sortedFighters = new List<Fighter>();
    internal static Action OnFightersWasSortedByDistance;

    private void Start()
    {
        Invoke(nameof(SortFightersByDistance), 1f);
    }

    /// <summary>
    /// Add a fighter class in AllFighters List
    /// </summary>
    /// <param name="fighter"></param>
    internal void AddFighterInList(Fighter fighter) 
    {
        _allFighters.Add(fighter);
    }

    /// <summary>
    /// Return sorted list of fighters by distance
    /// </summary>
    /// <returns></returns>
    internal List<Fighter> GetSortedFightersList() 
    {
        return _sortedFighters;
    }

    private void SortFightersByDistance() 
    {
        var sortedFighters = _allFighters
            .OrderBy(distanceX => distanceX.transform.position.x)
            .ThenBy(distanceZ => distanceZ.transform.position.z);
        _sortedFighters = sortedFighters.ToList();
        OnFightersWasSortedByDistance?.Invoke();
    }
}
