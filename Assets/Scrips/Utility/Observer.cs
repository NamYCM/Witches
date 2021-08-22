using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IObserverSkill
{
    void OnNotify (SkillType skillType);
}

public abstract class Subject : MonoBehaviour {
    private HashSet<IObserverSkill> observers = new HashSet<IObserverSkill>();

    public void RegisterObserver (IObserverSkill observer) {
        observers.Add(observer);
    }

    public void RemoveObserver (IObserverSkill observer) {
        observers.Remove(observer);
    }

    public void Notify (SkillType skillType) {
        foreach (var observer in observers) {
            observer.OnNotify(skillType);
        }
    }
}



