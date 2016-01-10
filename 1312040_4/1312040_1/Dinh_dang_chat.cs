using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _1312040_1
{
    class Dinh_dang_chat : INotifyPropertyChanged
    {
        string _Ten;
        string _Noi_dung_chat;
        string _Thoi_gian;
        public string Ten {
            get
            {
                return _Ten;
            }
            set
            {
                _Ten = value;
                OnPropertyChanged("Ten");
            }
        
        }

        public string Noi_dung_chat {
            get
            {
                return _Noi_dung_chat;
            }
            set
            {
                _Noi_dung_chat = value;
                OnPropertyChanged("Noi_dung_chat");
            }
        }

        //string _Ten_noi_dung_chat;
        //public string Ten_noi_dung_chat
        //{
        //    get
        //    {
        //        return _Ten_noi_dung_chat;
        //    }
        //    set
        //    {
        //        _Ten_noi_dung_chat = value;
        //        OnPropertyChanged("Ten_noi_dung_chat");
        //    }
        //}
          
        //public void Hinh_anh
        //{
        //    get;
        //    set;
        //}
        public string Thoi_gian{
            get
            {
                return _Thoi_gian;
            }
            set
            {
                _Thoi_gian = value;
                OnPropertyChanged("Thoi_gian");
            }
        }

        //public override string ToString()
        //{
        //    return String.Format("{0} {1} {2}", Ten, Noi_dung_chat, Thoi_gian, HorizontalAlignment.Right);
        //}


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
