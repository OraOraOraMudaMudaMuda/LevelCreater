using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PlayerAgent : Agent
{
    [field: SerializeField] Player player;
    int prevScore;
    MLAgentUI mlagentUI;

    public override void Initialize()
    {
        base.Initialize();
        mlagentUI = UIManager.Instance.transform.Find("MLAgent").GetComponent<MLAgentUI>();
        LevelCreater.Instance.CreateLevel();
    }

    public void Update()
    {
        mlagentUI.SetRewardValue(GetCumulativeReward());
        if (player.Health <= 0)
        {
            AddReward(-1f);
            GameManager.Instance.GameStop();
            EndEpisode();
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        var pos = LevelCreater.Instance.tileMap[0].map.WorldToCell(player.transform.position);
        sensor.AddObservation((float)pos.x / (LevelCreater.Instance.grid.x * 0.5f));
        sensor.AddObservation((float)pos.y / (LevelCreater.Instance.grid.y * 0.5f));     

        sensor.AddObservation((float)player.Health / (float)player.MaxHealth);
        sensor.AddObservation((float)player.Stamina / (float)player.MaxStamina);

        bool isCanAttack = player.attackEnemyList.Count > 0 ? true : false;
        sensor.AddObservation(isCanAttack);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        AgentAction(actions.DiscreteActions);
    }
    public void AgentAction(ActionSegment<int> _act)
    {
        var action = _act[0];
        switch (action)
        {
            case 0:
                player.Movement(Vector2.right);
                break;
            case 1:
                player.Movement(Vector2.left);
                break;
            case 2:
                player.Movement(Vector2.up);
                break;
            case 3:
                player.Movement(Vector2.down);
                break;
            case 4:
                player.Attack();
                if(player.state == Character.State.ATTACK)
                    AddReward(player.attackEnemyList.Count * 0.01f);
                break;
        }

        if (prevScore != player.score)
        {
            AddReward((player.score - prevScore) / 10f);
            prevScore = player.score;
        }
        mlagentUI.SetStepValue(StepCount);
    }

    public override void OnEpisodeBegin()
    {
        GameManager.Instance.GameStop();
        prevScore = 0;
        mlagentUI.SetStepValue(0);
        SetReward(0);
        mlagentUI.SetRewardValue(GetCumulativeReward());

        LevelCreater.Instance.grid = new Vector2Int(Random.Range(50, 125), Random.Range(50, 125));
        LevelCreater.Instance.obstarclePercentage = Random.Range(0f, 0.05f);
        LevelCreater.Instance.CreateLevel();

        GameManager.Instance.GameStart();
    }
}
