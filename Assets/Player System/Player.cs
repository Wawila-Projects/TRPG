﻿using Assets.CharacterSystem;
using Assets.CombatSystem;
using Assets.EnemySystem;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.PlayerSystem
{
    public class Player: Character
    {
        public Equipment Equipment = new Equipment();
        public bool IsMoving;
        
        public override void Die () {
            //TODO Revive!
            transform.position = new Vector3(transform.position.x,  -6, transform.position.z);
        }

        public void Move(Tile destination, Action<bool> completion = null) {
            StartCoroutine(TakeStep());

            IEnumerator TakeStep() {
                IsMoving = true;
                while(transform.position != destination.transform.position) {
                    transform.position =  Vector3.MoveTowards (transform.position, 
                        destination.transform.position, 5 * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                
                Location.Occupant = null;
                destination.Occupant = this;
                Location = destination;

                var enemy = CheckForAllOutAttack ();
                var didAttack = false;
                if (enemy != null) {
                    enemy.IsSurrounded = true;
                    TurnFinished = true;
                    DeactivateOneMore();
                    CombatManager.Manager.AllOutAttack (enemy);
                    didAttack = true;
                }
                
                IsMoving = false;
                completion?.Invoke(didAttack);
                yield break;
            }

            Enemy CheckForAllOutAttack (bool needsSixCount = false) {
                foreach (var neighhbor in Location.Neighbors) {
                    if (neighhbor.Occupant == null) continue;
                    var enemy = neighhbor.Occupant as Enemy;
                    if (enemy == null) continue;
                    if (enemy.IsSurrounded) continue;

                    if (needsSixCount || enemy.Location.Neighbors.Count == 6 &&
                        enemy.Location.Neighbors.TrueForAll (tile => tile.Occupant is Player))
                        return enemy;
                }

                return null;
            }
        }

        public new Player ClonePlayer()
        {
            return GetComponent<Player>().gameObject.GetComponent<Player>();
        }
    }
}