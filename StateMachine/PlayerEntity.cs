using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.HFiniteStateMachine;
using Laserbean.CoreSystem;
using Cinemachine.Utility;
using System.Data;

using Laserbean.Weapon2D;
using Laserbean.ZombGaem.PlayerStates;
using unityInventorySystem.Attribute;

namespace Laserbean.ZombGaem.PlayerStates
{


    public class PlayerEntity : Entity
    {

        public PlayerAirState InAirState { get; private set; }

        public PlayerPoisedState PoiseStunnedState { get; private set; }

        public PlayerDeadState DeadState { get; private set; }
        public PlayerIdleGround IdleGroundState { get; private set; }
        public PlayerWalkGround WalkGroundState { get; private set; }
        public PlayerDashGround DashGroundState { get; private set; }

        public PlayerMainWeaponAbility MainWeaponState { get; private set; }

        public PlayerSecondWeaponAbility SecondWeaponState { get; private set; }


        public PlayerMovementData movementData;
        protected override void Awake()
        {
            base.Awake();

            InAirState = new PlayerAirState(this, StateMachine, movementData, "inAir");

            PoiseStunnedState = new PlayerPoisedState(this, StateMachine, movementData, "poiseStunned");

            DeadState = new PlayerDeadState(this, StateMachine, movementData, "dead");

            IdleGroundState = new PlayerIdleGround(this, StateMachine, movementData, "idle");
            WalkGroundState = new PlayerWalkGround(this, StateMachine, movementData, "idle");
            DashGroundState = new PlayerDashGround(this, StateMachine, movementData, "idle");

            MainWeaponState = new PlayerMainWeaponAbility(this, StateMachine, movementData, "shoot1");
            SecondWeaponState = new PlayerSecondWeaponAbility(this, StateMachine, movementData, "shoot2");

            StateMachine.Initialize(IdleGroundState);
        }
    }


    // Level 0
    public abstract class PlayerState : State
    {
        protected PlayerEntity playerEntity;
        protected Movement2D movement2D;
        protected PlayerInputHandler playerinput;
        protected PlayerMovementData movementData;

        protected StatusCC status;

