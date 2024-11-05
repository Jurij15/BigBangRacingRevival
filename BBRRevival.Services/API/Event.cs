using BBRRevival.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.API
{
    public class Event : DictionaryModel
    {
        public string eventName {  get; set; }
        public string eventType {  get; set; }
        public string messageId {  get; set; }
        public string id {  get; set; }
        public string header {  get; set; }
        public string message {  get; set; }
        public string label {  get; set; }
        public bool popup {  get; set; }
        public bool newsFeed {  get; set; }
        public long startTime {  get; set; }
        public long endTime {  get; set; }
        public Dictionary<string, object> eventData {  get; set; } //can be a tournament dictionary
        public List<Dictionary<string, object>> uris {  get; set; }

        /* SAMPLE URIS LIST
        List<Dictionary<string, object>> turis = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object> { { "uri", "http://example.com/1" } },
            new Dictionary<string, object> { { "uri", "http://example.com/2" } },
            new Dictionary<string, object> { { "uri", "http://example.com/3" } }
        };
        */
    }
}
