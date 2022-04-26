using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PersonClient.ExternalConnectors
{
    public class ExternalMethodController
    {
        private IExternalConnector _externalConnector;

        private string _url = $"https://localhost:7098/api/Person";

        public ExternalMethodController(IExternalConnector externalConnector)
        {
            _externalConnector = externalConnector ?? throw new ArgumentNullException(nameof(externalConnector));
        }

        public async Task<List<Person>> GetPersonsAsync()
        {
            var resp = await _externalConnector.SendRequestAsync(_url, "GET");

            if (resp == null)
            {
                throw new Exception();
            }
            return JsonConvert.DeserializeObject<List<Person>>(resp);
        }

        public async Task AddPerson(string firstName, string lastName)
        {
            var person = new PersonCreationRequest(firstName, lastName);

            var resp = await _externalConnector.SendRequestAsync(_url, "POST", JsonConvert.SerializeObject(person));
        
        }
    }

    public class PersonCreationRequest
    {
        public PersonCreationRequest()
        {
        }

        public PersonCreationRequest(string firstName, string lastName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
    public class Person
    {
        [JsonProperty("gid")]
        public Guid Gid { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"111Guid = {Gid}\nf.Name = {FirstName}\nl.Name = {LastName}\n";
        }
    }
}
