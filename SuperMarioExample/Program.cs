// See https://aka.ms/new-console-template for more information
using SuperMarioExample;

Console.WriteLine("Hello, Super Mario FSM!");

var logger = new ConsoleMarioStateLogger();
var sm = new SuperMarioStateMashine(logger);
sm.GetItem(SuperMarioStateMashine.Transition.Mushroom);
sm.GetItem(SuperMarioStateMashine.Transition.Mushroom);
sm.GetItem(SuperMarioStateMashine.Transition.Feather);
sm.GetItem(SuperMarioStateMashine.Transition.Feather);
sm.GetItem(SuperMarioStateMashine.Transition.Mushroom);
sm.GetItem(SuperMarioStateMashine.Transition.Flower);
sm.GetItem(SuperMarioStateMashine.Transition.Flower);
sm.GetItem(SuperMarioStateMashine.Transition.Mushroom);
sm.GetItem(SuperMarioStateMashine.Transition.Mushroom);
sm.GetItem(SuperMarioStateMashine.Transition.Feather);
sm.GetItem(SuperMarioStateMashine.Transition.Mushroom);
sm.GetItem(SuperMarioStateMashine.Transition.Flower);

Console.WriteLine($"Super Mario FSM State must be 'FireMario' - {sm.FSMState}");
Console.ReadLine();
