using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using SimpleWebService.App_Start;

namespace SimpleWebService.Controllers
{
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Initialize the file config.
        /// </summary>
        public FileConfig file = new FileConfig();

        /// <summary>
        /// Static field for string "name".
        /// </summary>
        public static string Key = "name";

        /// <summary>
        /// Static field for string "Gideon".
        /// </summary>
        public static string Value = "Gideon";

        /// <summary>
        /// Adding lockObject to create thread safe environment to prevent conflicts in accessing the same resource.
        /// </summary>
        private readonly object lockObject = new object();

        /// <summary>
        /// Get the value for "name".
        /// </summary>
        /// <returns>200 if succeeded, with value</returns>
        [HttpGet, Route("Get")]
        public HttpResponseMessage Get()
        {
            lock(lockObject)
            {
                HttpResponseMessage msg = new HttpResponseMessage();
                string value = file.fileKeyValues.Where(n => n.Key == Key).Select(s => s.Value).FirstOrDefault();
                if (value == null) value = string.Empty;
                msg.Content = new StringContent(value);
                msg.StatusCode = HttpStatusCode.OK;
                return msg;
            }
        }

        /// <summary>
        /// Get the value for the given key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>200 if succeeded, with value</returns>
        [HttpGet, Route("Get")]
        public HttpResponseMessage Get(string key)
        {
            lock (lockObject)
            {
                HttpResponseMessage msg = new HttpResponseMessage();
                string value = file.fileKeyValues.Where(n => n.Key == key).Select(s => s.Value).FirstOrDefault();
                if (value == null) value = string.Empty;
                msg.Content = new StringContent(value);
                msg.StatusCode = HttpStatusCode.OK;
                return msg;
            }
        }


        /// <summary>
        /// Put a { "name":"Gideon" } in file config. Return BadRequest when "name" already exist.
        /// </summary>
        /// <returns>202 if succeeded; 400 when key already exist</returns>
        [HttpPut, Route("Put")]
        public HttpResponseMessage Put()
        {
            lock (lockObject)
            {
                HttpResponseMessage msg = new HttpResponseMessage();
                if (!file.fileKeyValues.ContainsKey(Key))
                {
                    file.fileKeyValues.Add(Key, Value);
                    msg.StatusCode = HttpStatusCode.Accepted;
                    return msg;
                }

                msg.StatusCode = HttpStatusCode.BadRequest;
                return msg;
            }
        }

        /// <summary>
        /// Put a key value in file config. Return BadRequest when key already exist.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>202 if succeeded; 400 when key already exist</returns>
        [HttpPut, Route("Put")]
        public HttpResponseMessage Put(string key, string value)
        {
            lock (lockObject)
            {
                HttpResponseMessage msg = new HttpResponseMessage();
                if (!file.fileKeyValues.ContainsKey(key))
                {
                    file.fileKeyValues.Add(key, value);
                    msg.StatusCode = HttpStatusCode.Accepted;
                    return msg;
                }

                msg.StatusCode = HttpStatusCode.BadRequest;
                return msg;
            }
        }

        /// <summary>
        /// Get fired when a request is received. This will read a latest version of fileConfig object.
        /// </summary>
        /// <param name="controllerContext">HttpControllerContext</param>
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            lock (lockObject)
            {
                base.Initialize(controllerContext);
                file.ReadFromFile();
            }
        }

        /// <summary>
        /// Get fired when a request is finished. This will save a copy of the latest fileConfig object.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            lock (lockObject)
            {
                base.Dispose(disposing);
                file.WriteToFile();
            }
        }
    }
}
