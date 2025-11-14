using _301._3_Development.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _301._3_Development.models;

namespace _301._3_Development.Scripts.Session
{
    public class SessionManager
    {
        private static SessionManager _instance;
        private static readonly object _lock = new();

        public UserDTO CurrentUser { get; private set; }
        public byte[] SessionKey { get; private set; }
        public DateTime SessionStartTime { get; private set; }
        public TimeSpan SessionTimeout { get; set; } = TimeSpan.FromMinutes(30);

        

        public static SessionManager Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new SessionManager();
                }
            }
        }

        public ApiClient Api { get; private set; }
        public string JwtToken { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;

        private SessionManager()
        {
            //Api = new ApiClient("https://<IpOfServer>:5001/api/"); // do this if local connection dosent work
            Api = new ApiClient("https://raspberrypi.local:5001/api/");
        }

        public bool IsSessionActive()
        {
            if (!IsLoggedIn) return false;
            return (DateTime.Now - SessionStartTime) < SessionTimeout;
        }

        public void StartSession(UserDTO user, string token)
        {
            CurrentUser = user;
            JwtToken = token;
            Api.SetToken(token);
            SessionStartTime = DateTime.Now;
        }

        public void EndSession()
        {
            // Wipe sensitive data from memory
            
            CurrentUser = null;
            JwtToken = null;
            Api.SetToken(null);

            
        }

    }

}
