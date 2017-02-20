using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Storage
{
    public class RedisSession
    {
        #region Members

        private string m_ConnectionString;
        protected IDatabase m_RedisConnection;
        protected ITransaction m_Tran;

        #endregion


        #region Properties

        public static ConnectionMultiplexer RedisConnectionGroup = null;
        public static object redisLock = new object();

        public string ConnectionString {
            get {
                return m_ConnectionString;
            }
        }
        public IDatabase RedisConnection
        {
            get
            {
                return m_RedisConnection;
            }
            set
            {
                m_RedisConnection = value;
            }
        }

        public RedisSession(string connectionString)
        {
            m_ConnectionString = connectionString;
        }

        public static RedisSession GetSession(string connectionString)
        {
            return new RedisSession(connectionString);
        }

        protected virtual void OpenConnection()
        {
            try
            {
                lock (redisLock)
                {
                    if (RedisSession.RedisConnectionGroup == null || !RedisSession.RedisConnectionGroup.IsConnected)
                    {
                        if (RedisSession.RedisConnectionGroup == null)
                        {
                            RedisSession.RedisConnectionGroup = ConnectionMultiplexer.Connect(m_ConnectionString);
                        }
                        else if (!RedisSession.RedisConnectionGroup.IsConnected)
                        {
                            RedisSession.RedisConnectionGroup.Dispose();
                            RedisSession.RedisConnectionGroup = ConnectionMultiplexer.Connect(m_ConnectionString);
                        }
                    }
                    m_RedisConnection = RedisConnectionGroup.GetDatabase();
                }
                if (m_RedisConnection == null)
                {
                    throw new Exception("Failed to connect to redis.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion


        #region Functions

        public RedisValue StringGet(RedisKey key ,CommandFlags flags=CommandFlags.None)
        {
            this.OpenConnection();

            try
            {
                return m_RedisConnection.StringGet(key, flags);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool StringDelete(RedisKey key, CommandFlags flags = CommandFlags.None)
        {
            this.OpenConnection();

            try
            {
                return m_RedisConnection.KeyDelete(key, flags);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool StringSet(RedisKey key, RedisValue value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            this.OpenConnection();

            try
            {
                return m_RedisConnection.StringSet(key, value, expiry, when, flags);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
