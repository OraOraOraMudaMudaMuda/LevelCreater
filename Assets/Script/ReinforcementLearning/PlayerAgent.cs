using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PlayerAgent : Agent
{
    [field: SerializeField] Player player;
    [field: SerializeField] AgentUI agentUI;
    int prevScore;

    public override void Initialize()
    {
        base.Initialize();
        agentUI = UIManager.Instance.GetComponentInChildren<AgentUI>();
        LevelManager.Instance.CreateLevel();
    }

    public void Update()
    {
        agentUI.SetRewardValue(GetCumulativeReward());
        if (player.Health <= 0)
        {
            AddReward(-1f);
            GameManager.Instance.GameStop();
            EndEpisode();
        }
        else if (GameManager.Instance.Spawner.enemyAmount <= 0)
        {
            AddReward(1f);
            GameManager.Instance.GameStop();
            EndEpisode();
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        var pos = LevelManager.Instance.baseTile.map.WorldToCell(player.transform.position);
        sensor.AddObservation((float)pos.x / (LevelManager.Instance.mapGrid.x * 0.5f));
        sensor.AddObservation((float)pos.y / (LevelManager.Instance.mapGrid.y * 0.5f));     

        sensor.AddObservation((float)player.Health / (float)player.MaxHealth);
        sensor.AddObservation((float)player.Stamina / (float)player.MaxStamina);

        var spawner = GameManager.Instance.Spawner;
        sensor.AddObservation(((float)spawner.enemyAmount / (float)spawner.createdEnemyAmount));

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
        agentUI.SetStepValue(StepCount);
    }

    public override void OnEpisodeBegin()
    {
        GameManager.Instance.GameStop();
        prevScore = 0;
        agentUI.SetStepValue(0);
        SetReward(0);
        agentUI.SetRewardValue(GetCumulativeReward());

        LevelManager.Instance.CreateLevel();
        GameManager.Instance.GameStart();
    }
}
