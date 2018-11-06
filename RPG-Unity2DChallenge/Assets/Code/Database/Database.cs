using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Utility;

namespace Project.Data {
    public class Database : Singleton<Database> {

        const string DATABASE_LOCATION = "https://www.hicksy.ca/Unity2DChallenge/Unity2DChallenge.php";

        public delegate void DatabaseCallback(string Text);

        public void QueryDatabase(QueryValues QueryValues, DatabaseCallback Callback)
        {
            StartCoroutine(Databasecall(DATABASE_LOCATION, toFormVariables(QueryValues), Callback));
        }

        public QueryValues GenerateQueryValues(string Command, Dictionary<string, string> Values) {
            List<QueryCommandValues> qcvs = new List<QueryCommandValues>();
            QueryValues qv = new QueryValues();
            qv.Command = Command;

            foreach (KeyValuePair<string, string> pair in Values) {
                QueryCommandValues qcv = new QueryCommandValues();
                qcv.Function = pair.Key;
                qcv.Value = pair.Value;
                qcvs.Add(qcv);
            }

            qv.CommandArray = qcvs.ToArray();
            return qv;
        }

        private IEnumerator Databasecall(string Path, WWWForm Varibles, DatabaseCallback Callback)
        {
            WWW urlRequest = new WWW(Path, Varibles);

            yield return urlRequest;

            Callback.Invoke(urlRequest.text);
        }

        private WWWForm toFormVariables(QueryValues QueryValues) {
            WWWForm form = new WWWForm();
            string jsonString = JsonUtility.ToJson(QueryValues);
            form.AddField("JSONString", jsonString);
            return form;
        }
	}

    [Serializable]
    public class QueryValues {
        public string Command;
        public QueryCommandValues[] CommandArray;
    }

    [Serializable]
    public class QueryCommandValues {
        public string Function;
        public string Value;
    }

    [Serializable]
    public class GeneralReturn {
        public bool Result;
        public string Details;
    }
}
