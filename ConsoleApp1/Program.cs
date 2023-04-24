// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

Console.WriteLine("Hello, Super Mario FSM!");

var sm = new SuperMarioStateMashine();
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
