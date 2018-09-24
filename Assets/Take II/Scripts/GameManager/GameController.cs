using System.Collections.Generic;
using Assets.Take_II.Scripts.EnemyManager;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.GameManager
 {
     public class GameController: MonoBehaviour {
         public static GameController Manager { get; private set; }
         public List<Player> Players = new List<Player>();
         public List<Enemy> Enemies = new List<Enemy>();
        
         void Awake()
         {
             Manager = this;
         }
     }
 }