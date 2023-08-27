using System.Collections.Generic;

namespace _project.Scripts.Observer
{
    public class GameObserver
    {
        private static GameObserver _instance;
        
        private List<IObserver> _observers;
        
        private GameObserver()
        {
            _observers = new List<IObserver>();
        }
        
        public static GameObserver GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameObserver();
            }
                
            return _instance;
        }
        
        public void Add(IObserver observer)
        {
            _observers.Add(observer);
        }
 
        public void Remove(IObserver observer)
        {
            _observers.Remove(observer);
        }
 
        public void Notify(GameAction gameAction)
        {
            foreach (IObserver observer in _observers)
            {
                observer.React(gameAction);
            }
        }
    }
}