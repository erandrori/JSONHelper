using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// Json Async Task Helper API.
/// Eran Drori ver 1.0
/// </summary>
public class JSONHelper : MonoBehaviour
{
    public class UserInfo
    {
        public string bundle_id { get; set; }
        public string appv { get; set; }
        public string advertising_identifier { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public bool gdpr { get; set; }
        public bool ccpa { get; set; }
        public bool test_mode { get; set; }
        public string sdkv { get; set; }
        public string country_code { get; set; }
    }

    public class AdData
    {
        public string publisher_id { get; set; }
        public string channel_id { get; set; }
        public string ad_sequence_id { get; set; }
    }

    public class Root
    {
        public UserInfo user_info { get; set; }
        public AdData ad_data { get; set; }
    }

    public class JsonClass
    {
        public string SDK { get; set; }

        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }


    void Start()
    {
        _ = PostJSONAsync();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<JsonClass> GetJSONAsync()
    {
        string jsonUrl = "https://jsonplaceholder.typicode.com/todos/1";

        try
        {
            //var webClient = new WebClient();
            //webClient.Headers.Add(HttpRequestHeader.Cookie, "cookievalue");
            //var json = webClient.DownloadString(@"https://jsonplaceholder.typicode.com/todos/1");
            //RootObject objJson = JsonConvert.DeserializeObject<RootObject>(json);

            var httpClient = new HttpClient();

            string json = await httpClient.GetStringAsync(jsonUrl);

            // JsonClass jsonClass = UnityEngine.JsonUtility.FromJson<JsonClass>(json);

            JsonClass jsonClass = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonClass>(json);

            UnityEngine.Debug.Log("JSON userId: " + jsonClass.userId);
            UnityEngine.Debug.Log("JSON id: " + jsonClass.id);
            UnityEngine.Debug.Log("JSON title: " + jsonClass.title);
            UnityEngine.Debug.Log("JSON completed: " + jsonClass.completed);

            return jsonClass;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;

            UnityEngine.Debug.Log("Exception JSON string: " + msg);
        }
        return null;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<HttpResponseMessage> PostJSONAsync()
    {
        string jsonUrl = "https://jsonplaceholder.typicode.com/posts/";

        try
        {
            var httpClient = new HttpClient();

            // JsonClass jsonClass = UnityEngine.JsonUtility.FromJson<JsonClass>(json);

            Root post = new Root();
            post.user_info = new UserInfo();
            post.user_info.bundle_id = "123456789";
            post.user_info.country_code = "IL";

            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(post);

            HttpResponseMessage response = await httpClient.PostAsync(jsonUrl, new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json"));

            string content = await response.Content.ReadAsStringAsync();

            Root root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(content);

            UnityEngine.Debug.Log("JSON:" + " bundle_id: " + root.user_info.bundle_id + " country_code: " + root.user_info.country_code);

            return response;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;

            UnityEngine.Debug.Log("Exception JSON string: " + msg);
        }
        return null;
    }
}