using _project.Scripts.Observer;
using _project.Scripts.Save;

namespace _project.Scripts.Utilities
{
    public class PointsCounter : IObserver
    {
        private SaveManager _saveManager;
        private int _pointsCount;

        public PointsCounter()
        {
            _saveManager = SaveManager.GetInstance();
            _pointsCount = 0;
        }
        
        public void React(GameAction gameAction)
        {
            switch (gameAction)
            {
                case GameAction.TakePoint:
                    _pointsCount++;
                    break;
                case GameAction.CharacterCollide:
                    TrySaveNewRecord();
                    break;
                case GameAction.GameRestart:
                case GameAction.GameEnd:
                    TrySaveNewRecord();
                    _pointsCount = 0;
                    break;
            }
        }

        private void TrySaveNewRecord()
        {
            if (_pointsCount > _saveManager.SaveData.lastRecord)
            {
                var saveData = _saveManager.SaveData;
                
                saveData.lastRecord = _pointsCount;
                saveData.lastDifficultyLevel = _saveManager.SaveData.difficultyLevel;
                
                _saveManager.Save(saveData);
            }
        }
    }
}