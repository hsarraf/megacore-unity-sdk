using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;


namespace MegaCore.WebRequest
{

    public enum Method
    {
        GET, POST
    }
    public enum Encode
    {
        FORM, JSON, NONE
    }

    public enum ErrorType
    {
        NetErr, HttpErr, NoUID
    }

    public class WData
    {
        public string sid;
        public WData(string sid)
        {
            this.sid = sid;
        }
    }

    public class WebRequest
    {
        /// <summary>
        /// generic
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="wdata"></param>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFail"></param>
        /// <param name="onNetErr"></param>
        public WebRequest(MonoBehaviour mono, object wdata, string url, Method method, Encode encode, Action<string> onSuccess = null, Action<string, ErrorType> onFail = null)
        {
            mono.StartCoroutine(WebRequestCo(wdata, url, method, encode, onSuccess, onFail));
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="wdata"></param>
        /// <param name="url"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFail"></param>
        /// <param name="onNetErr"></param>
        public WebRequest(MonoBehaviour mono, object wdata, string url, Action<string> onSuccess = null, Action<string, ErrorType> onFail = null)
        {
            mono.StartCoroutine(WebRequestCo(wdata, url, Method.GET, Encode.NONE, onSuccess, onFail));
        }

        /// <summary>
        /// POST (FORM/JSON)
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="wdata"></param>
        /// <param name="url"></param>
        /// <param name="encode"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFail"></param>
        /// <param name="onNetErr"></param>
        public WebRequest(MonoBehaviour mono, object wdata, string url, Encode encode, Action<string> onSuccess = null, Action<string, ErrorType> onFail = null)
        {
            mono.StartCoroutine(WebRequestCo(wdata, url, Method.POST, encode, onSuccess, onFail));
        }

        /// <summary>
        /// Call Coroutine
        /// </summary>
        /// <param name="wdata"></param>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFail"></param>
        /// <param name="onNetErr"></param>
        /// <returns></returns>
        public IEnumerator WebRequestCo(object wdata, string url, Method method, Encode encode, Action<string> onSuccess, Action<string, ErrorType> onFail)
        {
            UnityWebRequest request = null;
            if (wdata == null)
            {
                request = UnityWebRequest.Get(url);
                request.downloadHandler = new DownloadHandlerBuffer();
            }
            else if (method == Method.POST)
            {
                if (encode == Encode.JSON)
                {
                    byte[] jsonBinary = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(wdata));
                    DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
                    UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
                    uploadHandlerRaw.contentType = "application/json";
                    request = new UnityWebRequest(url, method.ToString(), downloadHandlerBuffer, uploadHandlerRaw);
                }
                else if (encode == Encode.FORM)
                {
                    List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
                    foreach (PropertyInfo property in wdata.GetType().GetRuntimeProperties())
                    {
                        //formData.Add(property.GetValue(wdata));
                    }
                    request = UnityWebRequest.Post(url, formData);
                    request.downloadHandler = new DownloadHandlerBuffer();
                }
            }
            else if (method == Method.GET)
            {
                url += "?";
                foreach (PropertyInfo property in wdata.GetType().GetRuntimeProperties())
                {
                    Debug.Log(property.Name);
                    url += string.Format("{0}={1}&", property.Name, property.GetValue(wdata));
                }
                url = url.Remove(url.Length - 1);
                request = UnityWebRequest.Get(url);
                request.downloadHandler = new DownloadHandlerBuffer();
            }
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.ConnectionError)
            {
                if (request.result != UnityWebRequest.Result.ProtocolError)
                {
                    onSuccess?.Invoke(request.downloadHandler.text);
                }
                else
                {
                    onFail?.Invoke(request.error, ErrorType.HttpErr);
                }
            }
            else
            {
                onFail?.Invoke(request.error, ErrorType.NetErr);
            }
            request.Dispose();
        }

    }

}