using UnityEngine;
using UnityEngine.SceneManagement;

namespace EKTemplate
{
    public class GameManager : MonoBehaviour
    {
        [Header("LEVEL'S"), Space(5)]
        public int level = -1;
        public int levelCount = 10;
        public int levelLoopFrom = 3;

        [Header("CURRENCY"), Space(5)]
        public int money = 0;

        #region Singleton
        public static GameManager instance = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
                GetDependencies();
            }
            else
            {
                DestroyImmediate(this);
            }
        }
        #endregion

        private void GetDependencies()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || level == -1) level = DataManager.instance.level;
            money = DataManager.instance.money;
        }

        #region DataOperations
        public void AddMoney(int amount)
        {
            money += amount;
            DataManager.instance.SetMoney(money);
        }

        public void LevelUp()
        {
            DataManager.instance.SetLevel(++level);
        }
        #endregion

        #region SceneOperations
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OpenScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void OpenScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        #endregion
    }
}