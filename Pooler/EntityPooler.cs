using System;
using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using UnityEngine;

namespace Laserbean.General
{
    public class EntityPooler : Singleton<EntityPooler>
    {
        public List<ObjectPoolItem> MobList = new();
        public ObjectPoolItem GroundItemObjectPool;

        List<MiniObjectPooler> MobPool = new();
        MiniObjectPooler ItemPool;

        [NonSerialized]
        Dictionary<string, int> MobNameDict = new();

        private void Start()
        {
            int ind = 0;
            foreach (var mob in MobList) {
                MobPool.Add(new MiniObjectPooler(mob, transform));

                MobNameDict.Add(mob.objectToPool.name, ind);
                ind += 1;
            }

            ItemPool = new(GroundItemObjectPool, transform);
        }

        public bool IsMobName(string nnnn)
        {
            return MobNameDict.ContainsKey(nnnn);
        }

        public GameObject GetPooledMob(string name)
        {
            if (!enabled) return null;

            return MobPool[MobNameDict[name]].GetPooledObject();
        }

        public GameObject InstantiatePooledMob(string name)
        {
            if (!enabled) return null;

            return Instantiate(MobPool[MobNameDict[name]].GetPooledObject());
        }

        public GameObject GetNewGroundItem()
        {
            if (!enabled) return null;
            return ItemPool.GetPooledObject();
        }
    }


}
