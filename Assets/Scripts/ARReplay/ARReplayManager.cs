using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ota.ndi
{
    public class ARReplayManager : MonoBehaviour
    {
        [HideInInspector] public Texture texture = null;
        [HideInInspector] public string metadatastr = null;
        [HideInInspector] public string bgmetadatastr = null;
        private ARReplay _arreplay = null;

        // Start is called before the first frame update
        void Start()
        {
            _arreplay = new ARReplay();
        }

        void OnDestroy()
        {
            _arreplay.Dispose();
            _arreplay = null;
        }

        // Update is called once per frame
        void Update()
        {
            _arreplay.Update();
            texture = _arreplay.Texture;
        }
    }
}