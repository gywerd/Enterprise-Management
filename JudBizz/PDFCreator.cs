using iTextSharp.text;
using iTextSharp.text.pdf;
using itextsharp.pdfa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JudBizz
{
    public class PdfCreator
    {
        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public PdfCreator() { }

        #endregion

        #region Methods
        // Method to add single cell to the header
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 12, 1, iTextSharp.text.BaseColor.WHITE))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(0, 51, 102) });
        }



        // Method to add single cell to the body
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 12, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
        }

        /// <summary>
        /// Method, that adds content to the EnterprisList PDF
        /// </summary>
        /// <param name="tableLayout">PdfPTable</param>
        /// <param name="project">Project</param>
        /// <param name="list">List<IndexableEnterprise></param>
        /// <param name="users">List<User></param>
        /// <returns>PdfPTable</returns>
        private PdfPTable AddContentToEnterpriseListPDF(PdfPTable tableLayout, Project project, List<IndexableEnterprise> list, List<User> users)
        {
            float[] headers = { 30, 35, 35 };  //Header Widths
            tableLayout.SetWidths(headers);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            DateTime today = DateTime.Today;
            string date = @"Udskrift pr.: " + today.ToShortDateString();
            string executive = @"Tilbudsansvarlig: " + GetExecutiveInitials(project.Executive, users);

            //Add Title to the PDF file at the top
            tableLayout.AddCell(new PdfPCell(new Phrase("Jorton A/S", new Font(Font.FontFamily.HELVETICA, 24, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Entrepriseliste", new Font(Font.FontFamily.HELVETICA, 24, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(date, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Sagsnr: " + project.CaseId, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(project.Name, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(executive, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_BOTTOM });

            //Add header
            AddCellToHeader(tableLayout, "Entreprise");
            AddCellToHeader(tableLayout, "Beskrivelse");
            AddCellToHeader(tableLayout, "Tilbudsliste");

            //Add body
            foreach (IndexableEnterprise temp in list)
            {
                if (temp.Project == project.Id)
                {
                    AddCellToBody(tableLayout, temp.Name);
                    AddCellToBody(tableLayout, temp.Elaboration);
                    AddCellToBody(tableLayout, temp.OfferList);
                }
            }

            return tableLayout;

        }

        /// <summary>
        /// Method, that adds content to the EnterprisList PDF
        /// </summary>
        /// <param name="tableLayout">PdfPTable</param>
        /// <param name="project">Project</param>
        /// <param name="enterpriseList">List<IndexableEnterprise></param>
        /// <param name="users">List<User></param>
        /// <returns>PdfPTable</returns>
        private PdfPTable AddContentToSubEntrepeneursPDF(PdfPTable tableLayout, Project project, List<IndexableEnterprise> enterpriseList, List<IndexableSubEntrepeneur> entrepeneurList, List<User> users)
        {
            float[] headers = { 22, 19, 9, 9, 11, 9, 7, 7, 7 };  //Header Widths
            tableLayout.SetWidths(headers);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            DateTime today = DateTime.Today;
            string date = @"Udskrift pr.: " + today.ToShortDateString();
            string executive = @"Tilbudsansvarlig: " + GetExecutiveInitials(project.Executive, users);

            //Add Title to the PDF file at the top
            tableLayout.AddCell(new PdfPCell(new Phrase("Jorton A/S", new Font(Font.FontFamily.HELVETICA, 24, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Fagentrepenører", new Font(Font.FontFamily.HELVETICA, 24, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(date, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Sagsnr: " + project.CaseId, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(project.Name, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(executive, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(153, 51, 0)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_BOTTOM });

            //Add header
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "Kontaktperson");
            AddCellToHeader(tableLayout, "Tlf.");
            AddCellToHeader(tableLayout, "Mobil");
            AddCellToHeader(tableLayout, "Email");
            AddCellToHeader(tableLayout, "Afgiver tilbud");
            AddCellToHeader(tableLayout, "Vedståelse");
            AddCellToHeader(tableLayout, "Forbehold");
            AddCellToHeader(tableLayout, "Tilbudspris");

            //Add body
            foreach (IndexableEnterprise temp in enterpriseList)
            {
                if (temp.Project == project.Id)
                {
                    AddCellToBody(tableLayout, temp.Name);
                    AddCellToBody(tableLayout, "");
                    AddCellToBody(tableLayout, "");
                    AddCellToBody(tableLayout, "");
                    AddCellToBody(tableLayout, "");
                    AddCellToBody(tableLayout, "");
                    AddCellToBody(tableLayout, "");
                    AddCellToBody(tableLayout, "");
                    AddCellToBody(tableLayout, "");
                    foreach (IndexableSubEntrepeneur tempSub in entrepeneurList)
                    {
                        if (tempSub.EnterpriseList == temp.Id)
                        {
                            Contact contact = GetContact(tempSub.Contact);
                            ContactInfo info = GetContactInfo(contact.ContactInfo);
                            string uphold = "Nej";
                            if (tempSub.Uphold)
                            {
                                uphold = "Ja";
                            }
                            string reservations = "Nej";
                            if (tempSub.Uphold)
                            {
                                reservations = "Ja";
                            }
                            AddCellToBody(tableLayout, tempSub.Name);
                            AddCellToBody(tableLayout, contact.Name);
                            AddCellToBody(tableLayout, info.Phone);
                            AddCellToBody(tableLayout, info.Mobile);
                            AddCellToBody(tableLayout, info.Email);
                            AddCellToBody(tableLayout, Convert.ToString(GetRequestStatus(tempSub.Request)));
                            AddCellToBody(tableLayout, uphold);
                            AddCellToBody(tableLayout, reservations);
                            AddCellToBody(tableLayout, Convert.ToString(GetRequestStatus(tempSub.Request)));
                        }
                    }
                }
            }

            return tableLayout;
        }

        private string GetRequestStatus(int request)
        {
            throw new NotImplementedException();
        }

        public string GenerateEnterpriseListPdf(Project tempProject, List<IndexableEnterprise> list, List<User> users)
        {
            DateTime today = DateTime.Today;
            string date = today.ToShortDateString();
            string path = @"PDF_Documents\Projekt_" + tempProject.CaseId.ToString() + "_" + date + ".pdf";
            
            //Create document
            Document document = new Document(PageSize.A4.Rotate(), 48, 48, 48, 48);

            //Create PDF Table
            PdfPTable tableLayout = new PdfPTable(3);

            //Create a PDF file in specific path
            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));

            //Open the PDF document
            document.Open();

            //Add Content to PDF
            document.Add(AddContentToEnterpriseListPDF(tableLayout, tempProject, list, users));

            // Closing the document
            document.Close();

            return path;

        }

        public string GenerateSubEntrepeneursPdf(Project tempProject, List<IndexableEnterprise> enterpriseList, List<IndexableSubEntrepeneur> entrepeneurList, List<User> users)
        {
            DateTime today = DateTime.Today;
            string date = today.ToShortDateString();
            string path = @"PDF_Documents\Projekt_" + tempProject.CaseId.ToString() + "_" + date + ".pdf";

            //Create document
            Document document = new Document(PageSize.A4.Rotate(), 48, 48, 48, 48);

            //Create PDF Table
            PdfPTable tableLayout = new PdfPTable(3);

            //Create a PDF file in specific path
            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));

            //Open the PDF document
            document.Open();

            //Add Content to PDF
            document.Add(AddContentToSubEntrepeneursPDF(tableLayout, tempProject, enterpriseList, entrepeneurList, users));

            // Closing the document
            document.Close();

            return path;

        }

        private ContactInfo GetContactInfo(int id)
        {
            throw new NotImplementedException();
        }

        private Contact GetContact(int contact)
        {
            throw new NotImplementedException();
        }

        private string GetExecutiveInitials(int id, List<User> users)
        {
            string result = "";
            foreach (User user in users)
            {
                if (user.Id == id)
                {
                    result = user.Initials;
                    return result;
                }
            }
            return result;
        }

        #endregion

    }
}
