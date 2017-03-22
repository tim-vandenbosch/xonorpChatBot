using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace xonorpChatBot.Twitch
{
    public class TwitchHandler
    {
        #region local vars
        public string _ip { get; set; }
        public int _port { get; set; }
        public string _userName { get; set; }
        public string _aothoken { get; set; }
        public string _channel { get; set; }

        private TcpClient _tcpClient;
        private StreamReader _inputStream;
        private StreamWriter _outputStream;

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TwitchHandler() { Initialization(); }

        /// <summary>
        /// Constructor TwitchHandler
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <param name="aothoken"></param>
        /// <param name="channel"></param>
        public TwitchHandler(string ip, int port, string userName, string aothoken, string channel)
        {
            this._ip = ip;
            this._userName = userName;
            this._port = port;
            this._aothoken = aothoken;
            this._channel = channel;

            Initialization();
        }

        /// <summary>
        /// Initialization of the connection
        /// </summary>
        protected void Initialization()
        {
            _tcpClient = new TcpClient(_ip, _port);
            _inputStream = new StreamReader(_tcpClient.GetStream());
            _outputStream = new StreamWriter(_tcpClient.GetStream());
            _outputStream.WriteLine("PASS " + _aothoken);
            _outputStream.WriteLine("NICK " + _userName);
            _outputStream.WriteLine("USER " + _userName + " 8 * :" + _userName);
            _outputStream.Flush();
        }

        /// <summary>
        /// Joining a chatroom on twitch
        /// </summary>
        public void JoinRoom()
        {
            try
            {
                _outputStream.WriteLine("JOIN #" + _channel);
                _outputStream.Flush();
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show("Something went wrong connecting to the channel: " + nre);
            }
        }

        /// <summary>
        /// Send A message to the chatroom
        /// </summary>
        /// <param name="message"></param>
        public void SendIrcMessage(string message)
        {
            _outputStream.WriteLine(message);
            _outputStream.Flush();
        }

        /// <summary>
        /// Send a message to the chatroom
        /// </summary>
        /// <param name="message"></param>
        public void SendChatMessage(string message)
        {
            SendIrcMessage(":" + _userName + "!" + _userName + "@" + _userName + ".tmi.twitch.tv PRIVMSG #" + _channel + " :" + message);
        }

        public async Task<string> Messagereader()
        {
            var readstring = await _inputStream.ReadLineAsync();
            if (string.IsNullOrEmpty(readstring) || readstring == " ")
            {
                return "";
            }
            return readstring;
        }
    }
}