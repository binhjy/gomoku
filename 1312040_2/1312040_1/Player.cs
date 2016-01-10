using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _1312040_1
{
    class Player : INotifyPropertyChanged
    {
        //public Chess_board board;

        int row = Chess_board._row;
        int column = Chess_board._column;

        public int[,] arr;
        public int[,] arr_win;

        public Player()
        {
            //board = new Chess_board();
            //int  row = board.row;
            // int column = board.column;

            arr = new int[row, column];
            set_value_default(arr);

            arr_win = new int[row, column];
            set_value_default(arr_win);


        }

        private void set_value_default(int[,] arr)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    arr[i, j] = 0;
                }
            }
        }



        public bool check_horizoltal(int a, int b)
        {
            arr_win[a, b] = 1;

            int count = 1;
            int x = a + 1;
            while (x < column && arr[a, b] == arr[x, b])
            {
                count++;
                arr_win[x, b] = 1;
                x++;
            }
            x = a - 1;
            while (x >= 0 && arr[a, b] == arr[x, b])
            {
                count++;
                arr_win[x, b] = 1;
                x--;
            }
            if (count >= 5)
            {
                return true;
            }
            else
            {
                set_value_default(arr_win);
                return false;
            }

            
        }

        public bool check_vertical(int a, int b)
        {
            arr_win[a, b] = 1;
            int count = 1;
            int y = b + 1;
            while (y < row && arr[a, b] == arr[a, y])
            {
                count++;
                arr_win[a,y] = 1;
                y++;
            }
            y = b - 1;
            while (y >= 0 && arr[a, b] == arr[a, y])
            {
                count++;
                arr_win[a,y] = 1;
                y--;
            }
            if (count >= 5)
            {
                return true;
            }
            else
            {
                set_value_default(arr_win);
                return false;
            }
        }

        public bool check_diagonally(int a, int b)
        {
            arr_win[a, b] = 1;
            int count = 1;
            int x = a + 1;
            int y = b + 1;
            while (x < column && y < column && arr[a, b] == arr[x, y])
            {
                count = count + 1;
                arr_win[x, y] = 1;
                x++;
                y++;
            }
            x = a - 1;
            y = b - 1;
            while (x >= 0 && y >= 0 && arr[a, b] == arr[x, y])
            {
                count = count + 1;
                arr_win[x, y] = 1;
                x--;
                y--;
            }
            if (count >= 5)
            {
                return true;
            }
            else
            {
                set_value_default(arr_win);
                return false;
            }
        }
        public bool check_diagonally_extra(int a, int b)
        {
            arr_win[a, b] = 1;
            int count = 1;
            int x = a + 1;
            int y = b - 1;
            while (x < column && y >= 0 && arr[a, b] == arr[x, y])
            {
                count++;
                arr_win[x, y] = 1;
                x++;
                y--;
            }
            x = a - 1;
            y = b + 1;
            while (x >= 0 && y < column && arr[a, b] == arr[x, y])
            {
                count++;
                arr_win[x, y] = 1;
                x--;
                y++;
            }
            if (count >= 5)
            {
                return true;
            }
            else
            {
                set_value_default(arr_win);
                return false;
            }
        }

        public bool IsWin(int x, int y)
        {
            if (check_horizoltal(x, y) == true || check_vertical(x, y) == true || check_diagonally(x, y) == true || check_diagonally_extra(x, y))
                // if (check_horizoltal(x, y) == true)
                return true;
            return false;
        }


        public int[] Find_i(int x, int y, Player player1) //x cot, y dong
        {
            int x_start = x;
            int y_start = y;
            int[] arr_1 = new int[2]; //dong, cot
            double[] arr1 = new double[10];
            double tren = y;
            double duoi = row - y;
            double trai = x;
            double phai = column - x;
            // double cheo_trai_tren = Math.Sqrt(x * x + y * y);
            // double cheo_phai_tren = Math.Sqrt(y*y + (row-x)*(row-x));
            // double cheo_trai_duoi = Math.Sqrt(x * x + (row - y) * (row - y));
            // double cheo_phai_duoi = Math.Sqrt((row - x) * (row - x) + (row - y) * (row - y));
            arr1[0] = tren;
            arr1[1] = duoi;
            arr1[2] = trai;
            arr1[3] = phai;
            // arr1[4] = cheo_trai_tren;
            // arr1[5] = cheo_phai_tren;
            //arr1[6] = cheo_trai_duoi;
            // arr1[7] = cheo_phai_duoi;

            double max = arr1[0];
            int i;
            for (i = 0; i < 8; i++)
            {
                if (arr1[i] > max )
                {
                    max = arr1[i];
                }
            }
            for (i = 0; i < 8; i++)
            {
                if (arr1[i] == max)
                {
                    break;
                }
            }
            // i = Array.IndexOf(arr_1, max);
            int a, b; //dong, cot


            while (true)
            {
                if (i == 0)
                {
                    y--;
                    while (y > 0 && (player1.arr[x, y] == 1 || arr[x, y] == 1)) //sai
                    {
                        y--;
                    }
                    if (y == 0 && (player1.arr[x, y] == 1 || arr[x, y] == 1))
                    {
                        i = 1;
                        x = x_start;
                        y = y_start;
                    }
                    else
                    {
                        a = y;
                        b = x;
                        arr_1[0] = a;
                        arr_1[1] = b;
                        break;
                    }
                }
                if (i == 1)
                {
                    y++;
                    while (y < row - 1 && (player1.arr[x, y] == 1 || arr[x, y] == 1)) //sai
                    {
                        y++;
                    }
                    if (y == row - 1 && (player1.arr[x, y] == 1 || arr[x, y] == 1))
                    {
                        i = 2;
                        x = x_start;
                        y = y_start;
                    }
                    else
                    {
                        a = y;
                        b = x;
                        arr_1[0] = a;
                        arr_1[1] = b;
                        break;
                    }
                }
                if (i == 2) //x cot y dong
                {
                    x--;

                    while (x > 0 && (player1.arr[x, y] == 1 || arr[x, y] == 1)) //sai
                    {
                        x--;
                    }
                    if (x == 0 && (player1.arr[x, y] == 1 || arr[x, y] == 1))
                    {
                        i = 3;
                        x = x_start;
                        y = y_start;
                        
                    }
                    else
                    {
                        b = x;
                        a = y;
                        arr_1[0] = a;
                        arr_1[1] = b;
                        break;
                    }
                }
                if (i == 3) //x cot y dong
                {
                    x++;
                    while (x < row - 1 && (player1.arr[x, y] == 1 || arr[x, y] == 1)) //sai
                    {
                        x++;
                    }
                    if (x == row - 1 && (player1.arr[x, y] == 1 || arr[x, y] == 1))
                    {
                        i = 0;
                        x = x_start;
                        y = y_start;
                    }
                    else
                    {
                        a = y;
                        b = x;
                        arr_1[0] = a;
                        arr_1[1] = b;
                        break;
                    }
                }

                //if (arr_1[0] >= 0 && arr_1[1] <= 11 && player1.arr[arr_1[0], arr_1[1]] == 0
                //    && arr_1[0] >= 0 && arr_1[1] <= 11 && arr[arr_1[0], arr_1[1]] == 0
                //    )
                //{
                //    break;
                //}

            }
            

                //case 4: //x cot y dong
                //    if (y - 1 > 0)
                //        a = y - 1;
                //    else
                //        a = y + 1;
                //    if (x - 1 > 0)
                //        b = x - 1;
                //    else
                //        b = x + 1;
                //    arr_1[0] = a;
                //    arr_1[1] = b;
                //    break;
                //case 5: //x cot y dong
                //    if (y - 1 > 0)
                //        a = y - 1;
                //    else
                //        a = y + 1;
                //    if (x + 1 < row)
                //        b = x + 1;
                //    else
                //        b = x - 1;
                //    arr_1[0] = a;
                //    arr_1[1] = b;
                //    break;
                //case 6: //x cot y dong
                //    if (y + 1 < row)
                //        a = y + 1;
                //    else
                //        a = y - 1;
                //    if (x - 1 > 0)
                //        b = x - 1;
                //    else
                //        b = x + 1;
                //    arr_1[0] = a;
                //    arr_1[1] = b;
                //    break;
                //case 7: //x cot y dong
                //    if (y + 1 < row)
                //        a = y + 1;
                //    else
                //        a = y - 1;
                //    if (x + 1 < row)
                //        b = x + 1;
                //    else
                //        b = x - 1;
                //    arr_1[0] = a;
                //    arr_1[1] = b;
                //    break;

            
            return arr_1;

        }








        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
