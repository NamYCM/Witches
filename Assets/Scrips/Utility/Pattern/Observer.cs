using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches {
    public interface IObserver
    {
        void OnNotify (object key, object data);
    }

    public abstract class SingleSubject<T> : SingletonMono<T> where T : MonoBehaviour
    {
        Dictionary<object, List<IObserver>> observers = new Dictionary<object, List<IObserver>>();

        public void RegisterObserver (object key, IObserver observer) {
            if (!observers.ContainsKey(key)) {
                List<IObserver> actions = new List<IObserver>();
                actions.Add(observer);
                observers.Add(key, actions);
            }
            else {
                observers[key].Add(observer);
            }
        }

        public void SendMessage (object key) {
            if (observers.ContainsKey(key)) {
                foreach (IObserver observer in observers[key]) {
                    observer.OnNotify(key, null);
                }
            }
        }
        public void SendMessage (object key, object data) {
            if (observers.ContainsKey(key)) {
                foreach (IObserver observer in observers[key]) {
                    observer.OnNotify(key, data);
                }
            }
        }

        public void RemoveRegister (object key, IObserver observer) {
            if (observers.ContainsKey(key)) {
                for (int i = 0; i < observers[key].Count; i++) {
                    if (observers[key][i] == observer) {
                        observers[key].RemoveAt(i);
                    }
                }
            }
        }

        public void RemoveAllRegister (IObserver observer) {
            foreach (KeyValuePair<object, List<IObserver>> item in observers) {
                for (int i = 0; i < item.Value.Count; i++) {
                    if (item.Value[i] == observer) {
                        item.Value.RemoveAt(i);
                    }
                }
            }
        }
    }
    
    public abstract class Subject : MonoBehaviour
    {
        Dictionary<object, List<IObserver>> observers = new Dictionary<object, List<IObserver>>();

        public void RegisterObserver (object key, IObserver observer) {
            if (!observers.ContainsKey(key)) {
                List<IObserver> actions = new List<IObserver>();
                actions.Add(observer);
                observers.Add(key, actions);
            }
            else {
                observers[key].Add(observer);
            }
        }

        public void SendMessage (object key) {
            if (observers.ContainsKey(key)) {
                foreach (IObserver observer in observers[key]) {
                    observer.OnNotify(key, null);
                }
            }
        }
        public void SendMessage (object key, object data) {
            if (observers.ContainsKey(key)) {
                foreach (IObserver observer in observers[key]) {
                    observer.OnNotify(key, data);
                }
            }
        }

        public void RemoveRegister (object key, IObserver observer) {
            if (observers.ContainsKey(key)) {
                for (int i = 0; i < observers[key].Count; i++) {
                    if (observers[key][i] == observer) {
                        observers[key].RemoveAt(i);
                    }
                }
            }
        }

        public void RemoveAllRegister (IObserver observer) {
            foreach (KeyValuePair<object, List<IObserver>> item in observers) {
                for (int i = 0; i < item.Value.Count; i++) {
                    if (item.Value[i] == observer) {
                        item.Value.RemoveAt(i);
                    }
                }
            }
        }
    }
}



