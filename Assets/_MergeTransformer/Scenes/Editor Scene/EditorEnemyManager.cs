using VTLTools;

namespace MergeAR.Editor
{
    public class EditorEnemyManager : Singleton<EditorEnemyManager>
    {
        public int currentEditingLevel;

        private void OnEnable()
        {
            currentEditingLevel = StaticVariables.CurrentLevel;
        }
    }
}