using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
public class Auth
{
    public string GetAccessToken()
    {

        string clientid = "a09a2fc526a54c80a205a2b3ac521f28";
        string clientsecret = "04c1e16c8a52484fa54d653eb1df59b5";
        SpotifyToken token = new SpotifyToken();
        string url = "https://accounts.spotify.com/api/token";


        var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientid, clientsecret)));


        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);


        webRequest.Method = "POST";
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.Accept = "application/json";
        webRequest.Headers.Add("Authorization: Basic " + encode_clientid_clientsecret);


        var request = ("grant_type=client_credentials");
        byte[] req_bytes = Encoding.ASCII.GetBytes(request);
        webRequest.ContentLength = req_bytes.Length;


        Stream strm = webRequest.GetRequestStream();
        strm.Write(req_bytes, 0, req_bytes.Length);
        strm.Close();


        HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
        String json = "";
        using (Stream respStr = resp.GetResponseStream())
        {
            using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
            {
                json = rdr.ReadToEnd();
                rdr.Close();
            }
        }
        token = JsonConvert.DeserializeObject<SpotifyToken>(json);
        return token.Access_token;
    }
}