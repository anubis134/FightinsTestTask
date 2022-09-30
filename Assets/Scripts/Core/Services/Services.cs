using UnityEngine;
using Utils;

public sealed class Services : MonoBehaviour
{
    internal static Services Singletone;
    internal GridManager GridManager;
    internal FightingManager FightingManager;
    private void Awake()
    {
        if (Singletone is null) Singletone = this;
        GridManager = this.FindOrException<GridManager>();
        FightingManager = this.FindOrException<FightingManager>();
    }
}
