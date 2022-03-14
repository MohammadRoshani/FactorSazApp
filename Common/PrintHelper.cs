// ***********************************************************************
// Assembly         : FactorSazApp
// Author           : M.Roshani
// Created          : 12-16-2019
//
// Last Modified By : M.Roshani
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="PrintHelper.cs" company="WaybillApp">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WaybillApp.Common
{
    using System.Printing;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Markup;
    using System.Windows.Media;

    using WaybillApp.View;

    /// <summary>
    ///     Class PrintHelper.
    /// </summary>
    public static class PrintHelper
    {
        /// <summary>
        /// Gets the fixed document.
        /// </summary>
        /// <param name="toPrint">To print.</param>
        /// <param name="printDialog">The print dialog.</param>
        /// <returns>Fixed Document.</returns>
        public static FixedDocument GetFixedDocument(FrameworkElement toPrint, PrintDialog printDialog)
        {
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            var capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            var pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            var visibleSize = new Size(
                capabilities.PageImageableArea?.ExtentWidth ?? 0,
                capabilities.PageImageableArea?.ExtentHeight ?? 0);
            var fixedDoc = new FixedDocument();

            // If the toPrint visual is not displayed on screen we neeed to measure and arrange it
            toPrint.Measure(visibleSize);

            // toPrint.Arrange(new Rect(new Point(0, 0), toPrint.DesiredSize));
            var size = toPrint.DesiredSize;

            // Will assume for simplicity the control fits horizontally on the page
            double yOffset = 0;
            while (yOffset < size.Height)
            {
                var vb = new VisualBrush(toPrint)
                             {
                                 Stretch = Stretch.None,
                                 AlignmentX = AlignmentX.Left,
                                 AlignmentY = AlignmentY.Top,
                                 ViewboxUnits = BrushMappingMode.Absolute,
                                 TileMode = TileMode.None,
                                 Viewbox = new Rect(0, yOffset, visibleSize.Width, visibleSize.Height)
                             };
                var pageContent = new PageContent();
                var page = new FixedPage();
                ((IAddChild)pageContent).AddChild(page);
                fixedDoc.Pages.Add(pageContent);
                page.Width = pageSize.Width;
                page.Height = pageSize.Height;
                var canvas = new Canvas();
                FixedPage.SetLeft(canvas, capabilities.PageImageableArea?.OriginWidth ?? 0);
                FixedPage.SetTop(canvas, capabilities.PageImageableArea?.OriginHeight ?? 0);
                canvas.Width = visibleSize.Width;
                canvas.Height = visibleSize.Height;
                canvas.Background = vb;
                page.Children.Add(canvas);
                yOffset += visibleSize.Height;
            }

            return fixedDoc;
        }

        /// <summary>
        /// Gets the fixed document2.
        /// </summary>
        /// <param name="toPrint">To print.</param>
        /// <param name="printDialog">The print dialog.</param>
        /// <returns>Fixed Document.</returns>
        public static FixedDocument GetFixedDocument2(FrameworkElement toPrint, PrintDialog printDialog)
        {
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            var capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);

            // var pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            var visibleSize = new Size(
                capabilities.PageImageableArea?.ExtentWidth ?? 0,
                capabilities.PageImageableArea?.ExtentHeight ?? 0);
            var fixedDoc = new FixedDocument();

            // If the toPrint visual is not displayed on screen we need to measure and arrange it
            toPrint.Measure(visibleSize);
            var pageSize = new Size(8.5 * 96.0, 5.8 * 96.0);

            // toPrint.Arrange(new Rect(new Point(0, 0), toPrint.DesiredSize));
            var size = toPrint.DesiredSize;

            // Will assume for simplicity the control fits horizontally on the page
            double yOffset = 0;
            while (yOffset < size.Height)
            {
                var vb = new VisualBrush(toPrint)
                             {
                                 Stretch = Stretch.None,
                                 AlignmentX = AlignmentX.Left,
                                 AlignmentY = AlignmentY.Top,
                                 ViewboxUnits = BrushMappingMode.Absolute,
                                 TileMode = TileMode.None,
                                 Viewbox = new Rect(0, yOffset, pageSize.Width, pageSize.Height)
                             };
                var pageContent = new PageContent();
                var page = new FixedPage();
                ((IAddChild)pageContent).AddChild(page);
                fixedDoc.Pages.Add(pageContent);
                page.Width = 5.8 * 96;
                page.Height = 8.27 * 96;
                var canvas = new Canvas();
                FixedPage.SetLeft(canvas, capabilities.PageImageableArea?.OriginWidth ?? 0);
                FixedPage.SetTop(canvas, capabilities.PageImageableArea?.OriginHeight ?? 0);
                canvas.Width = visibleSize.Width;
                canvas.Height = visibleSize.Height;
                canvas.Background = vb;
                page.Children.Add(canvas);
                yOffset += visibleSize.Height;
            }

            return fixedDoc;
        }

        /// <summary>
        /// Gets the fixed document3.
        /// </summary>
        /// <param name="toPrint">To print.</param>
        /// <param name="printDialog">The print dialog.</param>
        /// <returns>Fixed Document.</returns>
        public static FixedDocument GetFixedDocument3(FrameworkElement toPrint, PrintDialog printDialog)
        {
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            var fixedDoc = new FixedDocument();
            var pageContent = new PageContent();
            var fixedPage = new FixedPage { Height = 5.8 * 96, Width = 8.27 * 96 };
            var pageSize = new Size(8.5 * 96.0, 5.8 * 96.0);

            toPrint.UpdateLayout();
            toPrint.Height = pageSize.Height;
            toPrint.Width = pageSize.Width;
            toPrint.UpdateLayout();
            fixedPage.Children.Add(toPrint);
            ((IAddChild)pageContent).AddChild(fixedPage);
            fixedDoc.Pages.Add(pageContent);
            return fixedDoc;
        }

        /// <summary>
        ///     Prints the no preview.
        /// </summary>
        /// <param name="printDialog">The print dialog.</param>
        /// <param name="fixedDoc">The fixed document.</param>
        public static void PrintNoPreview(PrintDialog printDialog, FixedDocument fixedDoc)
        {
            printDialog.PrintDocument(fixedDoc.DocumentPaginator, "Test Print No Preview");
        }

        /// <summary>
        ///     Shows the print preview.
        /// </summary>
        /// <param name="fixedDoc">The fixed document.</param>
        public static void ShowPrintPreview(FixedDocument fixedDoc)
        {
            new PrintWindow { DataContext = fixedDoc }.ShowDialog();
        }
    }
}