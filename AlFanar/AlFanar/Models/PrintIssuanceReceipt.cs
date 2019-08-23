using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using Services.Helper;

namespace AJC.KMS.Models
{
    public class PrintIssuanceReceipt
    {
        PrintDocument pdoc = null;
        List<Services.SalesItems.SalesItems> obj;
        int nTransactionNo;
        DateTime mIssueDate;
        public void PrintReceipt(List<Services.SalesItems.SalesItems> SalesItem,int TransactionNo,DateTime IssueDate)
        {
            obj = SalesItem;
            nTransactionNo = TransactionNo;
            mIssueDate = IssueDate;
            pdoc = new PrintDocument();
            
          //  PrinterSettings ps = new PrinterSettings();
            //Font font = new Font("Courier New", 15);


           // PaperSize psize = new PaperSize("Custom", 100, 200);
           // ps.DefaultPageSettings.PaperSize = psize;

            //  pdoc.DefaultPageSettings.PaperSize.Height = 820;

            //  pdoc.DefaultPageSettings.PaperSize.Width = 520;

            pdoc.DocumentName = "Print Issuance Receipt";
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            try
            {
                pdoc.Print();
            }

            catch (Exception e)
                {

               }
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {

            //print header
            //graphics.DrawString("FERREIRA MATERIALS PARA CONSTRUCAO LTDA", bold, Brushes.Black, 20, 10);
            // graphics.DrawString("EST ENGENHEIRO MARCILAC, 116, SAO PAOLO - SP", regular, Brushes.Black, 30, 30);
            // graphics.DrawString("Telefone: (11)5921-3826", regular, Brushes.Black, 110, 50);
            // graphics.DrawLine(Pens.Black, 80, 70, 320, 70);
            // graphics.DrawString("CUPOM NAO FISCAL", bold, Brushes.Black, 110, 80);
            /// graphics.DrawLine(Pens.Black, 80, 100, 320, 100);

            //  string filename=@"/Content/Images/";

            
            Graphics graphics = e.Graphics;

            Font font = new Font("Courier New", 10);

            Font regular = new Font(FontFamily.GenericSansSerif, 8.0f, FontStyle.Regular);
            Font bold = new Font(FontFamily.GenericSansSerif, 8.0f, FontStyle.Bold);

            float fontHeight = font.GetHeight();

           // int startX = 50;
           // int startY = 150;
            // int Offset = 40;
           // int Offset = 40;

           // graphics.DrawString("Palace Kitchen", new Font("Courier New", 14),
           //                   new SolidBrush(Color.Black), startX, startY);



            //print header
            graphics.DrawString( "AL JABER ", bold, Brushes.Black, 20, 10 );
            graphics.DrawString( "PALACE KITCHEN", regular, Brushes.Black, 20, 30 );
            graphics.DrawString( "Transaction No:" + nTransactionNo, regular, Brushes.Black, 20, 50 );
            graphics.DrawString( "Date:" + mIssueDate, regular, Brushes.Black, 175, 50 );

            graphics.DrawLine( Pens.Black, 80, 70, 320, 70 );
            graphics.DrawString( "Issued To:" + obj[0].ChiefName, bold, Brushes.Black, 110, 80 );
            graphics.DrawLine( Pens.Black, 80, 100, 320, 100 );
           

            graphics.DrawString( "S.No | ITEM                      | QTY | X |  Unit |  Total |", bold, Brushes.Black, 10, 120 );
            graphics.DrawLine( Pens.Black, 10, 140, 430, 140 );
            Double lTotal;
            var sno=0;
            for ( int i = 0; i < obj.Count; i++ )
        {
                sno = Convert.ToInt16(i) + 1;
                lTotal = Convert.ToDouble(obj[i].Quantity) * Convert.ToDouble(obj[i].Rate);
               graphics.DrawString(sno.ToString(), regular, Brushes.Black, 10, 150 + i * 20);
                graphics.DrawString(obj[i].ItemName.ToString(), regular, Brushes.Black, 20, 150 + i * 20 );
               graphics.DrawString(obj[i].Quantity.ToString(), regular, Brushes.Black, 160, 150 + i * 20);
               graphics.DrawString(obj[i].Rate.ToString(), regular, Brushes.Black, 180, 150 + i * 20);
               graphics.DrawString(obj[i].Unit.ToString(), regular, Brushes.Black, 210, 150 + i * 20);
               graphics.DrawString(lTotal.ToString(), regular, Brushes.Black, 250, 150 + i * 20);
                lTotal = 0;
                //      string productDescription = obj[i].ItemName;
                //   string productQty = obj[i].Quantity.ToString();
                //   string productPrice = obj[i].Rate.ToString();
                // string ProductLine = productDescription + productQty + productPrice;


                // graphics.DrawString(ProductLine, font, new SolidBrush(Color.Black), 20, startY + Offset);/

                // Offset = Offset + 20;


            }

            //graphics.DrawLine(Pens.Black, 300, 400, 520, 600);
            //graphics.DrawString("Total Amount to pay :300 ",font, new SolidBrush(Color.lBlack), startX, startY + Offset);


            // foreach (Services.SalesItems.SalesItems item in obj)
            // { 
            //   string productDescription = item.ItemName.PadRight(10);
            //  string productQty = item.Quantity.ToString().PadRight(5);
            //   string productPrice = item.Rate.ToString();
            //  string ProductLine = productDescription + productQty + productPrice;

            //   graphics.DrawString(ProductLine, font, new SolidBrush(Color.Black), startX, startY + Offset);

            //  Offset = Offset + 20;
            // String underLine = "------------------------------------------";
            // graphics.DrawString(underLine, new Font("Courier New", 10),
            //        new SolidBrush(Color.Black), startX, startY + Offset);

            //                Offset = Offset + (int)fontHeight+5; 
            //          }



            //        Offset = Offset + 20;
            //      graphics.DrawString("Total Amount to pay :300 ",font, new SolidBrush(Color.lBlack), startX, startY + Offset);
            //Bitmap bmpPicture = new Bitmap("Print.bmp");
           // graphics.DrawImage(bmpPicture,400,60);



            //Image img = Image.FromFile("C:\a.jpg");
            //Font myFont = new Font("Arial", 16);
            //SolidBrush myBrush = new SolidBrush(Color.Black);

            //Graphics gfx = Graphics.FromImage(img);
            //gfx.DrawString("Some Data", myFont, myBrush, 100.0F, 30.0F);

            //Bitmap newBit = new Bitmap(440, 60, gfx);

          




        }

    }
}