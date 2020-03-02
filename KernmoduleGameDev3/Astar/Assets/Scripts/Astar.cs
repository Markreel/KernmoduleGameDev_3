using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Astar
{
    /// <summary>
    /// TODO: Implement this function so that it returns a list of Vector2Int positions which describes a path
    /// Note that you will probably need to add some helper functions
    /// from the startPos to the endPos
    /// </summary>
    /// <param name="_startPos"></param>
    /// <param name="_endPos"></param>
    /// <param name="_grid"></param>
    /// <returns></returns>
    public List<Vector2Int> FindPathToTarget(Vector2Int _startPos, Vector2Int _endPos, Cell[,] _grid)
    {
        if(_endPos.x < 0 || _endPos.x > _grid.GetLength(0) || _endPos.y < 0 || _endPos.y > _grid.GetLength(1))
        {
            return null;
        }

        List<Node> _openList = new List<Node>();
        List<Node> _closedList = new List<Node>();

        Node _startNode = new Node(_startPos, null, 0, 0);
        _openList.Add(_startNode);

        Node _currentNode = _startNode;

        while (_openList.Count != 0)
        {
            _currentNode = _openList[0];

            foreach (var _openNode in _openList)
            {
                if (_openNode.FScore < _currentNode.FScore) { _currentNode = _openNode; }
            }

            _openList.Remove(_currentNode);
            _closedList.Add(_currentNode);

            //Check if goal is found
            if (_currentNode.position == _endPos) { break; }

            //Check neighbours
            List<Cell> _neighbours = GetValidNeighbours(_grid[_currentNode.position.x, _currentNode.position.y], _grid);
            foreach (var _neighbour in _neighbours)
            {
                if (_closedList.Any(n => n.position == _neighbour.gridPosition)) { continue; }

                int _h = ManhattanDistance(_neighbour.gridPosition, _endPos);
                int _g = _currentNode.GScore + 1;

                Node _newNode = new Node(_neighbour.gridPosition, _currentNode, _g, _h);

                if (_openList.Any(n => n.position == _neighbour.gridPosition && _newNode.GScore > n.GScore) ) { continue; }

                _openList.Add(_newNode);
            }
        }

        return BacktrackNode(_currentNode);
    }

    private List<Vector2Int> BacktrackNode(Node _node)
    {
        List<Vector2Int> _returnValue = new List<Vector2Int>();
        Node _currentNode = _node;

        while (_currentNode.parent != null)
        {
            _returnValue.Add(_currentNode.position);
            _currentNode = _currentNode.parent;
        }

        _returnValue.Reverse();
        return _returnValue;
    }

    private List<Cell> GetValidNeighbours(Cell _cell, Cell[,] _grid)
    {
        List<Cell> result = new List<Cell>();

        int _xStart, _xEnd, _yStart, _yEnd;
        _xStart = _xEnd = _yStart = _yEnd = 0;

        if (!_cell.HasWall(Wall.LEFT)) { _xStart = -1; }
        if (!_cell.HasWall(Wall.RIGHT)) { _xEnd = 1; }
        if (!_cell.HasWall(Wall.DOWN)) { _yStart = -1; }
        if (!_cell.HasWall(Wall.UP)) { _yEnd = 1; }

        //int _xStart = _cell.HasWall(Wall.LEFT) ? 0 : -1;
        //int _xEnd = _cell.HasWall(Wall.RIGHT) ? 0 : 1;
        //int _yStart = _cell.HasWall(Wall.DOWN) ? 0 : -1;
        //int _yEnd = _cell.HasWall(Wall.UP) ? 0 : 1;

        for (int x = _xStart; x <= _xEnd; x++)
        {
            for (int y = _yStart; y <= _yEnd; y++)
            {
                int cellX = _cell.gridPosition.x + x;
                int cellY = _cell.gridPosition.y + y;
                if (cellX < 0 || cellX >= _grid.GetLength(0) || cellY < 0 || cellY >= _grid.GetLength(1) || Mathf.Abs(x) == Mathf.Abs(y))
                {
                    continue;
                }
                Cell canditateCell = _grid[cellX, cellY];

                result.Add(canditateCell);
            }
        }

        return result;
    }

    private int ManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    /// <summary>
    /// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
    /// </summary>
    public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node

        public int FScore
        { //GScore + HScore
            get { return GScore + HScore; }
        }
        public int GScore; //Current Travelled Distance
        public int HScore; //Distance estimated based on Heuristic

        public Node() { }
        public Node(Vector2Int position, Node parent, int GScore, int HScore)
        {
            this.position = position;
            this.parent = parent;
            this.GScore = GScore;
            this.HScore = HScore;
        }
    }
}
