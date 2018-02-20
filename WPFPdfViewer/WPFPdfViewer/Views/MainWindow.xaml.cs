using Microsoft.Win32;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.PdfViewer;
using Syncfusion.Windows.PdfViewer;
using Syncfusion.Pdf;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using WPF.PdfViewer.Models;
using WPF.PdfViewer.ViewModels;

namespace WPF.PdfViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        PDFViewModel pdfVM;
        public MainWindow()
        {
            InitializeComponent();
            pdfVM = new PDFViewModel();
            lvDataBinding.ItemsSource = pdfVM.GetOtherPDFReportData();
        }

        private async void LoadPDF(object sender, RoutedEventArgs e)
        {
            //Create a OpenFileDialog

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Pdf Files (.pdf)|*.pdf";

            dialog.ShowDialog();



            //Load the document from the open file dialog

            if (!string.IsNullOrEmpty(dialog.FileName))

            {
                //PdfDocumentView documentViewer = new PdfDocumentView();
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(dialog.FileName);
                PdfLoadedPageCollection pages = loadedDocument.Pages;

                documentViewer.ZoomMode = ZoomMode.FitWidth;
                documentViewer.LoadAsync(loadedDocument);

                //Task.Run(() => GeneratePDFReport(pages));
                var chartObj = await Task.Run(() => pdfVM.GeneratePDFReport(pages)); //pdfVM.GeneratePDFReport(pages);
                chartObj.Top10WordsFromPDFLoaded = chartObj.Top10WordsFromPDFLoaded;
                lvDataBinding.ItemsSource = chartObj.ListOfDetailsToPrint;
                series.ItemsSource = chartObj.Top10WordsFromPDFLoaded;
            }
        }

        private void PrintLoadedPDF(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            //Print the PDF document
            if (documentViewer.LoadedDocument != null)
                documentViewer.Print();
            else MessageBox.Show("Please load the PDF file");
            //dialog.ShowDialog();
        }

    }
}


