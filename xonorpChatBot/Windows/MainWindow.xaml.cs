using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using xonorpChatBot.Twitch;
using xonorpChatBot.Beam;
using xonorpChatBot.Beam.proxys;

namespace xonorpChatBot.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region vars
        private bool _connected;
        private TwitchHandler _twitchHandler;
        private TwitchCommands _twitchCommands;
        private string _ip, _username, _oauth, _channel;
        private int _port;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            connectedStatusBar.Items.Remove(connectedStatus);
            _connected = false;
            _ip = "irc.chat.twitch.tv";
            _port = 6667; // or port 443
            _username = "baerrybot";
            _oauth = "oauth:k1dwds0qzrklcdazlslkcrfuq3qw8i";
            _channel = "haazeyow";
        }

        #region Window Interaction

        private void ConnectingToTwitch(object sender, RoutedEventArgs e)
        {
            if (!_connected)
            {
                try
                {
                    if (string.IsNullOrEmpty(ChannelTextBox.Text) || ChannelTextBox.Text == "")
                    {
                        MessageBox.Show("No channel given!");
                        return;
                    }
                    _channel = ChannelTextBox.Text;
                    _twitchHandler = new TwitchHandler(_ip, _port, _username, _oauth, _channel);

                    _connected = true;

                    // layout
                    SetVisualConnectionToColor(1);

                    // join room before listening
                    _twitchHandler.JoinRoom();

                    // start thread
                    Task.Factory.StartNew(FillChatListBox);
                }
                catch (TimeoutException te)
                {
                    MessageBox.Show("A timeout occured, connection canceled: " + te);
                    throw;
                }
                finally
                {
                    // layout
                    SetVisualConnectionToColor(0);
                }
            }
            else
            {
                MessageBox.Show("Already connected to: " + _twitchHandler._channel);
            }
        }

        /// <summary>
        /// opens a db window in the future???
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenANewWindow(object sender, RoutedEventArgs e)
        {
            popupwindow test = new popupwindow();
            test.ShowDialog();
        }

        private void SendAMessageToTheChannel(object sender, RoutedEventArgs e)
        {
            if (_connected)
            {
                var message = chatInputTextBox.Text;
                _twitchHandler.SendChatMessage(message);
                chatInputTextBox.Clear();
            }
            else
            {
                MessageBox.Show("You must connect first to be able to chat!");
            }
        }

        private void getUserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            //using (var db = new XonorpChatBotDb())
            //{
            //    var user =
            //        from u in db.Users
            //        where u.Name == getUserInfoTextBox.Text
            //        select u;

            //    foreach (User u in user)
            //    {
            //        MessageBox.Show(u.Name + " Total points " + u.Points + " Total posts " + u.TotalPost + " First seen " + u.FirstSeen);
            //    }
            //}
        }

        

        #endregion

        #region HelperFunctions
        /// <summary>
        /// UI connection status (red/ green)
        /// </summary>
        /// <param name="status"></param>
        private void SetVisualConnectionToColor(int status)
        {
            if (status != 1) return;
            connectedStatusBar.Items.Remove(notConnectedStatus);
            connectedStatusBar.Items.Add(connectedStatus);
            connectedStatusBar.Background = Brushes.Green;
        }

        /// <summary>
        /// EDIT THIS FUNCTION
        /// </summary>
        private async void FillChatListBox()
        {
            while (_connected)
            {
                var readString = await _twitchHandler.Messagereader();
                var editedString = StringParser(readString);
                listBox.Dispatcher.Invoke(editedString.Length > 2
                    ? new Action(() => listBox.Items.Add(editedString[1] + ": " + editedString[editedString.Length - 1]))
                    : new Action(() => listBox.Items.Add(editedString[1])));
            }
            // stop thread???
        }

        /// <summary>
        /// EDIT THIS FUNTION
        /// This function should use cases and split up to other functions to
        /// make the code more clear, instead of what's happening now
        /// </summary>
        /// <param name="inputstring"></param>
        /// <returns></returns>
        private string[] StringParser(string inputstring)
        {
            var parsedString = inputstring;
            var split = new string[] { };
            if (parsedString.Contains("WHISPER"))
            {
                var charSeparatorWhisper = new char[] { ':', '!', '@', ':' };
                split = parsedString.Split(charSeparatorWhisper);
            }
            else
            {
                if (parsedString.Contains("PRIVMSG"))
                {
                    //split[5] message
                    var charSeparatorChat = new char[] { ':', '!', '@', '#', ':' };
                    split = parsedString.Split(charSeparatorChat);
                    //if (split[5].StartsWith("!"))
                    //{
                    //   // _twitchCommands.command(1,2)

                    //}
                    //else
                    //{
                    //    if (_database.checkIfUserExist(split[1]) == true)
                    //    {
                    //        _database.newPost(split[1], split[5]);
                    //    }
                    //    else
                    //    {
                    //        _database.newUser(split[1]);
                    //        _database.newPost(split[1], split[5]);
                    //    }

                    //}
                }
                else
                {
                    if (parsedString.Contains("PING"))
                    {
                        _twitchHandler.SendIrcMessage("PONG tmi.twitch.tv");
                    }
                    else
                    {
                        var charSeparator = new char[] { ':' };
                        split = parsedString.Split(charSeparator);
                    }
                }
            }
            return split;
        }
        #endregion

        #region apiTester Tab-item

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            var beamApiProxy = new ChannelApiProxy();

            textBlock.Text = await beamApiProxy.GetChannelsFromBeamAsync();
        }

        #endregion
    }
}
