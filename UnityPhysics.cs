﻿using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;
using UnityVector3 = UnityEngine.Vector3;
using Vector3 = System.Numerics.Vector3;

namespace Physics
{
    public class UnityPhysics : IPhysics
    {
        private Collider[] _colliders;
        private GameObjectEntity[] _entities;

        public int Size
        {
            set
            {
                _colliders = new Collider[value];
                _entities = new GameObjectEntity[value];
                for (var i = 0; i < value; i++)
                {
                    _entities[i] = new GameObjectEntity();
                }
            }
        }

        public IEnumerable<IEntity> OverlapCapsule(Vector3 center, float halfHeight, float radius, Vector3 up)
        {
            return OverlapCapsule(center + up * halfHeight, center - up * halfHeight, radius);
        }
        
        public IEnumerable<IEntity> OverlapCapsule(Vector3 a, Vector3 b, float radius)
        {
            var length = UnityEngine.Physics.OverlapCapsuleNonAlloc(
                UnityPhysicUtils.Convert(a),
                UnityPhysicUtils.Convert(b),
                radius,
                _colliders
            );
            for (var i = 0; i < length; i++)
            {
                _entities[i].Impl = _colliders[i].gameObject;
            }

            return _entities.Take(length);
        }

        public IEnumerable<IEntity> OverlapSphere(Vector3 position, float radius)
        {
            var length = UnityEngine.Physics.OverlapSphereNonAlloc(
                UnityPhysicUtils.Convert(position),
                radius,
                _colliders
            );
            for (var i = 0; i < length; i++)
            {
                _entities[i].Impl = _colliders[i].gameObject;
            }

            return _entities.Take(length);
        }
    }
}