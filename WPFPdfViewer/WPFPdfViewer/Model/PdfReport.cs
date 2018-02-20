using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.PdfViewer.PdfReport
{
    public class PdfReport : INotifyPropertyChanged
    {
        private Stream docStream;

        public event PropertyChangedEventHandler PropertyChanged;
        public Stream DocumentStream
        {
            get
            {
                return docStream;
            }
            set
            {
                docStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }
        public PdfReport()
        {
            //Load the stream from the local system.
            docStream = new FileStream("Barcode.pdf", FileMode.OpenOrCreate);
        }
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}


