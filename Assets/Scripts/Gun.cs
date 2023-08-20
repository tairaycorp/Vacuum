using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
[System.Serializable]
public class Gun 
{
    public Gun_Def gunDef;
    public List<Attachment> a;
    public GameObject owner;
    public List<GameObject> projectiles;
    public List<EventResponse> durationBased;
    public bool canFire;
    public Transform spawnPoint;
    public bool newClick;
    public float timeStampFire;
    public Transform gunPos;
    public MonoBehaviour playerMove;
    public int magSize;
    public float reloadTimeDuration;
    // Object reference exception?
    public void Initialize(Gun_Def gd, GameObject newOwner, List<Attachment> addAttachments = null) {
        owner = newOwner;
        gunDef = gd;
        a = gunDef.baseAttachments;
        if(addAttachments != null) {
            a.AddRange(addAttachments);
        }
        magSize = gd.magSize;
    }

    public Transform GetTransform() {
        return owner.transform;
    }

    public virtual float Event() {
        return 0;
    }
    public float OnFireEvent() {
        //Debug.Log("Here");
        timeStampFire = Time.time;
        // Here, maybe we make two concurrent lists: FireTypes (EventResponses that shoot projectiles) and OnFireEffects (EventResponses that modify values when we fire (movement speed))
        List<EventResponse> events = new List<EventResponse>();
        events = SortEventsByDescending(events, typeof(FireEventResponse));
        // Here we have Fire Type events. Now we want them to fire, so we run their response.
        foreach(EventResponse e in events) {
            e.Respond(this, null, 0f);
            magSize -= 1;
            if(magSize < 1) {
                //OnReloadEvent(true);
                //break;
            }
        }
        if(owner.GetComponent<PlaverMovement>() != null ) { 
            OnRecoilEvent();
        }

        return 0;
    }

    public float OnDamageCalcEvent(Collision2D coll) {
        float damage = 0f;
        List<EventResponse> events = new List<EventResponse>();
        events = SortEventsByDescending(events, typeof(DamageCalcEventResponse));
        
        foreach(EventResponse e in events) {
            damage += e.Respond(this, coll, damage);
        }
        Debug.Log("Damge Before Crit: " + damage);
        float newDamage = Crit(events.Cast<DamageCalcEventResponse>().ToList(), damage);
        bool isCrit = false;
        if(newDamage > damage) {
            isCrit = true;
        }
        damage = newDamage;
        Debug.Log("Damge After Crit: " + damage);
        if(coll.collider.gameObject.GetComponent<ActiveEnemy>() != null) {
            coll.gameObject.GetComponent<ActiveEnemy>().enemy.Damage(damage, isCrit);
        } else {
            coll.gameObject.GetComponent<PlaverMovement>().playerCharacter.Damage(damage, isCrit);
        }
        

        return damage;
    }

    public float OnPlayerTakeDamageEvent() {
        return 0;
    }

    public float OnHitEvent(Collision2D col) {
        if(col.collider.gameObject.GetComponent<ActiveEnemy>() != null || col.collider.gameObject.GetComponent<PlaverMovement>() != null) {
            List<EventResponse> events = new List<EventResponse>();
            events = SortEventsByDescending(events, typeof(OnHitEventResponse));
            // Here we have Fire Type events. Now we want them to fire, so we run their response.
            foreach(EventResponse e in events) {
                e.Respond(this, null, 0f);
            }
            OnDamageCalcEvent(col);

        }

        return 0;
    }

    List<EventResponse> SortEventsByDescending(List<EventResponse> events, Type type) {
        // Make a list of all DamageCalcResponses
        foreach(Attachment tar in a) {
            foreach(EventResponse tarEvent in tar.e) {
                if(tarEvent.GetType().IsSubclassOf(type)) {
                    events.Add(tarEvent);
                }
            }
        }

        events = events.OrderByDescending(e => e.priority).ToList();

        //Debug.Log(events);
        return events;
    }

