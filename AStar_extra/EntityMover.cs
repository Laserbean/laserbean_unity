using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Laserbean.General;
using Laserbean.General.GlobalTicks;

#if PATHFINDING

using Pathfinding; 


public class EntityMover : MonoBehaviour 
{    
    AIDestinationSetter destinationSetter; 
    IAstarAI aipath; 

    Rigidbody2D rigidbody2d; 

    bool currentlyCanMove = true;

    [SerializeField] 
    EntityState state = EntityState.Wander; 

    [Header("Roaming")]
    [SerializeField] bool stays_near_location = false;
    [SerializeField] Vector3 location_to_stay_near = Vector3.zero; 

    [SerializeField] float roam_radius = 5f;
    [SerializeField] float roam_time = 10f; 

    TickTimer tickTimer; 

    private void OnDisable() {
        state = EntityState.Wander; 
        currentlyCanMove = true;
    }

    public void DisenableMovement(bool canMove) {
        // if (!canMove) this.GetComponent<Rigidbody2D>().velocity = Vector3.zero; 
        canMove = !canMove; 
        if (aipath != null) {
            if (aipath.isStopped != canMove) {
                aipath.isStopped = canMove; 
            }
        }

        currentlyCanMove = !canMove; 
    }

    public void DoMovement(Vector3 move) {
        rigidbody2d.AddForce(move, ForceMode2D.Impulse); 
    }

    public void Stun(float time) {
        DisenableMovement(false); 

        Invoke("ReenableMovement", time); 
    }
    
    void ReenableMovement() {
        DisenableMovement(true); 
    }


    void Awake() {
        aipath = this.GetComponent<IAstarAI>(); 
        destinationSetter = this.GetComponent<AIDestinationSetter>(); 
        // if (destinationSetter == null) 
        destinationSetter ??= this.gameObject.AddComponent<AIDestinationSetter>();

        rigidbody2d = this.GetComponent<Rigidbody2D>();        

        // GlobalTickSystem.OnTick += delegate (object sender, GlobalTickSystem.OnTickEventArgs e) {TimeTick_OnTick(sender, e);}; 

        tickTimer = new TickTimer(roam_time); 
    }

    #if UNITY_EDITOR
    private void OnValidate() {
        tickTimer?.SetTimerSeconds(roam_time);
    }
    #endif



    private void Update() {
        if (!GameManager.Instance.IsRunning) {
            DisenableMovement(false); 
        } else {
            DisenableMovement(currentlyCanMove); 
        }

        if (state == EntityState.Sleep) {
            DisenableMovement(false);
            return; 
        }

        if (destinationSetter.target == null && state == EntityState.Wander && tickTimer.HasFinished()) {
            UpdateRoamLocation(); 
            tickTimer.StartTimer();
        } 

    }

    public void SetFocus() {
        state = EntityState.Focus; 
    }

    public void SetWander() {
        state = EntityState.Wander; 
    }

    public void SetSleep() {
        state = EntityState.Sleep; 
    }

    public void WakePeaceful() {
        state = EntityState.Wander; 
    }

    public void WakeForced(Transform other_trans = null) {
        SetTarget(other_trans); 
        state = EntityState.Focus; 
    }

    public void SetStayNearLocation(Vector3 posi) {
        location_to_stay_near = posi;
    }

    void UpdateRoamLocation() {
        Vector2 center;

        if (stays_near_location) {
            center = location_to_stay_near; 
        } else {
            center = this.transform.position; 
        }
        center += (Vector2.up * roam_radius).Rotate(Random.Range(0,360)); 

        SetDestination(center); 
    }

    public void SetTarget(Transform transform) {
        destinationSetter ??= this.GetComponent<AIDestinationSetter>(); 

        destinationSetter.target = transform; 
    }

    public void SetDestination(Vector3 position) {
        destinationSetter.target = null; 
        aipath.destination = position;
    }



    public enum EntityState {
        Sleep,
        Focus,
        Wander
    }


}


#endif