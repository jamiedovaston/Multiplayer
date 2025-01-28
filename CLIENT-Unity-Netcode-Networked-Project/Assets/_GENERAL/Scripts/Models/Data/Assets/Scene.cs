using UnityEngine;

namespace Game.Model.DataAsset.Scene
{
    [CreateAssetMenu(fileName = "scene_NAME", menuName = "JD/Scenes")]
    public class Scene : DataAsset<Scene>
    {
        [SerializeField] private int _buildIndex;
        public int buildIndex { get { return _buildIndex; } }

        new protected static string GetResourcePath() => "Scenes";
    }
}
