using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameObjectFactory : ScriptableObject
{
    //fabric will keep all content in another scene, that will be loaded in main
    private Scene _contentScene;

    protected T CreateGameObjectInstance<T>(T prefab) where T : MonoBehaviour
    {
        if (!_contentScene.isLoaded)
        {
            if (Application.isEditor)
            {
                _contentScene = SceneManager.GetSceneByName(name);

                if (!_contentScene.isLoaded)
                {
                    _contentScene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                _contentScene = SceneManager.CreateScene(name);
            }
        }
        T instance = Instantiate(prefab);
        SceneManager.MoveGameObjectToScene(instance.gameObject, _contentScene);
        return instance;
    }
}

