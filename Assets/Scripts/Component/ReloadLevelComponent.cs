using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace FirstPlatformer.Components
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var scene = SceneManager.GetActiveScene(); // Получаем сцену которая на старте
            SceneManager.LoadScene(scene.name); //когда закгружаем сцену мы перещагружаем её
        }
    }
}

