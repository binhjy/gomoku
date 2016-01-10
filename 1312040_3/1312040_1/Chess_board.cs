﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json.Linq;

using Quobject.SocketIoClientDotNet.Client;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Globalization;
using System.IO;


namespace _1312040_1
{
    class Chess_board : INotifyPropertyChanged
    {
        Chess chess_temp = new Chess();
        bool vServer = false;

        bool flag_turn = false;
        BackgroundWorker bg = new BackgroundWorker();

        Xu_li_chat xu_li_chat = new Xu_li_chat();

        string image_black = @"pack://application:,,,/1312040_1;component/image/black.png";
        string image_white = @"pack://application:,,,/1312040_1;component/image/white3.png";

        static public Socket socket;
        int play = -1;
        static public int turn;
        #region
        static public int _row = 12;
        static public int _column = 12;




        public int row
        {
            get { return _row; }
            set { _row = value; OnPropertyChanged("row"); }
        }

        public int column
        {
            get { return _column; }
            set { _column = value; OnPropertyChanged("column"); }
        }

        public ICommand PVP
        {
            get;
            internal set;
        }

        public ICommand PVC
        {
            get;
            internal set;
        }

        public ICommand NewGame
        {
            get;
            internal set;
        }

        public ICommand CvServer
        {
            get;
            internal set;
        }

        Brush _Background_PVP;

        public Brush Background_PVP
        {
            get { return _Background_PVP; }
            set { _Background_PVP = value; OnPropertyChanged("Background_PVP"); }
        }

        Brush _Background_PVC;

        public Brush Background_PVC
        {
            get { return _Background_PVC; }
            set { _Background_PVC = value; OnPropertyChanged("Background_PVC"); }
        }

        Brush _Background_CvServer;

        public Brush Background_CvServer
        {
            get { return _Background_CvServer; }
            set { _Background_CvServer = value; OnPropertyChanged("Background_CvServer"); }
        }

        Brush _Background_NewGame;

        public Brush Background_NewGame
        {
            get { return _Background_NewGame; }
            set { _Background_NewGame = value; OnPropertyChanged("Background_NewGame"); }
        }


        #endregion

        public Player player1 = new Player();
        public Player player2 = new Player();

        #region
        List<Chess> _list_chess;
        public List<Chess> list_chess
        {
            get { return _list_chess; }
            set { _list_chess = value; OnPropertyChanged("list_chess"); }
        }

        #endregion

        #region
        public Chess_board()
        {
            Background_PVP = Brushes.Brown;
            Background_PVC = Brushes.Brown;
            Background_CvServer = Brushes.Brown;
            Background_NewGame = Brushes.Brown;

            Chess chess;
            
            socket = null;
            turn = -1;
            list_chess = new List<Chess>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {

                    if ((i + j) % 2 == 0)
                        chess = new Chess { Row = i, Column = j, Background=Brushes.Wheat,IsTurn=0};
                    else
                        chess = new Chess { Row = i, Column = j, Background = Brushes.DarkGray, IsTurn = 0 };
                    list_chess.Add(chess);
                }
            }

            ClickCommand = new Command<Chess>(OnSquareClick);

            PVP = new CommandHandler(CreatePVP, true);
            PVC = new CommandHandler(CreatePVC, true);
            NewGame = new CommandHandler(CreateNewGame, true);
            CvServer = new CommandHandler(CreateCvServer, true);

            Xu_li_chat.SomethingHappened += Xu_li_chat_SomethingHappened;

            bg.WorkerReportsProgress = true;
            bg.DoWork += bg_DoWork;
            bg.ProgressChanged += bg_ProgressChanged;
            bg.RunWorkerCompleted += bg_RunWorkerCompleted;

