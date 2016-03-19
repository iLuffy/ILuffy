namespace ILuffy.IOP.Net
{
    using System;
    using System.IO;
    using System.Net;
    using I18N;
    using Newtonsoft.Json;

    public class JsonRequest : IJsonRequest
    {
        public T Get<T>(RequestParameters parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(parameter.Url);
            request.Method = "GET";
            request.ContentType = "text/json";
            //request.KeepAlive = true;
            if (parameter.Timeout.HasValue)
            {
                request.Timeout = parameter.Timeout.Value;
                request.ReadWriteTimeout = request.Timeout;
            }

            if (parameter.Cookie != null && parameter.Cookie.Count > 0)
            {
                request.CookieContainer = new CookieContainer();
                foreach (var item in parameter.Cookie)
                {
                    request.CookieContainer.Add(new Cookie(item.Key, item.Value, null, 
                        request.RequestUri.Authority));
                }
            }

            if (parameter.Header != null && parameter.Header.Count > 0)
            {
                foreach (var item in parameter.Header)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new IOPException(CoreRS.RequestFailedFormat(
                        parameter.Url, response.StatusCode, response.StatusDescription));
                }

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var json = reader.ReadToEnd();
                    var jsonSettings = new JsonSerializerSettings()
                    {
                        Error = (sender, e) => { throw e.ErrorContext.Error; }
                    };

                    var obj = JsonConvert.DeserializeObject<T>(json, jsonSettings);

                    return obj;
                }
            }
        }
    }
}
