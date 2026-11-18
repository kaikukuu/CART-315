using UnityEngine;

namespace Cart315.FirstPerson
{ //this class is responsible for pulling stars towards the black hole when they enter the pull zone
    public class BlackHolePullZone : MonoBehaviour
    {
        private BlackHole _blackHole;
        //this method is called to initialize the black hole reference for the pull zone
        internal void Initialize(BlackHole blackHole)
        {
            _blackHole = blackHole;
        }
        //this method is called when a collider stays within the trigger zone of the black hole pull zone
        private void OnTriggerStay(Collider other)
        {
            if (_blackHole != null)
                _blackHole.PullStar(other);
        }
    }
}
