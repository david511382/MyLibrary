using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDbHelper
{
    public class DBStructure
    {
        public DatabaseConnStrStruct this[string dbName]
        {
            get
            {
                return _connStrs[dbName];
            }
        }

        private Dictionary<string, DatabaseConnStrStruct> _connStrs;

        public DBStructure()
        {
            _connStrs = new Dictionary<string, DatabaseConnStrStruct>();
        }

        public void SetMainAt(string dbName,string key, string conStr)
        {
            if (!_connStrs.ContainsKey(dbName))
                _connStrs.Add(dbName, new DatabaseConnStrStruct(key, conStr));

            _connStrs[dbName].SetMain(key,conStr);
        }

        public void AddReadOnlyAt(string dbName, string key, string conStr)
        {
            if (!_connStrs.ContainsKey(dbName))
                _connStrs.Add(dbName, new DatabaseConnStrStruct());
            _connStrs[dbName].AddReadOnly(key, conStr);
        }

        public void AddColdAt(string dbName, string key, string conStr)
        {
            if (!_connStrs.ContainsKey(dbName))
                _connStrs.Add(dbName, new DatabaseConnStrStruct());
            _connStrs[dbName].AddCold(key, conStr);
        }

        public void AddColdReadOnlyAt(string dbName, string key, string conStr)
        {
            if (!_connStrs.ContainsKey(dbName))
                _connStrs.Add(dbName, new DatabaseConnStrStruct());
            _connStrs[dbName].AddColdReadOnly(key, conStr);
        }

        public string GetDbByName(string dbName)
        {
            string result;
            foreach(DatabaseConnStrStruct db in _connStrs.Values)
            {
                try
                {
                    result = db[dbName];
                    return result;
                }
                catch(Exception e)
                {

                }                    
            }
            throw new Exception("no found");
        }
    }

    public struct DatabaseConnStrStruct
    {
        public enum DbStructType
        {
            Main,
            ReadOnly,
            Cold,
            ColdReadOnly
        }

        public Dictionary<string,string> this[DbStructType dbStructType]
        {
            get
            {
                switch (dbStructType)
                {
                    case DbStructType.Main:
                        Dictionary<string, string> result = new Dictionary<string, string>();
                        result.Add(_hot.Key, _hot.Value);
                        return result;
                    case DbStructType.ReadOnly:
                        return _hotReadOnly;
                    case DbStructType.Cold:
                        return _cold;
                    case DbStructType.ColdReadOnly:
                        return _coldReadOnly;
                    default:
                        throw new Exception("wrong struct type");
                }
            }
        }

        public string this[string dbName]
        {
            get
            {
                if (_hot.Key.Equals(dbName))
                    return _hot.Value;

                foreach (KeyValuePair<string, string> item in _hotReadOnly)
                    if (item.Key.Equals(dbName))
                        return item.Value;

                foreach (KeyValuePair<string, string> item in _cold)
                    if (item.Key.Equals(dbName))
                        return item.Value;

                foreach (KeyValuePair<string, string> item in _coldReadOnly)
                    if (item.Key.Equals(dbName))
                        return item.Value;

                throw new Exception("not found");
            }
        }

        private KeyValuePair<string,string> _hot;
        private Dictionary<string,string> _hotReadOnly, _coldReadOnly, _cold;

        public DatabaseConnStrStruct(string name, string hot)
        {
            _hot = new KeyValuePair<string, string>( name,hot);
            _hotReadOnly = new Dictionary<string, string>();
            _cold = new Dictionary<string, string>();
            _coldReadOnly = new Dictionary<string, string>();
        }

        public void SetMain(string name, string hot)
        {
            _hot = new KeyValuePair<string, string>(name, hot);
        }

        public void AddReadOnly(string name,  string readOnly)
        {
            _hotReadOnly.Add(name, readOnly);
        }

        public void AddCold(string name, string cold)
        {
            _cold.Add(name, cold);
        }

        public void AddColdReadOnly(string name, string coldReadOnly)
        {
            _coldReadOnly.Add(name, coldReadOnly);
        }
    }
}
