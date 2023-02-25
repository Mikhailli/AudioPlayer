using Microsoft.AspNetCore.SignalR;

namespace AudioPlayer;

public class AudioHub : Hub
{
    public async Task Send(string audio)
    {
        await Clients.Caller.SendAsync("Notify", "Ваш трек добавлен");
    }
}