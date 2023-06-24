using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Provides functionality to compare two JObject objects and detect differences between them.
    /// </summary>
    public class JObjectComparisonResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the two objects are equal.
        /// </summary>
        public bool AreEqual { get; set; }

        /// <summary>
        /// Gets or sets the list of differences between the two objects.
        /// </summary>
        public List<string> Differences { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JObjectComparisonResult"/> class.
        /// </summary>
        public JObjectComparisonResult()
        {
            AreEqual = true;
            Differences = new List<string>();
        }

        /// <summary>
        /// Compares two JObject objects and detects differences between them.
        /// </summary>
        /// <param name="obj1">The first JObject object to compare.</param>
        /// <param name="obj2">The second JObject object to compare.</param>
        public void CompareJObjects(JObject obj1, JObject obj2)
        {
            foreach (var property in obj1.Properties())
            {
                JToken value1 = property.Value;
                JToken value2;

                if (obj2.TryGetValue(property.Name, out value2))
                {
                    if (!JToken.DeepEquals(value1, value2))
                    {
                        CompareJTokens(property.Name, value1, value2);
                    }
                }
                else
                {
                    AreEqual = false;
                    Differences.Add($"Key '{property.Name}' is missing from the second object.");
                }
            }
            // add each json object to the list of differences by serialize it to a string

        }

        /// <summary>
        /// Compares two JTokens and adds any differences to the list.
        /// </summary>
        /// <param name="key">The key associated with the JTokens.</param>
        /// <param name="value1">The first JToken to compare.</param>
        /// <param name="value2">The second JToken to compare.</param>
        private void CompareJTokens(string key, JToken value1, JToken value2)
        {
            if (value1.Type != value2.Type)
            {
                AreEqual = false;
                Differences.Add($"Key '{key}' has different value types: {value1.Type} and {value2.Type}.");
            }
            else if (value1.Type == JTokenType.Object)
            {
                CompareJObjects(value1 as JObject, value2 as JObject);
            }
            else if (value1.Type == JTokenType.Array)
            {
                var array1 = value1 as JArray;
                var array2 = value2 as JArray;

                if (array1.Count != array2.Count)
                {
                    AreEqual = false;
                    Differences.Add($"Key '{key}' has arrays with different lengths: {array1.Count} and {array2.Count}.");
                }
                else
                {
                    for (int i = 0; i < array1.Count; i++)
                    {
                        CompareJTokens($"{key}[{i}]", array1[i], array2[i]);
                    }
                }
            }
            else if (!JToken.DeepEquals(value1, value2))
            {
                AreEqual = false;
                Differences.Add($"Key '{key}' has different values: '{value1}' and '{value2}'.");
            }
        }
    }
}
