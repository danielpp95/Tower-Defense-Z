namespace Assets.Scripts.Manager
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class WarriorsPrefabBase : MonoBehaviour
    {
        public Material HighlightedMaterial;

        public GameObject Goku;
        public List<Material> GokuMaterials;

        public GameObject GokuSSJ1;
        public List<Material> GokuSSJ1Materials;

        public GameObject GokuSSJ2;
        public List<Material> GokuSSJ2Materials;

        public GameObject GokuSSJ3;
        public List<Material> GokuSSJ3Materials;
    }
}
