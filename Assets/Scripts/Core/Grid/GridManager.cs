using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Cell _cellPrefab;
    internal Grid Grid;
    internal List<Cell> CellsList = new List<Cell>();
    [Header("Grid Settings")]
    [Space]
    [SerializeField]
    [Range(1, 30)]
    private int _gridWidth = 1;
    [SerializeField]
    [Range(1, 30)]
    private int _gridHeight = 1;
    [SerializeField]
    [Range(0f, 5f)]
    private float _cellOffset = 0f;



    private void Awake()
    {
        Grid = new Grid(_gridWidth, _gridHeight);
        CreateCells(Grid);
    }

    private void CreateCells(Grid grid) 
    {
        for (int i = 0; i < Grid.Width; i++)
        {
            for (int j = 0; j < Grid.Height; j++)
            {
                Cell cell = Instantiate(_cellPrefab);
                cell.transform.parent = this.transform;
                cell.transform.position = new Vector3(i, 0F,j) * _cellOffset;
                CellsList.Add(cell);
            }
        }
    }

    #region Debugging
    private void OnDrawGizmos()
    {
        Grid = new Grid(_gridWidth, _gridHeight);
        for (int i = 0; i < Grid.Width; i++) 
        {
            for (int j = 0; j < Grid.Height; j++) 
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(new Vector3(i,0F,j) * _cellOffset, Vector3.one * 0.1F);
            }
        }
    }
    #endregion
}
