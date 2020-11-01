using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuController
{
    public class MainMenuController : MonoBehaviour
    {
        public Wildfire wildFire = null;
        public GameObject treesContainer = null;

        public void StartSimulation()
        {
            if (wildFire)
                wildFire.simulationStarted = true;
        }

        public void StopSimulation()
        {
            if (wildFire)
                wildFire.simulationStarted = false;
        }

        public void ResetSimulation()
        {
            if (wildFire)
                wildFire.Reset();

            Tree[] trees = treesContainer.GetComponentsInChildren<Tree>();

            foreach (Tree tree in trees)
            {
                tree.Reset();
            }
        }
    }
}
