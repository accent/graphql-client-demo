// See https://aka.ms/new-console-template for more information

using graphql_client_demo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices(services =>
{
    services.AddClient().ConfigureHttpClient(client => { client.BaseAddress = new Uri("https://localhost:7038/graphql"); });
});
var buildServices = builder.Build();

var graphqlClient = buildServices.Services.GetRequiredService<Client>();
var allBooks = await graphqlClient.GetAllBooks.ExecuteAsync();

foreach (var book in allBooks.Data.Book)
{
    Console.WriteLine($"[{book.BookId}] {book.Title}, ISBN: {book.Isbn}");
}

var authorResponse = await graphqlClient.GetAuthor.ExecuteAsync(1);
var author = authorResponse.Data.Author;
Console.WriteLine($"Author: {author.FirstName} {author.LastName}");

var authorRemovalResult = await graphqlClient.RemoveAuthor.ExecuteAsync("4");
Console.WriteLine($"Author removed? {authorRemovalResult.Data.Boolean}");