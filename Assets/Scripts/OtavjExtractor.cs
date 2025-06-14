using Newtonsoft.Json;
using ota.ndi;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace ota.ndi
{
    public class OtavjExtractor : MonoBehaviour
    {
        [SerializeField] ARReplayManager _arreplayManager = null;
        [SerializeField] Shader _demuxShader = null;

        [SerializeField] RawImage _tmpcolorrawimage = null;
        [SerializeField] RawImage _tmpdepthrawimage = null;

        [HideInInspector] public RenderTexture ColorTexture;
        [HideInInspector] public RenderTexture DepthTexture;
        [HideInInspector] public MetadataInfo Metadata;
        [HideInInspector] public BgMetadataInfo BgMetadata;

        Material _demuxMaterial;

        // Start is called before the first frame update
        void Start()
        {
            _demuxMaterial = new Material(_demuxShader);
        }

        private void OnDestroy()
        {
            ReleaseInstanceObject();
        }

        void ReleaseInstanceObject()
        {
            Destroy(ColorTexture);
            Destroy(DepthTexture);
        }

        // Update is called once per frame
        void Update()
        {
            //ExtractMetadata();
            //ExtractBgMetadata();
            ExtractTextures();
        }

        void ExtractTextures()
        {
            var source = _arreplayManager.texture;
            if (source == null) return;
            // Lazy initialization
            if (ColorTexture == null) InitializeTextures(source);

            // Parameters from metadata
            //後で直すこと！
            //_demuxMaterial.SetVector("_DepthRange", Metadata.getDepthRange());
            _demuxMaterial.SetVector("_DepthRange", new Vector2(0.2f, 3.2f));

            // Blit (color/depth)
            ColorTexture.Release();
            Graphics.Blit(source, ColorTexture, _demuxMaterial, 0);
            _tmpcolorrawimage.texture = ColorTexture;
            DepthTexture.Release();
            Graphics.Blit(source, DepthTexture, _demuxMaterial, 1);
            _tmpdepthrawimage.texture = DepthTexture;
        }

        void InitializeTextures(Texture source)
        {
            //var w = source.width / 2;
            //var h = source.height / 2;
            //ColorTexture = new RenderTexture(w, h * 2, 0);
            
            var w = source.width;
            var h = source.height;
            Debug.Log("w:" + w);
            Debug.Log("h:" + h);

            ColorTexture = new RenderTexture(w, h, 0);
            ColorTexture.Create();
            DepthTexture = new RenderTexture(w, h, 0, RenderTextureFormat.RHalf);
            DepthTexture.Create();
        }

        void ExtractMetadata()
        {
            Regex reg = new Regex("\\{\"arcameraPosition\".+\\}", RegexOptions.Singleline);
            var match = reg.Match(_arreplayManager.metadatastr);
            var json = match.ToString();
            if (json != null)
            {
                Metadata = JsonConvert.DeserializeObject<MetadataInfo>(json);
            }
        }

        void ExtractBgMetadata()
        {
            Regex reg = new Regex("\\{\"verticesList\".+\\}", RegexOptions.Singleline);
            var match = reg.Match(_arreplayManager.bgmetadatastr);
            var json = match.ToString();
            if (json != null)
            {
                BgMetadata = JsonConvert.DeserializeObject<BgMetadataInfo>(json);
            }
        }
    }
}
