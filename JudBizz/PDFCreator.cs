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
        #region Fields
        Bizz Bizz = new Bizz();

        #endregion

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
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 10, 1, iTextSharp.text.BaseColor.WHITE))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(0, 104, 64) });
        }

        // Method to add single cell to the body - aligned left
        private static void AddCellToBodyLeft(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 10, 0, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
        }

        // Method to add single cell to the body - aligned right
        private static void AddCellToBodyRight(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 10, 0, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
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
            tableLayout.AddCell(new PdfPCell(new Phrase("Jorton A/S", new Font(Font.FontFamily.HELVETICA, 24, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Entrepriseliste", new Font(Font.FontFamily.HELVETICA, 24, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(date, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Sagsnr: " + project.CaseId, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(project.Name, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(executive, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_BOTTOM });

            //Add header
            AddCellToHeader(tableLayout, "Entreprise");
            AddCellToHeader(tableLayout, "Beskrivelse");
            AddCellToHeader(tableLayout, "Tilbudsliste");

            //Add body
            foreach (IndexableEnterprise temp in list)
            {
                if (temp.Project == project.Id)
                {
                    AddCellToBodyLeft(tableLayout, temp.Name);
                    AddCellToBodyLeft(tableLayout, temp.Elaboration);
                    AddCellToBodyLeft(tableLayout, temp.OfferList);
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
            float[] headers = { 20, 20, 10, 10, 13, 7, 6, 6, 8 };  //Header Widths
            tableLayout.SetWidths(headers);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            DateTime today = DateTime.Today;
            string date = @"Udskrift pr.: " + today.ToShortDateString();
            string executive = @"Tilbudsansvarlig: " + GetExecutiveInitials(project.Executive, users);

            //Add Title to the PDF file at the top
            tableLayout.AddCell(new PdfPCell(new Phrase("Jorton A/S", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Fagentrepenører", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 2, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 2, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(date, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 3, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Sagsnr: " + project.CaseId, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(project.Name, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 2, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 2, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(executive, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 3, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });

            //Add header
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "Kontaktperson");
            AddCellToHeader(tableLayout, "Tlf.");
            AddCellToHeader(tableLayout, "Mobil");
            AddCellToHeader(tableLayout, "Email");
            AddCellToHeader(tableLayout, "Afgiver tilbud");
            AddCellToHeader(tableLayout, "Vedstå");
            AddCellToHeader(tableLayout, "Forbe.");
            AddCellToHeader(tableLayout, "Pris");

            //Add body
            foreach (IndexableEnterprise temp in enterpriseList)
            {
                if (temp.Project == project.Id)
                {
                    tableLayout.AddCell(new PdfPCell(new Phrase(temp.Name, new Font(Font.FontFamily.HELVETICA, 10, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
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
                            AddCellToBodyLeft(tableLayout, tempSub.Name);
                            AddCellToBodyLeft(tableLayout, contact.Name);
                            AddCellToBodyRight(tableLayout, info.Phone);
                            AddCellToBodyRight(tableLayout, info.Mobile);
                            AddCellToBodyLeft(tableLayout, info.Email);
                            AddCellToBodyLeft(tableLayout, GetOfferStatus(tempSub));
                            AddCellToBodyLeft(tableLayout, uphold);
                            AddCellToBodyLeft(tableLayout, reservations);
                            AddCellToBodyRight(tableLayout, GetOfferPrice(tempSub.Offer));
                        }
                    }
                }
            }

            return tableLayout;
        }

        private PdfPTable AddContentToSubEntrepeneursPDFForAgreement(PdfPTable tableLayout, Project project, List<IndexableEnterprise> enterpriseList, List<IndexableSubEntrepeneur> entrepeneurList, List<User> users)
        {
            float[] headers = { 20, 20, 20, 20, 6, 6, 8 };  //Header Widths
            tableLayout.SetWidths(headers);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            DateTime today = DateTime.Today;
            string date = @"Udskrift pr.: " + today.ToShortDateString();
            string executive = @"Tilbudsansvarlig: " + GetExecutiveInitials(project.Executive, users);

            //Add Title to the PDF file at the top
            tableLayout.AddCell(new PdfPCell(new Phrase("Jorton A/S", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Fagentrepenører", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(date, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 3, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("Sagsnr: " + project.CaseId, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(project.Name, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 18, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 1, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });
            tableLayout.AddCell(new PdfPCell(new Phrase(executive, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(24, 80, 116)))) { Colspan = 3, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_BOTTOM });

            //Add header
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "Kontaktperson");
            AddCellToHeader(tableLayout, "Kontaktoplysninger");
            AddCellToHeader(tableLayout, "Tilbudbrev");
            AddCellToHeader(tableLayout, "Vedstå");
            AddCellToHeader(tableLayout, "Forbe.");
            AddCellToHeader(tableLayout, "Pris");

            //Add body
            foreach (IndexableEnterprise temp in enterpriseList)
            {
                if (temp.Project == project.Id)
                {
                    tableLayout.AddCell(new PdfPCell(new Phrase(temp.Name, new Font(Font.FontFamily.HELVETICA, 10, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    foreach (IndexableSubEntrepeneur tempSub in entrepeneurList)
                    {
                        if (tempSub.EnterpriseList == temp.Id)
                        {
                            Contact contact = GetContact(tempSub.Contact);
                            ContactInfo info = GetContactInfo(contact.ContactInfo);
                            String ittletterSentDate = "Sendt: " + GetIttLetterSentDate(tempSub.IttLetter);
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
                            AddCellToBodyLeft(tableLayout, tempSub.Name);
                            AddCellToBodyLeft(tableLayout, contact.Name);
                            AddCellToBodyRight(tableLayout, info.ToLongString());
                            AddCellToBodyLeft(tableLayout, ittletterSentDate);
                            AddCellToBodyLeft(tableLayout, uphold);
                            AddCellToBodyLeft(tableLayout, reservations);
                            AddCellToBodyRight(tableLayout, GetOfferPrice(tempSub.Offer));
                        }
                    }
                }
            }

            return tableLayout;
        }

        public string GenerateEnterpriseListPdf(Project tempProject, List<IndexableEnterprise> list, List<User> users)
        {
            DateTime today = DateTime.Today;
            string date = today.ToShortDateString();
            string path = @"PDF_Documents\Projekt_" + tempProject.CaseId.ToString() + "_Entrepriseliste_" + date + ".pdf";
            
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

        public string GenerateSubEntrepeneursPdf(Bizz bizz, Project tempProject, List<IndexableEnterprise> enterpriseList, List<IndexableSubEntrepeneur> entrepeneurList, List<User> users)
        {
            this.Bizz = bizz;
            DateTime today = DateTime.Today;
            string date = today.ToShortDateString();
            string path = @"PDF_Documents\Projekt" + tempProject.CaseId.ToString() + "_Underentrenoerer_" + date + ".pdf";

            //Create document
            Document document = new Document(PageSize.A4.Rotate(), 48, 48, 48, 48);

            //Create PDF Table
            PdfPTable tableLayout = new PdfPTable(9);

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

        public string GenerateSubEntrepeneursPdfForAgreement(Bizz bizz, Project tempProject, List<IndexableEnterprise> enterpriseList, List<IndexableSubEntrepeneur> entrepeneurList, List<User> users)
        {
            this.Bizz = bizz;
            DateTime today = DateTime.Today;
            string date = today.ToShortDateString();
            string path = @"PDF_Documents\Projekt_" + tempProject.CaseId.ToString() + "_Underentrenoerer_" + date + ".pdf";

            //Create document
            Document document = new Document(PageSize.A4.Rotate(), 48, 48, 48, 48);

            //Create PDF Table
            PdfPTable tableLayout = new PdfPTable(7);

            //Create a PDF file in specific path
            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));

            //Open the PDF document
            document.Open();

            //Add Content to PDF
            document.Add(AddContentToSubEntrepeneursPDFForAgreement(tableLayout, tempProject, enterpriseList, entrepeneurList, users));

            // Closing the document
            document.Close();

            return path;

        }

        private Contact GetContact(int id)
        {
            foreach (Contact contact in Bizz.Contacts)
            {
                if (contact.Id == id)
                {
                    return contact;
                }
            }
            return new Contact(Bizz.strConnection);
        }

        private ContactInfo GetContactInfo(int id)
        {
            foreach (ContactInfo info in Bizz.ContactInfoList)
            {
                if (info.Id == id)
                {
                    return info;
                }
            }
            return new ContactInfo(Bizz.strConnection);
        }

        private string GetExecutiveInitials(int id, List<User> users)
        {
            foreach (User user in users)
            {
                if (user.Id == id)
                {
                    return user.Initials;
                }
            }
            return "";
        }

        private string GetIttLetterSentDate(int id)
        {
            string result = "";
            foreach (IttLetter letter in Bizz.IttLetters)
            {
                if (letter.Id == id)
                {
                    result = letter.SentDate.ToLongDateString();
                }
            }
            if (result == "31. december 1899")
            {
                result = "ukendt";
            }
            return result;
        }

        private string GetOfferPrice(int id)
        {
            string result = "";
            foreach (Offer offer in Bizz.Offers)
            {

                if (id == offer.Id)
                {
                    result = Convert.ToString(offer.Price);
                }
            }
            return result;
        }

        private string GetOfferStatus(SubEntrepeneur sub)
        {
            string result = "Ukendt";
            foreach (Offer offer in Bizz.Offers)
            {
                if (sub.Offer == offer.Id && !offer.Received && !offer.Chosen)
                {
                    foreach (IttLetter letter in Bizz.IttLetters)
                    {
                        result = "Nej";
                        if (letter.Id == sub.IttLetter && letter.Sent)
                        {
                            result = "Sendt";
                        }
                    }
                }
                if (sub.Offer == offer.Id && offer.Received && !offer.Chosen)
                {
                    result = "Modtaget";
                }
                if (sub.Offer == offer.Id && offer.Chosen)
                {
                    result = "Valgt";
                }
            }
            return result;
        }

        #endregion
    }
}
