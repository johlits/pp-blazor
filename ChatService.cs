using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pp_blazor.ScopedService;

public class ChatService
{
    private string userName = null;
    private string eventTitle = null;
    private List<string> chat = new List<string>();
    private int pingId = 0;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public string UserName
    {
        get => userName;
        set
        {
            userName = value;
            OnPropertyChanged();
        }
    }

    public string EventTitle
    {
        get => eventTitle;
        set
        {
            eventTitle = value;
            OnPropertyChanged();
        }
    }

    public List<string> Chat
    {
        get => chat;
        set
        {
            chat = value;
            OnPropertyChanged();
        }
    }

    public int PingId
    {
        get => pingId;
        set
        {
            pingId = value;
            OnPropertyChanged();
        }
    }
}