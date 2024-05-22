using Godot;

public partial class SignalBus : Node 
{
    [Signal]
    public delegate void DisplayDialogEventHandler(string texKey);
}