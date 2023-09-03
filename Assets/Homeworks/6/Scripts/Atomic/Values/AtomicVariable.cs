using System;
using Declarative;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic
{
    [Serializable]
    public class AtomicVariable<T> : IAtomicVariable<T>, IDisposable
    {
        public event Action<T> OnChanged
        {
            add { this.onChanged += value; }
            remove { this.onChanged -= value; }
        }
        public event Action<T> OnUniqueChanged
        {
            add { this.onUniqueChanged += value; }
            remove { this.onUniqueChanged -= value; }
        }

        public T Value
        {
            get { return this.value; }
            set
            {
                if (value != null)
                {
                    if(this.value == null)
                        this.onUniqueChanged?.Invoke(value);
                    else if(!this.value.Equals(value))
                        this.onUniqueChanged?.Invoke(value);
                }
                
                this.value = value;
                this.onChanged?.Invoke(value);
            }
        }

        private Action<T> onChanged;
        private Action<T> onUniqueChanged;

        [OnValueChanged("OnValueChanged")]
        [SerializeField]
        private T value;

        public AtomicVariable()
        {
            this.value = default;
        }

        public AtomicVariable(T value)
        {
            this.value = value;
        }

#if UNITY_EDITOR
        private void OnValueChanged(T value)
        {
            this.onChanged?.Invoke(value);
        }
#endif
        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}