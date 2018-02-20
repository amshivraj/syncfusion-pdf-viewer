using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.PdfViewer.Models
{
    public class ChartData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ChartData> _top10WordsFromPDFLoaded;

        public ObservableCollection<ChartData> Top10WordsFromPDFLoaded
        {
            get
            {
                return _top10WordsFromPDFLoaded;
            }
            set
            {
                _top10WordsFromPDFLoaded = value;
                OnPropertyChanged("Top10WordsFromPDFLoaded");
            }
        }

        public string Word { get; set; }

        public Int32 Count { get; set; }
        public Int32 ParagraphCount { get; set; }
        public Int32 TotalUniqueWords { get; set; }
        public Int32 NumberOfSentences { get; set; }
        public Int32 AvgSetenceLength { get; set; }
        public Int32 AverageWordLength { get; set; }

        public List<string> ListOfDetailsToPrint { get; set; }


        public void OnPropertyChanged(string propertyName)
        {
            //When the property value is changed
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}


