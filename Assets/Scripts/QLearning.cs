using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = System.Random;

public class QLearning
{
    private double _alpha;
    private double _gamma;

    private int _mazeWidth;
    private int _mazeHeight;
    private int _statesCount;

    private int _reward;
    private int _penalty;

    private char[][] _maze;
    private int[][] _rewards;
    private double[][] _qValues;

    public QLearning(int reward = 100, int penalty = -10, double alpha = 0.1, double gamma = 0.9)
    {
        _reward = reward;
        _penalty = penalty;
        _alpha = alpha;
        _gamma = gamma;

        _maze = new[]
        {
            new[] {'0', '0', '0', '0', '0', '0'}, // each increments damage
            new[] {'X', 'X', 'X', 'X', 'X', '0'},
            new[] {'0', '0', '0', '0', '0', 'F'},

            //    start
            //      0    1    2    3    4    5
            //      6    7    8    9   10   11
            //     12   13   14   15   16   17 end
        };

        _mazeWidth = _maze[0].Length;
        _mazeHeight = _maze.Length;

        _statesCount = _mazeHeight * _mazeWidth;

        _rewards = new int[_statesCount][];
        InitializeArray(_rewards, _statesCount);

        _qValues = new double[_statesCount][];
        InitializeArray(_qValues, _statesCount);

        InitializeQTable();
    }

    public class AttackMechanics
    {
        public float damage = 2;
        public int cooldown = 2000;

        private QLearning qLearning = new QLearning();
        private Dictionary<int, int> attackSteps = new Dictionary<int, int>();

        private Transform _thisObject;

        public AttackMechanics(Transform thisObject)
        {
            _thisObject = thisObject;
        }

        public void Attack(Transform otherObject, float objectHealth, float attackDistance, Func<float, HealthChange> performAttack)
        {
            attackSteps = qLearning.GetPolicy();
            UpdateMetrics();

            if (Vector3.Distance(_thisObject.position, otherObject.position) <= attackDistance)
            {
                HealthChange healthChange = performAttack(damage);
                if (healthChange.noChange()) return;

                var chars = new[]
                {
                    new[] {'0', '0', '0', '0', '0', '0'},
                    GetHealthMazeLayer(6, objectHealth),
                    new[] {'F', '0', '0', '0', '0', '0'},
                };
                string str = "";
                foreach (var c in chars)
                {
                    foreach (var c1 in c)
                    {
                        str += c1 + " ";
                    }

                    str += " | ";
                }
                // Debug.Log(str);
                qLearning.SetMaze(chars);
            }
        }

        private void UpdateMetrics()
        {
            // foreach (var keyValuePair in attackSteps)
            // {
            //     Debug.Log($"{keyValuePair.Value} -> {keyValuePair.Value}");
            //     Debug.Log("---------------------");
            // }

            foreach (var keyValuePair in attackSteps)
            {
                bool reached = false;
                switch (keyValuePair.Value)
                {
                    case 15:
                        damage = 0.75f;
                        reached = true;
                        cooldown = 2000;
                        break;
                    case 14:
                        damage = 1.25f;
                        reached = true;
                        cooldown = 1650;
                        break;
                    case 13:
                        damage = 1.75f;
                        reached = true;
                        cooldown = 1500;
                        break;
                    case 12:
                        damage = 2.25f;
                        reached = true;
                        cooldown = 1650;
                        break;
                    case 11:
                        damage = 3;
                        reached = true;
                        cooldown = 1900;
                        break;
                }

                if (reached) break;
            }
        }

        private char[] GetHealthMazeLayer(int size, float health)
        {
            char[] layer = new char[size];
            int steps = Convert.ToInt32(Math.Round((health / FightHealth.MAX_HEALTH) * 6));

            for (int i = 0; i < size; i++)
            {
                layer[i] = i < steps - 1 ? 'X' : '0';
            }

            return layer;
        }
        
        public class HealthChange
        {
            public float Before;
            public float After;

            public HealthChange(float before, float after)
            {
                Before = before;
                After = after;
            }

            public bool noChange()
            {
                return Math.Abs(Before - After) < 0.1;
            }
        }
    }

    public void SetMaze(char[][] maze)
    {
        _maze = maze;
        InitializeQTable();
    }

    private void InitializeArray<T>(T[][] array, int size)
    {
        for (var i = 0; i < array.Length; i++)
        {
            array[i] = new T[size];
        }
    }

