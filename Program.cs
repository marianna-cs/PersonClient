
using PersonClient.ExternalConnectors;

var externalMethodController = new ExternalMethodController(new HttpConnector());

var firstName = Console.ReadLine();

var lastName = Console.ReadLine();

await externalMethodController.AddPerson(firstName, lastName);

var persons = await externalMethodController.GetPersonsAsync();

Console.WriteLine("=========== RESPONSE =========");
foreach(var p in persons)
{
    Console.WriteLine(p);
}
Console.WriteLine("=========== END RESPONSE =========");
