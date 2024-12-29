using System;
using System.Collections.Generic;
using System.Linq;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Better.Conditions.Runtime
{
    [Serializable]
    public abstract class ComplexCondition : Condition
    {
        [Select]
        [SerializeReference] private Condition[] _sourceSet;

        protected HashSet<Condition> CurrentSet { get; private set; }

        public ComplexCondition(IEnumerable<Condition> conditions)
        {
            _sourceSet = conditions.ToArray();
        }

        protected ComplexCondition() : this(Array.Empty<Condition>())
        {
        }

        public override void Rebuild()
        {
            base.Rebuild();
            
            CurrentSet ??= new HashSet<Condition>();
            CurrentSet.Clear();
            CurrentSet.CollectValidated(_sourceSet);
            CurrentSet.Rebuild();
        }

        protected override bool Validate(out Exception exception)
        {
            if (_sourceSet == null)
            {
                var message = $"{nameof(_sourceSet)} cannot be null";
                exception = new InvalidOperationException(message);
                return false;
            }

            exception = null;
            return true;
        }
    }
}