    private void InitializeQTable()
    {
        for (int k = 0; k < _statesCount; k++)
        {
            int i = k / _mazeWidth;
            int j = k - i * _mazeWidth;

            for (int s = 0; s < _statesCount; s++)
            {
                _rewards[k][s] = -1;
            }

            if (_maze[i][j] != 'F')
            {
                int goLeft = j - 1;
                if (goLeft >= 0)
                {
                    int target = i * _mazeWidth + goLeft;
                    if (_maze[i][goLeft] == '0')
                    {
                        _rewards[k][target] = 0;
                    }
                    else if (_maze[i][goLeft] == 'F')
                    {
                        _rewards[k][target] = _reward;
                    }
                    else
                    {
                        _rewards[k][target] = _penalty;
                    }
                }

                int goRight = j + 1;
                if (goRight < _mazeWidth)
                {
                    int target = i * _mazeWidth + goRight;
                    if (_maze[i][goRight] == '0')
                    {
                        _rewards[k][target] = 0;
                    }
                    else if (_maze[i][goRight] == 'F')
                    {
                        _rewards[k][target] = _reward;
                    }
                    else
                    {
                        _rewards[k][target] = _penalty;
                    }
                }

                int goUp = i - 1;
                if (goUp >= 0)
                {
                    int target = goUp * _mazeWidth + j;
                    if (_maze[goUp][j] == '0')
                    {
                        _rewards[k][target] = 0;
                    }
                    else if (_maze[goUp][j] == 'F')
                    {
                        _rewards[k][target] = _reward;
                    }
                    else
                    {
                        _rewards[k][target] = _penalty;
                    }
                }

                int goDown = i + 1;
                if (goDown < _mazeHeight)
                {
                    int target = goDown * _mazeWidth + j;
                    if (_maze[goDown][j] == '0')
                    {
                        _rewards[k][target] = 0;
                    }
                    else if (_maze[goDown][j] == 'F')
                    {
                        _rewards[k][target] = _reward;
                    }
                    else
                    {
                        _rewards[k][target] = _penalty;
                    }
                }
            }
        }

        for (int i = 0; i < _statesCount; i++)
        {
            for (int j = 0; j < _statesCount; j++)
            {
                _qValues[i][j] = _rewards[i][j];
            }
        }
    }

    private void CalculateQValue()
    {
        Random rand = new Random();

        for (int i = 0; i < 1000; i++)
        {
            int crtState = rand.Next(_statesCount);

            while (!IsFinalState(crtState))
            {
                int[] actionsFromCurrentState = PossibleActionsFromState(crtState);

                int index = rand.Next(actionsFromCurrentState.Length);
                int nextState = actionsFromCurrentState[index];

                double q = _qValues[crtState][nextState];
                double maxQ = MaxQ(nextState);
                int r = _rewards[crtState][nextState];

                double value = q + _alpha * (r + _gamma * maxQ - q);
                _qValues[crtState][nextState] = value;

                crtState = nextState;
            }
        }
    }

    private bool IsFinalState(int state)
    {
        int i = state / _mazeWidth;
        int j = state - i * _mazeWidth;

        return _maze[i][j] == 'F';
    }

    private int[] PossibleActionsFromState(int state)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < _statesCount; i++)
        {
            if (_rewards[state][i] != -1)
            {
                result.Add(i);
            }
        }

        return result.ToArray();
    }

    private double MaxQ(int nextState)
    {
        int[] actionsFromState = PossibleActionsFromState(nextState);
        double maxValue = -10;

        foreach (int nextAction in actionsFromState)
        {
            double value = _qValues[nextState][nextAction];

            if (value > maxValue)
                maxValue = value;
        }

        return maxValue;
    }

    private int GetPolicyFromState(int state)
    {
        int[] actionsFromState = PossibleActionsFromState(state);

        double maxValue = Double.MinValue;
        int policyGotoState = state;

        foreach (int nextState in actionsFromState)
        {
            double value = _qValues[state][nextState];

            if (value > maxValue)
            {
                maxValue = value;
                policyGotoState = nextState;
            }
        }

        return policyGotoState;
    }

    public Dictionary<int, int> GetPolicy()
    {
        CalculateQValue();
        Dictionary<int, int> policies = new Dictionary<int, int>();

        int i = 0;
        while (!IsFinalState(i))
        {
            var policy = GetPolicyFromState(i);
            policies[i] = policy;
            i = policy;
        }

        return policies;
    }
}