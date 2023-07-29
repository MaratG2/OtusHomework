using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Homework8.Events
{
    public class EventBus
    {
        private readonly Dictionary<Type, IEventHandlerCollection> _handlers = new Dictionary<Type, IEventHandlerCollection>();
        private readonly Queue<object> _eventQueue = new Queue<object>();

        private bool _isRunning;
        
        public void Subscribe<T>(Action<T> handler)
        {
            var eventType = typeof(T);

            if (!_handlers.ContainsKey(eventType))
            {
                _handlers.Add(eventType, new EventHandlerCollection<T>());
            }
            
            _handlers[eventType].Subscribe(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var eventType = typeof(T);

            if (_handlers.TryGetValue(eventType, out var eventHandlerCollection))
            {
                eventHandlerCollection.Unsubscribe(handler);
            }
        }

        public void RaiseEvent<T>(T evt)
        {
            if (_isRunning)
            {
                _eventQueue.Enqueue(evt);
                return;
            }
            
            _isRunning = true;
            
            var eventType = evt.GetType();
            Debug.Log(eventType);

            if (!_handlers.TryGetValue(eventType, out var eventHandlerCollection))
            {
                Debug.Log("No subscribers found");
                return;
            }

            eventHandlerCollection.RaiseEvent(evt);

            _isRunning = false;

            if (_eventQueue.Any())
            {
                RaiseEvent(_eventQueue.Dequeue());
            }
        }

        private interface IEventHandlerCollection
        {
            public void Subscribe(Delegate handler);

            public void Unsubscribe(Delegate handler);

            public void RaiseEvent<T>(T evt);
        }
        
        private sealed class EventHandlerCollection<T> : IEventHandlerCollection
        {
            private readonly List<Delegate> _handers = new List<Delegate>();

            private int _currentIndex = -1;
            
            public void Subscribe(Delegate handler)
            {
                _handers.Add(handler);
            }

            public void Unsubscribe(Delegate handler)
            {
                var index = _handers.IndexOf(handler);
                _handers.RemoveAt(index);

                if (index <= _currentIndex)
                {
                    --_currentIndex;
                }
            }

            public void RaiseEvent<TEvent>(TEvent evt)
            {
                if (evt is not T concreteEvent)
                {
                    return;
                }

                for (_currentIndex = 0; _currentIndex < _handers.Count; ++_currentIndex)
                {
                    var handler = (Action<T>) _handers[_currentIndex];
                    handler.Invoke(concreteEvent);
                }

                _currentIndex = -1;
            }
        }
    }
}