    public bool OnReloadEvent(bool n) {
        
        newClick = n;
        int reload = 0; 
        int magEmptyTrue = 0;
        List<EventResponse> masterList = new List<EventResponse>();
        masterList = SortEventsByDescending(masterList, typeof(EventResponse));
        //Debug.Log("Master List Count: " + masterList.Count);
        List<EventResponse> events = new List<EventResponse>();
        foreach(EventResponse e in masterList) {
            if(e.GetType().IsSubclassOf(typeof(ReloadEventResponse))) {
                events.Add(e);
            }
        }
        //events = SortEventsByDescending(masterList, typeof(ReloadEventResponse));
        //Debug.Log("Reload Event Count: " + events.Count);

        List<EventResponse> magEmptyEvents = new List<EventResponse>();
        //magEmptyEvents = SortEventsByDescending(masterList, typeof(MagEmptyEventResponse));
        foreach(EventResponse e in masterList) {
            //Debug.Log(e.GetType());
            if(e.GetType().Equals(typeof(MagEmptyEventResponse))) {
                //Debug.Log()
                magEmptyEvents.Add(e);
            }
        }
        //Debug.Log("Mag Empty Events: " + magEmptyEvents[0] + " " + magEmptyEvents.Count);
        
        //events = events.Except(magEmptyEvents).ToList();
        //Debug.Log("RELOAD EVENTS AFTER MAG REMOVAL" + events.Count);
        

        if(magEmptyEvents.Count > 0) {
            reloadTimeDuration = 0;
            //Debug.Log(magEmptyEvents[0]);
            foreach(EventResponse e in magEmptyEvents) {
                MagEmptyEventResponse mEE = (MagEmptyEventResponse)e;
                if(mEE.fireWait > reloadTimeDuration) {
                    reloadTimeDuration = mEE.fireWait;
                }
                magEmptyTrue += (int)e.Respond(this, null, 0f);
             
            }
        } else {
            magSize = gunDef.magSize;
        }
       // Debug.Log("Getting to OnReload");
        if(magEmptyTrue >= 1) {
            canFire = false;
        //Debug.Log("Surving Mag Empty");
        } else {
           
            if(events.Count > 0) {
                //Debug.Log("Getting to foreach");
                foreach(EventResponse e in events) {
                    reload += (int)e.Respond(this, null, 0f);
                }
                if(reload > 0) {

                    // magSize = gunDef.magSize;
                    canFire = true;
                } else {
                    
                    canFire = false;
                }
                
            } else {
                //Debug.Log("Events is empty");
                if((Time.time - timeStampFire) >= 0.1f) {
                    canFire = true;
                } else {
                    canFire = false;
                }
            }
            }
            return canFire;
        }


    public void OnRecoilEvent() {
        float knockBackAmount = 0f;
        List<EventResponse> events = new List<EventResponse>();
        events = SortEventsByDescending(events, typeof(RecoilEventResponse));
        //Debug.Log("Knockback: " + knockBackAmount);
        // Here we have Fire Type events. Now we want them to fire, so we run their response.
        foreach(EventResponse e in events) {
            
            knockBackAmount += e.Respond(this, null, 0f);
           // Debug.Log("Knockback: " + knockBackAmount);
        }
        owner.GetComponent<PlaverMovement>().Knockback(knockBackAmount);
    }

    public void OnMagEmptyEvent() {

    }

    public float Crit(List<DamageCalcEventResponse> e, float damage) {
        float critChance = 0;
        float critMod = 0;
        foreach(DamageCalcEventResponse er in e) {
            critChance += er.critChance;
            critMod += (er.critMod - 1);
        }
        //Debug.Log(critChance + "  " + critMod);
        if(UnityEngine.Random.Range(0, 100) > critChance * 100) {
        } else {
            damage = damage + (damage * critMod);
            //Debug.Log("Damage After the Crit: " + damage);
        }
        return damage;
    }

    public float SpeedEventAdder(float currentSpeed) {
        return 0f;
    }
}


class EventResponseCondition {

}