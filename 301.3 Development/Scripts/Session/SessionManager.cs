using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.Scripts.Session
{
    public class SessionManager
    {
        private static SessionManager _instance;
        private static readonly object _lock = new();

        public User CurrentUser { get; private set; }
        public byte[] SessionKey { get; private set; }
        public DateTime SessionStartTime { get; private set; }
        public TimeSpan SessionTimeout { get; set; } = TimeSpan.FromMinutes(30);

        private SessionManager() { }

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

        public bool IsLoggedIn => CurrentUser != null;

        public bool IsSessionActive()
        {
            if (!IsLoggedIn) return false;
            return (DateTime.Now - SessionStartTime) < SessionTimeout;
        }

        public void StartSession(User user, byte[] sessionKey)
        {
            CurrentUser = user;
            SessionKey = sessionKey;
            SessionStartTime = DateTime.Now;
        }

        public void EndSession()
        {
            // Wipe sensitive data from memory
            if (SessionKey != null)
            {
                Array.Clear(SessionKey, 0, SessionKey.Length);
                SessionKey = null;
            }

            CurrentUser = null;
        }
        /*
          usage:::
         
            // On successful login
            var key = EncryptionHelper.DeriveSessionKey(user.PasswordHash);
            SessionManager.Instance.StartSession(user, key);

            // Check if session is still active
            if (!SessionManager.Instance.IsSessionActive())
            {
                SessionManager.Instance.EndSession();
                // Redirect user to login
            }

         */
    }

}