            delete_all();
        }

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("complete");

            
        }

        void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            //int[] arr_1 = (int[])e.Argument;
            int[] arr_1 = player2.Find_i(chess_temp.Column, chess_temp.Row, player1);
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Xu_li(arr_1);
            }));
            
        }

        void Xu_li_chat_SomethingHappened(object sender, EventArgs e)
        {
            play = 2;
           // if (turn == 0)
           // {
                
           // }
           // else if (turn == 1)
           // {
                //MessageBox.Show("turn 1");
                socket.On("NextStepIs", (data1) =>
                        {
                            string str1 = data1.ToString();
                            string[] numbers = Regex.Split(str1, @"\D+");
                            if (numbers[1] == "1")
                            {
                                Chess che = list_chess.Find(item => item.Row == int.Parse(numbers[2]) && item.Column == int.Parse(numbers[3]));
                                int i = list_chess.IndexOf(che);
                                player2.arr[int.Parse(numbers[3]), int.Parse(numbers[2])] = 1;
                                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    list_chess[i].MyImageSource = new BitmapImage(new Uri(image_white));
                                }));

                                if (vServer)
                                {
                                    chess_temp.Column = int.Parse(numbers[3]);
                                    chess_temp.Row = int.Parse(numbers[2]);

                                    bg.RunWorkerAsync();
                                }

                                socket.On("EndGame", (data) =>
                                 {
                                     string name2 = Xu_li_chat.Name_player2;

                                     if (player2.IsWin(int.Parse(numbers[3]), int.Parse(numbers[2])))
                                     {

                                         int[,] arr = new int[row, column];
                                         arr = player2.arr_win;
                                         for (int j = 0; j < row; j++)
                                         {
                                             for (int k = 0; k < column; k++)
                                             {
                                                 if (arr[j, k] == 1)
                                                 {
                                                     Chess ches = list_chess.Find(item => item.Row == k && item.Column == j);
                                                     int t = list_chess.IndexOf(ches);
                                                     Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                                                     {

                                                         list_chess[t].OptionBlink = true;
                                                     }));
                                                 }
                                             }
                                         }
                                     }
                                 });

                                
                            }

                            if (numbers[1] == "0")
                            {

                                //ClickCommand = new Command<Chess>(OnSquareClick);

                                //Chess chess1 = (Chess)sender;
                                //MessageBox.Show(chess1.Row.ToString());


                                //Chess chess = chess_temp;
                               // int i = list_chess.IndexOf(chess);

                                //Chess chess = list_chess.Find(item => item.Row == chess_temp.Row && item.Column == chess_temp.Column);
                               
                                //int i = list_chess.IndexOf(chess);

                                Chess chess = list_chess.Find(item => item.Row == int.Parse(numbers[2]) && item.Column == int.Parse(numbers[3]));
                                int i = list_chess.IndexOf(chess);

                                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    list_chess[i].MyImageSource = new BitmapImage(new Uri(image_black));
                                }));
                                player1.arr[chess.Column, chess.Row] = 1;

                                socket.On("EndGame", (data) =>
                                {

                                    string name1 = xu_li_chat.Text_textbox_name;

                                    if (player1.IsWin(chess.Column, chess.Row))
                                    {

                                        int[,] arr = new int[row, column];
                                        arr = player1.arr_win;
                                        for (int j = 0; j < row; j++)
                                        {
                                            for (int k = 0; k < column; k++)
                                            {
                                                if (arr[j, k] == 1)
                                                {
                                                    Chess ches = list_chess.Find(item => item.Row == k && item.Column == j);
                                                    int t = list_chess.IndexOf(ches);
                                                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                                                    {

                                                        list_chess[t].OptionBlink = true;
                                                    }));

                                                }
                                            }
                                        }
                                    }

                                });


                            }
                        });
           // }
        }

        public void CreatePVP()
        {
            delete_all();
            play = 0;
            //Background_PVP = new SolidColorBrush(Color.FromArgb(243, 243, 17, 0));
            Background_PVP = Brushes.Red;

            Background_PVC = Brushes.Brown;
            Background_CvServer = Brushes.Brown;
            Background_NewGame = Brushes.Brown;

        }
        public void CreatePVC()
        {
            //delete_all();
            play = 1;
            Background_PVC = Brushes.Red;

            Background_CvServer = Brushes.Brown;
            Background_NewGame = Brushes.Brown;
            Background_PVP = Brushes.Brown;
        }

        public void CreateCvServer()
        {
            //delete_all();
            vServer = true;
            Background_CvServer = Brushes.Red;

            Background_PVC = Brushes.Brown;
            Background_NewGame = Brushes.Brown;
            Background_PVP = Brushes.Brown;
        }


        public void CreateNewGame()
        {
            delete_all();
            vServer = false;
            play = -1;
            Background_NewGame = Brushes.Red;

            Background_PVC = Brushes.Brown;
            Background_CvServer = Brushes.Brown;
            Background_PVP = Brushes.Brown;
        }
        #endregion

        #region
        int flag = 0;
        

        private void OnSquareClick(Chess chess)
        {
            #region
            int i = list_chess.IndexOf(chess);
            chess_temp = chess;
            if (play == 0)
            {
                if (flag == 0 && player2.arr[chess.Column, chess.Row] != 1 && player1.arr[chess.Column, chess.Row] != 1)
                {
                    list_chess[i].MyImageSource = new BitmapImage(new Uri(image_black));

                    player1.arr[chess.Column, chess.Row] = 1;
                    if (player1.IsWin(chess.Column, chess.Row))
                    {
                        int[,] arr = new int[row, column];
                        arr = player1.arr_win;
                        for (int j = 0; j < row; j++)
                        {
                            for (int k = 0; k < column; k++)
                            {
                                if (arr[j, k] == 1)
                                {
                                    Chess che = list_chess.Find(item => item.Row == k && item.Column == j);
                                    int t = list_chess.IndexOf(che);
                                    list_chess[t].OptionBlink = true;
                                }
                            }
                        }

                    }
                    //MessageBox.Show("player1 win");


                    flag = 1;
                }
                else if (flag == 1 && player1.arr[chess.Column, chess.Row] != 1 && player2.arr[chess.Column, chess.Row] != 1)
                {
                    //list_chess[i].Background = Brushes.Blue;
                    list_chess[i].MyImageSource = new BitmapImage(new Uri(image_white));

                    player2.arr[chess.Column, chess.Row] = 1;
                    if (player2.IsWin(chess.Column, chess.Row))
                    {
                        int[,] arr = new int[row, column];
                        arr = player2.arr_win;
                        for (int j = 0; j < row; j++)
                        {
                            for (int k = 0; k < column; k++)
                            {
                                if (arr[j, k] == 1)
                                {
                                    Chess che = list_chess.Find(item => item.Row == k && item.Column == j);
                                    int t = list_chess.IndexOf(che);
                                    list_chess[t].OptionBlink = true;
                                }
                            }
                        }

                    }

                    flag = 0;
                }
            }

            else if (play == 1)
            {
                if (flag == 0 && player2.arr[chess.Column, chess.Row] != 1 && player1.arr[chess.Column, chess.Row] != 1)
                {
                    list_chess[i].MyImageSource = new BitmapImage(new Uri(image_black));
                    player1.arr[chess.Column, chess.Row] = 1;
                    if (player1.IsWin(chess.Column, chess.Row))
                    {
                        int[,] arr = new int[row, column];
                        arr = player1.arr_win;
                        for (int j = 0; j < row; j++)
                        {
                            for (int k = 0; k < column; k++)
                            {
                                if (arr[j, k] == 1)
                                {
                                    Chess che = list_chess.Find(item => item.Row == k && item.Column == j);
                                    int t = list_chess.IndexOf(che);
                                    list_chess[t].OptionBlink = true;
                                }
                            }
                        }

                    }
                    flag = 1;
                }
                if (flag == 1)
                {
                    bg.RunWorkerAsync();

                    flag = 0;


                }
            }
            else if (play == 2)
            {
                if (turn == 1)
                {
                    socket.Emit("MyStepIs", JObject.FromObject(new { row = chess.Row, col = chess.Column }));
                    //list_chess[i].MyImageSource = new BitmapImage(new Uri(image_white));

                }
                if (turn == 0)
                {
                    socket.Emit("MyStepIs", JObject.FromObject(new { row = chess.Row, col = chess.Column }));
                }
            }
            else if (play == 3)
            {
                bg.RunWorkerAsync();

                
            }



            #endregion

            
            
        }
        #endregion

        public void Xu_li(int[] arr_1)
        {
            Chess che = list_chess.Find(item => item.Row == arr_1[0] && item.Column == arr_1[1]);
            player2.arr[arr_1[1], arr_1[0]] = 1;
            if (vServer)
            {
                socket.Emit("MyStepIs", JObject.FromObject(new { row = che.Row, col = che.Column }));

                if (player2.IsWin(arr_1[1], arr_1[0]))
                {
                    int[,] arr = new int[row, column];
                    arr = player2.arr_win;
                    for (int j = 0; j < row; j++)
                    {
                        for (int k = 0; k < column; k++)
                        {
                            if (arr[j, k] == 1)
                            {
                                Chess ches = list_chess.Find(item => item.Row == k && item.Column == j);
                                int t = list_chess.IndexOf(ches);
                                list_chess[t].OptionBlink = true;


                            }
                        }
                    }
                }
                return;


            }

            int i = list_chess.IndexOf(che);
            list_chess[i].MyImageSource = new BitmapImage(new Uri(image_black));
            
            if (player2.IsWin(arr_1[1], arr_1[0]))
            {
                int[,] arr = new int[row, column];
                arr = player2.arr_win;
                for (int j = 0; j < row; j++)
                {
                    for (int k = 0; k < column; k++)
                    {
                        if (arr[j, k] == 1)
                        {
                            Chess ches = list_chess.Find(item => item.Row == k && item.Column == j);
                            int t = list_chess.IndexOf(ches);
                            list_chess[t].OptionBlink = true;
                           

                        }
                    }
                }
            }
        }
        
        public void delete_all()
        {
            //string path = image_white;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    //if (player1.arr[i, j] != 0)
                        player1.arr[i, j] = 0;
                    //if (player1.arr_win[i, j] != 0)
                         player1.arr_win[i, j] = 0;
                   // if (player2.arr[i, j] != 0)
                        player2.arr[i, j] = 0;
                   // if (player2.arr[i, j] != 0)
                        player2.arr_win[i, j] = 0;
                }
            }

            for (int i = 0; i < row * column; i++)
            {
                list_chess[i].OptionBlink = false;
               // if (list_chess[i].MyImageSource != null)
              //  {
                    list_chess[i].MyImageSource = null;
                   // list_chess[i].MyImageSource = new BitmapImage();
               // }


            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(value.ToString());
            image.EndInit();
            return image;
        }




        #region
        private Command<Chess> _ClickCommand;

        public Command<Chess> ClickCommand
        {

            get { return _ClickCommand; OnPropertyChanged("ClickCommand"); }
            set { _ClickCommand = value; }

        }




        public class Command<T> : ICommand
        {
            public Action<T> Action { get; set; }

            public void Execute(object parameter)
            {
                if (Action != null && parameter is T)
                    Action((T)parameter);
            }

            public bool CanExecute(object parameter)
            {
                return IsEnabled;
            }

            private bool _isEnabled = true;
            public bool IsEnabled
            {
                get { return _isEnabled; }
                set
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                        CanExecuteChanged(this, EventArgs.Empty);
                }
            }

            public event EventHandler CanExecuteChanged;

            public Command(Action<T> action)
            {
                Action = action;
            }
        }


        public class CommandHandler : ICommand
        {
            private Action _action;
            private bool _canExecute;
            public CommandHandler(Action action, bool canExecute)
            {
                _action = action;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _action();
            }
        }


        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }
        #endregion
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

