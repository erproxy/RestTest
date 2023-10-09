using System;
using Cysharp.Threading.Tasks;
using Tools.GameTools;
using UnityEngine;
using UnityEngine.Networking;

namespace Models.WebTool
{
    public interface IWebToolService
    {
        UniTask<string> LoadJson(string url);
        UniTask<Texture2D> LoadTexture2D(string url);
    }
    public class WebToolService : IWebToolService
    {
        [Obsolete("Obsolete")]
        public async UniTask<string> LoadJson(string url)
        {
            var request = await UnityWebRequest.Get(url)
                .SendWebRequest()
                .ToUniTask();
            if (request.isHttpError || request.isNetworkError)
            {
                Debugger.LogError(request.error);
                request.Dispose();
                return String.Empty;
            }
            else
            {
                var json = request.downloadHandler.text;
                request.Dispose();
                return json;
            }
        }

        public async UniTask<Texture2D> LoadTexture2D(string url) 
        {
            var request = await UnityWebRequestTexture.GetTexture(url)
                .SendWebRequest()
                .ToUniTask();
            
           return ProcessRequest(request);
        }
        
        private Texture2D ProcessRequest(UnityWebRequest webRequest)
        {
            if (CheckNoErrors(webRequest))
            {
                var texture = DownloadHandlerTexture.GetContent(webRequest);
                if (texture != null)
                {
                    webRequest.Dispose();
                    return texture;
                }
            }

            webRequest.Dispose();
            return null;
        }

        private bool CheckNoErrors(UnityWebRequest webRequest)
        {
#if UNITY_2020_1_OR_NEWER
            var isNetworkError = webRequest.result == UnityWebRequest.Result.ConnectionError;
            var isHttpError = webRequest.result == UnityWebRequest.Result.ProtocolError;
#else
			var isNetworkError = webRequest.isNetworkError;
			var isHttpError = webRequest.isHttpError;
#endif
            return !isNetworkError && !isHttpError;
        }
    }
}