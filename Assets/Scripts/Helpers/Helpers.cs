namespace Assets.Scripts.Helpers
{
    using UnityEngine;

    public class Helpers : MonoBehaviour
    {
        public void RemoveChildrens(GameObject go)
        {
            while (go.transform.childCount > 0)
            {
                var child = go.transform.GetChild(0);
                child.transform.SetParent(null);
                Destroy(child.gameObject);
            }
        }
    }
}