        public PlayerState(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, animBoolName)
        {
            playerEntity = entity;
            movement2D = entity.Core.GetComponentInChildren<Movement2D>();
            playerinput = entity.Core.GetComponentInParent<PlayerInputHandler>();
            movementData = playerdata;
            status = entity.Core.GetCoreComponent<StatusCC>();
        }


        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (status.Health.Value <= status.Health.MinValue) {
                stateMachine.ChangeState(playerEntity.DeadState);
            }
        }
    }


    // Level 1

    public abstract class PlayerAliveState : PlayerState
    {
        protected bool PoiseStunned = false;
        public PlayerAliveState(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        {
            status.Poise.OnCurrentValueMin += delegate { PoiseStunned = true; };
            status.Poise.OnCurrentValueMax += delegate { PoiseStunned = false; };
        }

        private void OnDestroy()
        {
            status.Poise.OnCurrentValueMin -= delegate { PoiseStunned = true; };
            status.Poise.OnCurrentValueMax -= delegate { PoiseStunned = false; };
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (PoiseStunned) {
                stateMachine.ChangeState(playerEntity.PoiseStunnedState);
            }

            if (playerinput.playerInputData.Reload) {
                playerEntity.MainWeaponState.Reload(); 
            }


        }
        
    }

    public class PlayerDeadState : PlayerState
    {
        public PlayerDeadState(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        { }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // movement2D?.SetVelocityZero(); 
        }

    }

    // Level 1.5

    public abstract class PlayerGround : PlayerAliveState
    {
        AttributesController attributesController;

        public PlayerGround(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        {
            attributesController = entity.GetComponent<AttributesController>();

            attributesController.OnAttributeChange += OnAttributeChange;
        }

        protected float ModifyWithAgility(float base_speed)
        {
            return base_speed * (1 + AttributeCalculations.CalculateAgilityPercentage(Current_Agility_Attribute));
        }

        int Current_Agility_Attribute = 0;

        private void OnAttributeChange(unityInventorySystem.Attribute.Attribute attribute)
        {
            switch (attribute.type) {
                case AttributeType.Agility:
                    Current_Agility_Attribute = attribute.ModifiedValue;
                    break;
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (playerinput.playerInputData.Jump) {
                // playerinput.playerInputData.Jump.Use(); 
                stateMachine.ChangeState(playerEntity.InAirState);
            }
            // Debug.Log("MainAttck = " + playerinput.playerInputData.MainAttack.Pressed.Value); 

            if (playerinput.playerInputData.MainAttack.Pressed && playerEntity.MainWeaponState.CanEnter()) {
                // playerinput.playerInputData.MainAttack.Pressed.Use(); 
                stateMachine.ChangeState(playerEntity.MainWeaponState);
            }

            if (playerinput.playerInputData.MeleeAttack.Pressed && playerEntity.SecondWeaponState.CanEnter()) {
                // playerinput.playerInputData.MeleeAttack.Pressed.Use(); 
                stateMachine.ChangeState(playerEntity.SecondWeaponState);
            }
        }
    }

    public abstract class PlayerAbility : PlayerAliveState
    {
        protected Weapon weapon_1;
        protected Weapon weapon_2;

        public PlayerAbility(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        {
            weapon_1 = entity.GetComponentsInChildren<Weapon>()[0];
            weapon_2 = entity.GetComponentsInChildren<Weapon>()[1];
        }

        float starttime = 0f;

        public abstract bool CanEnter();

        public override void OnEnter()
        {
            base.OnEnter();
            starttime = Time.time;
            movement2D?.SetVelocityZero();
        }

        public override void OnExit()
        {
            base.OnExit();
            // weapon.FinishAttack(); 
            weapon_1.OnAttackInputReleased();
            weapon_2.OnAttackInputReleased();
        }


        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (Time.time - starttime < 0.1f) {
                movement2D?.SetVelocityZero();
            }
        }


        public void Reload() {
            weapon_1.ReloadWeapon();
            weapon_2.ReloadWeapon();
        }
    }

    public class PlayerMainWeaponAbility : PlayerAbility
    {
        public PlayerMainWeaponAbility(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        { }

        float starttime = 0f;

        public override void OnEnter()
        {
            base.OnEnter();
            weapon_1.EnterAttack();
            starttime = Time.time;
            Debug.Log(animBoolName);
        }

        public override void OnExit()
        {
            base.OnExit();
            // weapon.FinishAttack(); 
        }


        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (playerinput.playerInputData.MainAttack.Released) {
                // playerinput.playerInputData.MainAttack.Released.Use();
                weapon_1.OnAttackInputReleased();
            }

            if (!weapon_1.IsAttacking) {
                stateMachine.ChangeState(playerEntity.IdleGroundState);
            }
        }

        public override bool CanEnter()
        {
            return weapon_1.CanEnterAttack;
        }
    }

    public class PlayerSecondWeaponAbility : PlayerAbility
    {

        public PlayerSecondWeaponAbility(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        { }

        float starttime = 0f;

        public override void OnEnter()
        {
            base.OnEnter();
            weapon_2.EnterAttack();
            starttime = Time.time;
        }

        public override void OnExit()
        {
            base.OnExit();
            // weapon.FinishAttack(); 
        }


        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (playerinput.playerInputData.MeleeAttack.Released) {
                // playerinput.playerInputData.MeleeAttack.Released.Use(); 
                weapon_2.OnAttackInputReleased();
            }

            if (!weapon_2.IsAttacking) {
                stateMachine.ChangeState(playerEntity.IdleGroundState);
            }
        }


        public override bool CanEnter()
        {
            return weapon_2.CanEnterAttack;
        }
    }

    public class PlayerAirState : PlayerState
    {

        public PlayerAirState(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        { }

        float start_time = 0f;
        public override void OnEnter()
        {
            base.OnEnter();
            start_time = Time.time;
            Debug.Log("Air state");
        }


        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (Time.time - startTime > movementData.JumpTime) {
                stateMachine.ChangeState(playerEntity.IdleGroundState);
            }
        }
    }


    public class PlayerPoisedState : PlayerAliveState
    {
        public PlayerPoisedState(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        { }

        public override void OnEnter()
        {
            base.OnEnter();
            movement2D?.SetVelocityZero();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (!PoiseStunned) {
                stateMachine.ChangeState(playerEntity.IdleGroundState);
            }

            movement2D.SetVelocity(playerinput.playerInputData.MoveDirection * (movementData.MoveSpeed/2));


        }
    }


    // Level 2

    public class PlayerIdleGround : PlayerGround
    {
        public PlayerIdleGround(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        { }

        public override void OnEnter()
        {
            base.OnEnter();
            movement2D?.SetVelocityZero();
            // Debug.Log("idle");

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (playerinput.playerInputData.MoveDirection.sqrMagnitude > 0) {
                movement2D.SetVelocity(playerinput.playerInputData.MoveDirection * ModifyWithAgility(movementData.MoveSpeed));
                stateMachine.ChangeState(playerEntity.WalkGroundState);
            }
        }
    }

    public class PlayerWalkGround : PlayerGround
    {
        public PlayerWalkGround(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        { }

        public override void OnEnter()
        {
            base.OnEnter();
            // Debug.Log("walk");
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (playerinput.playerInputData.MoveDirection.sqrMagnitude == 0) {
                stateMachine.ChangeState(playerEntity.IdleGroundState);
                return;
            }
            if (playerinput.playerInputData.Dash && playerEntity.DashGroundState.CanDash()) {
                // playerinput.playerInputData.Dash.Use(); 
                Debug.Log("Befoer change to dash");
                stateMachine.ChangeState(playerEntity.DashGroundState);
            }
            movement2D.SetVelocity(playerinput.playerInputData.MoveDirection * ModifyWithAgility(movementData.MoveSpeed));
        }
    }

    public class PlayerDashGround : PlayerGround
    {
        StatusCC statuscontroller;
        public PlayerDashGround(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
        {
            statuscontroller = entity.Core.GetCoreComponent<StatusCC>();
        }

        float start_time = 0f;
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Dash");

            start_time = Time.time;
            statuscontroller.Stamina.Decrease(movementData.DashStamina);
        }


        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            movement2D.SetVelocity(movement2D.LastDirection * movementData.DashSpeed);
            if (Time.time > start_time + movementData.DashTime) {
                stateMachine.ChangeState(playerEntity.IdleGroundState);
                return;
            }
        }

        public bool CanDash()
        {
            return Time.time > start_time + movementData.DashCooldown + movementData.DashTime && statuscontroller.Stamina.Value >= movementData.DashStamina;
        }
    }




}







