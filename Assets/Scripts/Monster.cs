using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Common
{
    private Player player;
    private GameManager gameManager;
    
    public void InitializeMonster(Player player, GameManager gameManager)
    {
        this.player = player;
        this.gameManager = gameManager;
    }

    public void spawnMonster()
    {
        gameManager.activeCommons.Add(this);
    } 

    void move()
    {
        
    }

    void DestroyMonster()
    {
        gameManager.activeCommons.Remove(this);
    }

    class Stat
    {
        public int Damage;
    }
}