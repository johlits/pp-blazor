using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pp_blazor.ScopedService;

public class ChatService
{
    private string userName = null;
    private string eventTitle = null;
    private bool init;
    private List<Tuple<string, string, int>> chat = new List<Tuple<string, string, int>>();
    private int pingId = 0;
    public Queue<string> messages = new Queue<string>();

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void PostMessage(string message)
    {
        messages.Enqueue(message);
    }

    public bool Init 
    {
        get => init;
        set
        {
            init = value;
            OnPropertyChanged();
        }
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

    public List<Tuple<string, string, int>> Chat
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