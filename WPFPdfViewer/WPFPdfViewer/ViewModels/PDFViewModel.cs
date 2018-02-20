using WPF.PdfViewer.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.PdfViewer.ViewModels
{
    class PDFViewModel
    {
        ChartData chartObj;
        public PDFViewModel()
        {
            chartObj = new ChartData();
        }
        public ChartData GeneratePDFReport(PdfLoadedPageCollection pages)
        {
            try
            {
                StringBuilder extractedText = new StringBuilder();
                if (pages != null)
                {
                    //Parallel.ForEach<PdfPageBase>(pages.AsParallel(), page =>
                    //{


                    //});

                    foreach (PdfPageBase pageT in pages.AsParallel())
                    {
                        PdfPageBase page = pageT;
                        extractedText.Append(page.ExtractText());
                    }

                }
                var wordsCount = System.Text.RegularExpressions.Regex.Matches(extractedText.ToString(), "\\S+").Count;
                var sentences = extractedText.ToString().Split(new string[] { ". ", "\r\n\\" }, StringSplitOptions.None);
                var sentenceReport = (from sentence in sentences
                                      where sentence != string.Empty
                                      group sentence by sentence into tempBag
                                      //let count = tempBag.Count()
                                      //orderby count descending
                                      select new { Value = tempBag.Key, Length = tempBag.Key.Length }
                                  ).ToList();
                chartObj.NumberOfSentences = sentenceReport.Distinct().Count();
                if (chartObj.NumberOfSentences > 0)
                    chartObj.AvgSetenceLength = sentenceReport.Sum(x => x.Length) / chartObj.NumberOfSentences;

                string[] source = extractedText.ToString().Split(new char[] { '.', '?', '!', ' ', ';', ':', ',', '_' }, StringSplitOptions.RemoveEmptyEntries);
                var matchQuery = from word in source
                                 where word.ToLowerInvariant() == "\r\n".ToLowerInvariant()
                                 select word;

                chartObj.ParagraphCount = matchQuery.Count();


                var wordReport = (from word in source
                                  where word != string.Empty
                                  group word by word into tempBag
                                  let count = tempBag.Count()
                                  orderby count descending
                                  select new { Value = tempBag.Key, Count = count, Length = tempBag.Key.Length }
                                  ).ToList();
                chartObj.TotalUniqueWords = wordReport.Count();
                if (chartObj.TotalUniqueWords > 0)
                    chartObj.AverageWordLength = wordReport.Sum(x => x.Length) / chartObj.TotalUniqueWords;

                var topTenOccuringWords = (from obj in wordReport.OrderByDescending(x => x.Count).ToList().Take(10)
                                           where obj.Value != "\r\n"
                                           select new ChartData
                                           {
                                               Word = obj.Value,
                                               Count = obj.Count
                                           }).ToList();
                chartObj.Top10WordsFromPDFLoaded = new ObservableCollection<ChartData>(topTenOccuringWords);
                chartObj.ListOfDetailsToPrint = GetOtherPDFReportData(chartObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while generating PDF Reports in PdfViewer.ViewModels.PDFViewModel.GeneratePDFReport", ex);
            }

            return chartObj;
        }
        public List<string> GetOtherPDFReportData(ChartData chartObj = null)
        {

            var reportList = new List<String>();
            try
            {
                if (chartObj == null)
                    chartObj = new ChartData();

                var properties = chartObj.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    if (prop.PropertyType.ToString() == typeof(Int32).ToString() && prop.Name != "Count")
                    {
                        reportList.Add(String.Format("{0} : {1}", prop.Name, prop.GetValue(chartObj)));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while generating list of Other Details in PDF Reports in PdfViewer.ViewModels.PDFViewModel.GeneratePDFReport", ex);
            }

            return reportList;
        }
    }
}


