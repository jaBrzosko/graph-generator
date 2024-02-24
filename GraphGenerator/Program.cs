using Cocona;
using GraphGenerator.Commands;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommands<GraphCreationCommands>();

app.Run();
