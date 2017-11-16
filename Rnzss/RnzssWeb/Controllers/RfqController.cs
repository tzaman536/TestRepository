using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using RnzssWeb.Models;
using log4net;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Data.SqlClient;
using System.Data;

namespace RnzssWeb.Controllers
{
    public class RfqController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Common
        public string GetFilePath(string uploadType, string filename)
        {
            string mapPath = Server.MapPath("~/");
            switch (uploadType)
            {
                case "SOLICITATION":
                    return System.IO.Path.Combine(mapPath, "UploadedFiles", "Solicitations", filename);

                case "PRODUCT":
                    break;
                case "RFQ_DOCUMENT":
                    return System.IO.Path.Combine(mapPath, "UploadedFiles", "RfqDocuments", filename);
                case "DOWNLOAD":
                    return System.IO.Path.Combine(mapPath, "DownloadedFiles", filename);
                default:
                    return System.IO.Path.Combine(mapPath, filename);
            }

            return System.IO.Path.Combine(mapPath, filename);
        }

        public string GetContentType(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".xlsx":
                    return "application/vnd.ms-excel";
                case ".pdf":
                    return "Content-Type: application/pdf";
                case ".doc":
                    return "Content-Type: application/vnd.ms-word";
                case ".jpg":
                    return "Content-Type: image/jpeg";
                default:
                    return "Content-Type: application/pdf";
            }


        }

        [HttpGet]
        public ActionResult DownloadDocument(int DocumentStoreId)
        {

            try
            {
                var dc = DocumentStore.GetDocument(DocumentStoreId);

                if (dc == null)
                    return null;

                string filePath = dc.FileBaseName;
                string mapPath = GetFilePath("DOWNLOAD", filePath);
                byte[] fileContents;
                string selectStmt = "SELECT BinaryData FROM rnz.DocumentStore WHERE DocumentStoreId = @DocumentStoreId";
                using (SqlConnection connection = new SqlConnection(CommonMethods._connectionString))
                using (SqlCommand cmdSelect = new SqlCommand(selectStmt, connection))
                {
                    cmdSelect.Parameters.Add("@DocumentStoreId", SqlDbType.Int).Value = DocumentStoreId;
                    connection.Open();
                    fileContents = (byte[])cmdSelect.ExecuteScalar();
                    connection.Close();
                }

                if (System.IO.File.Exists(mapPath))
                {
                    System.IO.File.Delete(mapPath);
                }



                if (fileContents != null)
                {
                    using (Stream file = System.IO.File.OpenWrite(mapPath))
                    {
                        file.Write(fileContents, 0, fileContents.Length);
                    }
                }

                string contectType = GetContentType(dc.FileExtension);

                return File(mapPath, contectType, System.IO.Path.GetFileName(mapPath));
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
            return null;
        }


        public ActionResult GetInstrumentName([DataSourceRequest]DataSourceRequest request, string sessionId)
        {
            string result = "Can't retrieve instrument name";
            try
            {
                result = "Hello From Server";
            }
            catch (Exception ex)
            {
                logger.Fatal(ex.Message);
            }

            return Json(new { success = true, message = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region RFQ
        // GET: Rfq
        public ActionResult RfqEntry(string RFQNo = null, string SolicitationNo= null)
        {
            HttpSessionState ss = System.Web.HttpContext.Current.Session;
            ViewBag.SessionId = ss.SessionID;
            if (ViewData["CurrentId"] == null)
            {
                ViewData["CurrentId"] = ss.SessionID;
            }

            if (!string.IsNullOrEmpty(RFQNo))
            {
                ViewData["RFQNo"] = RFQNo;
            }
            else
            {
                ViewData["RFQNo"] = "UNKNOWN";
            }

            logger.InfoFormat("RfqEntry called");

            return View();
        }



        public ActionResult ActiveRfq()
        {
            HttpSessionState ss = System.Web.HttpContext.Current.Session;


            ViewBag.SessionId = ss.SessionID;
            if (ViewData["CurrentId"] == null)
            {
                ViewData["CurrentId"] = ss.SessionID;
            }
            return View();
        }

        public ActionResult RfqEvent(string RFQNo = null)
        {
            if (!string.IsNullOrEmpty(RFQNo))
            {
                ViewBag.RfqNo = RFQNo;
            }
            else
            {
                ViewBag.RfqNo = "UNKNOWN";
            }


            return View();
        }

        public ActionResult AddAnotherProduct(RequestForQuote rfq)
        {
            string message = "Product added";
            rfq.UpdatedBy = Environment.UserName;
            if (rfq.Product.PartNumber != null)
            {
                if (!string.IsNullOrEmpty(rfq.RFQNo))
                {
                    int productCount = ProductInformation.GetAll(rfq.RFQNo).Count();
                    if (productCount == 4)
                    {
                        message = "We can only add four products to an RFQ. Please create a new RFQ.";
                        return Json(new { success = false, UserMessage = new UserMessage() { RfqNo = rfq.RFQNo, Message = message }, JsonRequestBehavior.AllowGet });
                    }

                    rfq.Product.RFQNo = rfq.RFQNo;
                    ProductInformation.Upsert(rfq.Product);
                }
                else
                {
                    message = "Please save RFQ first";
                    return Json(new { success = false, UserMessage = new UserMessage() { RfqNo = rfq.RFQNo, Message = message }, JsonRequestBehavior.AllowGet });

                }

            }


            return Json(new { success = true, UserMessage = new UserMessage() { RfqNo = rfq.RFQNo, Message = message }, JsonRequestBehavior.AllowGet });
        }

        public ActionResult SubmitRfqData(RequestForQuote rfq)
        {

            string message = "";
            RequestForQuote.Upsert(ref rfq);
            if (rfq.Product.PartNumber != null)
            {
                if (!string.IsNullOrEmpty(rfq.RFQNo))
                {
                    int productCount = ProductInformation.GetAll(rfq.RFQNo).Count();
                    if (productCount == 4)
                    {
                        message = "We can only add four products to an RFQ. Please create a new RFQ.";
                        return Json(new { success = false, UserMessage = new UserMessage() { RfqNo = rfq.RFQNo, Message = message }, JsonRequestBehavior.AllowGet });
                    }
                    rfq.Product.RFQNo = rfq.RFQNo;
                    ProductInformation.Upsert(rfq.Product);
                    message = "RFQ Saved.";
                    ViewData["RFQNo"] = rfq.RFQNo;
                }
            }
            else
            {
                message = "Company information saved. Please add a product.";
            }
            // Do my stuff here with my parameter
            return Json(new { success = true, UserMessage = new UserMessage() { RfqNo = rfq.RFQNo, Message = message }, JsonRequestBehavior.AllowGet });
        }

        public ActionResult FindRfq(RequestForQuote rfq)
        {
            rfq.Reset();
            rfq.CompanyName = "hello";

            rfq = RequestForQuote.GetRfq(rfq.RFQNo);
            if (rfq == null)
            {
                rfq = new RequestForQuote();
            }

            // Do my stuff here with my parameter
            return Json(new { success = true, RFQ = rfq, JsonRequestBehavior.AllowGet });
        }


        #region RFQ Info Grid
        public ActionResult Rfq_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

                return new JsonResult()
                {
                    Data = RequestForQuote.GetAll().ToDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
                //return Json(result.OrderByDescending(x => x.Currency).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Rfq_Update([DataSourceRequest] DataSourceRequest request, RequestForQuote mo)
        {
            string message = "Update successful";
            if (mo != null && ModelState.IsValid)
            {
                RequestForQuote.Update(mo);
            }
            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };


            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Rfq_Destroy([DataSourceRequest] DataSourceRequest request, RequestForQuote mo)
        {
            string message = "Delete successful";
            if (mo != null)
            {
                RequestForQuote.Delete(mo);
            }
            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };
            return Json(result);
        }

        #endregion


        #region ProductInfo Grid
        public ActionResult RfqProduct_Read([DataSourceRequest] DataSourceRequest request, string rfqNo)
        {
            try
            {

                return new JsonResult()
                {
                    Data = ProductInformation.GetAll(rfqNo).ToDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
                //return Json(result.OrderByDescending(x => x.Currency).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RfqProduct_Update([DataSourceRequest] DataSourceRequest request, ProductInformation mo)
        {
            string message = "Update successful";
            if (mo != null && ModelState.IsValid)
            {
                ProductInformation.Update(mo);
            }
            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };


            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RfqProduct_Destroy([DataSourceRequest] DataSourceRequest request, ProductInformation mo)
        {
            string message = "Delete successful";
            if (mo != null)
            {
                ProductInformation.Delete(mo);
            }
            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };
            return Json(result);
        }

        #endregion

        #region Print RFQ
        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            //string fullPath = Path.Combine(Server.MapPath("~/RfqFiles"), "RFQ_Template.xlsx");

            return File(file, "application/vnd.ms-excel", System.IO.Path.GetFileName(file));
        }

        private static WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document,
           string sheetName)
        {
            IEnumerable<Sheet> sheets =
               document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
               Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets.Count() == 0)
            {
                // The specified worksheet does not exist.

                return null;
            }

            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)
                 document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;

        }
        private static Cell GetCell(Worksheet worksheet,
             string columnName, uint rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null)
                return null;

            return row.Elements<Cell>().Where(c => string.Compare
                   (c.CellReference.Value, columnName +
                   rowIndex, true) == 0).First();
        }


        // Given a worksheet and a row index, return the row.
        private static Row GetRow(Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().
              Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
        }


        public ActionResult PrintRFQ(RequestForQuote rfq)
        {
            try
            {
                if (string.IsNullOrEmpty(rfq.RFQNo))
                {
                    return Json(new { success = false, message = "Please Save the RFQ before clicking download.", JsonRequestBehavior.AllowGet });
                }

                string sourceFile = System.IO.Path.Combine(Server.MapPath("~/RfqFiles"), "RFQ_Template.xlsx");
                string destinationFile = System.IO.Path.Combine(Server.MapPath("~/RfqFiles"), string.Format("RFQ_{0}.xlsx", rfq.RFQNo));
                if (System.IO.File.Exists(destinationFile))
                    System.IO.File.Delete(destinationFile);


                System.IO.File.Copy(sourceFile, destinationFile);


                RequestForQuote dbRfq = RequestForQuote.GetRfq(rfq.RFQNo);
                IEnumerable<ProductInformation> dbProducts = ProductInformation.GetAll(rfq.RFQNo);

                using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(destinationFile, true))
                {
                    WorksheetPart worksheetPart =
                         GetWorksheetPartByName(spreadSheet, "Sheet1");

                    if (worksheetPart != null)
                    {
                        Cell cell = null;
                        #region Our company info
                        cell = GetCell(worksheetPart.Worksheet,
                                                 "F", 11);



                        cell.CellValue = new CellValue(string.Format("RFQ No. {0} \n # Tanweer Zaman \n # R & Z Supply And Services LLC. \n # 14 Fountain Ln \n # Jericho NY 11753", rfq.RFQNo).Replace("\n", Environment.NewLine).Replace("#", "   "));
                        cell.DataType =
                            new EnumValue<CellValues>(CellValues.String);

                        #endregion

                        #region RFQ Company Info
                        cell = GetCell(worksheetPart.Worksheet,
                                                 "B", 11);


                        // Do not indent or add space.It will add space during print
                        cell.CellValue = new CellValue(string.Format(@"{0}
{1}
{2}
{3}"
                                                                    , dbRfq.CompanyName
                                                                    , dbRfq.Attention
                                                                    , dbRfq.CompanyAddress
                                                                    , dbRfq.PhoneNo
                                                                    )
                                                      );
                        cell.DataType =
                            new EnumValue<CellValues>(CellValues.String);

                        #endregion

                        #region Print Products

                        if (dbProducts != null && dbProducts.Any())
                        {
                            int line = 1;
                            foreach (var p in dbProducts)
                            {
                                switch (line)
                                {
                                    #region Line 1
                                    case 1:
                                        cell = GetCell(worksheetPart.Worksheet, "C", 22);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.PartNumber));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "E", 22);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.CN));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "F", 22);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.Quantity));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "C", 24);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.PartDescription));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);
                                        break;
                                    #endregion

                                    #region Line 2
                                    case 2:
                                        cell = GetCell(worksheetPart.Worksheet, "C", 30);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.PartNumber));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "E", 30);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.CN));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "F", 30);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.Quantity));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "C", 32);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.PartDescription));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);
                                        break;
                                    #endregion

                                    #region Line 3
                                    case 3:
                                        cell = GetCell(worksheetPart.Worksheet, "C", 38);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.PartNumber));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "E", 38);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.CN));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "F", 38);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.Quantity));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "C", 40);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.PartDescription));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);
                                        break;
                                    #endregion

                                    #region Line 4
                                    case 4:
                                        cell = GetCell(worksheetPart.Worksheet, "C", 46);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.PartNumber));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "E", 46);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.CN));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "F", 46);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.Quantity));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                                        cell = GetCell(worksheetPart.Worksheet, "C", 48);
                                        cell.CellValue = new CellValue(string.Format("{0}", p.PartDescription));
                                        cell.DataType = new EnumValue<CellValues>(CellValues.String);
                                        break;

                                    #endregion

                                    default:
                                        break;
                                }

                                line++;
                            }

                        }
                        #endregion



                        // Save the worksheet.
                        worksheetPart.Worksheet.Save();

                    }
                }


                return Json(new { success = true, fileName = destinationFile, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                logger.Error("Failed to print RFQ:");
                logger.Fatal(ex);
                return Json(new { success = false, message = string.Format("Failed to print RFQ. {0} ", ex.Message), JsonRequestBehavior.AllowGet });
            }

        }

        #endregion


        #region Parse Address
        public void Parse(ref RequestForQuote rfq, string inputAddress)
        {

            inputAddress = inputAddress.Replace("Associated CAGE Code:", "").Replace("Replacement CAGE Code:", "");

            //var temp = inputAddress.Split("Phone");
            Match match = Regex.Match(inputAddress, @"^(.*?)Phone:", RegexOptions.None);
            if (match.Success)
                rfq.CompanyAddress = match.Value.Replace("Phone:", "");

            match = Regex.Match(inputAddress, @"(?<=Phone:)(.*)(?=Fax:)", RegexOptions.None);
            if (match.Success)
                rfq.PhoneNo = match.Value.Trim();

            match = Regex.Match(inputAddress, @"(?<=Fax:)(.*)(?=CAGE Code:)", RegexOptions.None);
            if (match.Success)
                rfq.FaxNo = match.Value.Trim();

            match = Regex.Match(inputAddress, @"(?<=Government POC Email:)(.*)(?=Size:)", RegexOptions.None);
            if (match.Success)
                rfq.Email = match.Value.Trim();

            match = Regex.Match(inputAddress, @"(?<=Government POC:)(.*)(?=SIC:)", RegexOptions.None);
            if (match.Success)
                rfq.Attention = match.Value.Trim();



        }

        public ActionResult ParseAddress(string inputAddress)
        {
            RequestForQuote rfq = new RequestForQuote();
            Parse(ref rfq, inputAddress);

            

            // Do my stuff here with my parameter
            //return Json(new { success = true, RFQ = rfq, JsonRequestBehavior.AllowGet });
            return Json(new { success = true, message = rfq }, JsonRequestBehavior.AllowGet);

        }

        public void ReadPdfFile()
        {
            PdfReader reader = new PdfReader(@"C:\Personal\R&Z\Documents\SPE7L118T1092.PDF");
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(reader, page);
            }
            reader.Close();
            Console.WriteLine(text);
        }

        #endregion

        #region Rfq Event
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RfqEvent_Create([DataSourceRequest] DataSourceRequest request, RequestForQuoteEvent mo, string rfqNo)
        {
            string message = "RFQ Event Created";
            if (mo != null && ModelState.IsValid)
            {
                RequestForQuoteEvent.Add(mo);
            }

            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };


            return Json(result);
        }


        public ActionResult RfqEvent_Read([DataSourceRequest] DataSourceRequest request, string rfqNo)
        {
            try
            {

                return new JsonResult()
                {
                    Data = RequestForQuoteEvent.GetRfqEvent(rfqNo).ToDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
                //return Json(result.OrderByDescending(x => x.Currency).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RfqEvent_Update([DataSourceRequest] DataSourceRequest request, RequestForQuoteEvent mo)
        {
            string message = "Update successful";
            if (mo != null && ModelState.IsValid)
            {
                RequestForQuoteEvent.Update(mo);
            }
            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };


            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RfqEvent_Destroy([DataSourceRequest] DataSourceRequest request, RequestForQuoteEvent mo)
        {
            string message = "Delete successful";
            if (mo != null)
            {
                RequestForQuoteEvent.Delete(mo);
            }
            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };
            return Json(result);
        }

        #endregion

        #region Rfq Document Upload
        [HttpPost]
        public ActionResult UploadRfqAttachments(IEnumerable<HttpPostedFileBase> files, string inputRfqNo)
        {
            string uploadType = "RFQ_DOCUMENT";
            if (string.IsNullOrEmpty(inputRfqNo))
            {
                return Json(new { success = true, message = "Failed to upload file. Invalid RfqNo", JsonRequestBehavior.AllowGet });
            }
            //if (string.IsNullOrEmpty(inputSolicitationNo))
            //{
            //    return Json(new { success = true, message = "Failed to upload file. Invalid SolicitationNo", JsonRequestBehavior.AllowGet });
            //}

            string message = "file uploaded successfully";


            FileUploadLog f = new FileUploadLog();
            try
            {
                if (files == null || !files.Any())
                {
                    return Json(new { success = true, message = "Please don't forget to attach a document.", JsonRequestBehavior.AllowGet });
                }
                foreach (var file in files)
                {
                    if (file == null)
                        continue;

                    f = new FileUploadLog();
                    f.FileName = file.FileName;
                    f.FileUploaded = true;
                    f.Message = "File uploaded";
                    f.UpdatedBy = Environment.UserName;
                    string filePath = file.FileName;
                    string mapPath = GetFilePath(uploadType, filePath);


                    file.SaveAs(mapPath);

                    //Here you can write code for save this information in your database if you want
                    string contentType = GetContentType(System.IO.Path.GetExtension(file.FileName));

                    // Read the file and convert it to Byte Array
                    FileStream fs = new FileStream(mapPath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    br.Close();
                    fs.Close();

                    string sql = @"INSERT INTO [rnz].[DocumentStore](LinkId,FileBaseName,FileExtension, ContentType, BinaryData,UpdatedBy) 
                                                VALUES (@LinkId,@FileBaseName, @FileExtension,@ContentType, @BinaryData,@UpdatedBy)";


                    // and store it in the DB, along with other necessary identifying info
                    using (var sqlConn = new SqlConnection(CommonMethods._connectionString))
                    {

                        using (var insertRptsGenerated = new SqlCommand(sql))
                        {
                            insertRptsGenerated.Connection = sqlConn;
                            insertRptsGenerated.Parameters.Add
                            ("@LinkId", SqlDbType.VarChar, 200).Value = inputRfqNo;
                            insertRptsGenerated.Parameters.Add
                            ("@FileBaseName", SqlDbType.VarChar, 200).Value = f.FileName;
                            insertRptsGenerated.Parameters.Add
                            ("@FileExtension", SqlDbType.VarChar, 20).Value = System.IO.Path.GetExtension(file.FileName);
                            insertRptsGenerated.Parameters.Add
                            ("@ContentType", SqlDbType.VarChar, 50).Value = contentType;
                            insertRptsGenerated.Parameters.Add
                            ("@BinaryData", SqlDbType.Binary).Value = bytes;
                            insertRptsGenerated.Parameters.Add
                            ("@UpdatedBy", SqlDbType.VarChar, 100).Value = Environment.UserName;


                            sqlConn.Open();
                            insertRptsGenerated.ExecuteNonQuery();
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                f.FileUploaded = false;
                f.Message = message = ex.Message;
                logger.Fatal(ex);
            }
            if (files != null && files.Any())
                FileUploadLog.Upsert(f);
            return Json(new { success = true, message = f.Message, JsonRequestBehavior.AllowGet });

        }

        public ActionResult DocumentStoreRfq_Read([DataSourceRequest] DataSourceRequest request, string rfqNo)
        {
            try
            {

                return new JsonResult()
                {
                    Data = DocumentStore.GetAllRfqDocuments(rfqNo).ToDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
                //return Json(result.OrderByDescending(x => x.Currency).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return null;
            }
        }

        #endregion


        #endregion
        
        #region Solicitations

        public ActionResult Solicitations()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SaveSolicitation(IEnumerable<HttpPostedFileBase> files, string uploadType, string solicitaionNumber, string solicitaionDescription, string awardQuantity, string awardAmount, string dueDate)
        {
            string message = "file uploaded successfully";
            DateTime dt;
            DateTime.TryParse(dueDate, out dt);
            int qty;
            int.TryParse(awardQuantity, out qty);
            decimal amt;
            decimal.TryParse(awardAmount, out amt);

            if (string.IsNullOrEmpty(solicitaionNumber))
            {
                Json(new { success = true, message = "Please enter a valid solicitation number.", JsonRequestBehavior.AllowGet });
            }

            Solicitation s = new Solicitation();
            s.SolicitationNo = solicitaionNumber;
            s.SolicitationDescription = solicitaionDescription;
            s.AwardQuantity = qty;
            s.AwardAmount = amt;
            s.DueDate = dt;

            Solicitation.Upsert(s);

            FileUploadLog f = new FileUploadLog();
            try
            {
                if (files == null || !files.Any())
                {
                    return Json(new { success = true, message = "Please don't forget to attach a solicitation document.", JsonRequestBehavior.AllowGet });
                }
                foreach (var file in files)
                {
                    if (file == null)
                        continue;

                    f = new FileUploadLog();
                    f.FileName = string.Format("{0}{1}", s.SolicitationNo, System.IO.Path.GetExtension(file.FileName));
                    f.FileUploaded = true;
                    f.Message = "File uploaded";
                    f.UpdatedBy = Environment.UserName;
                    string filePath = solicitaionNumber + System.IO.Path.GetExtension(file.FileName);
                    string mapPath = GetFilePath(uploadType, filePath);


                    file.SaveAs(mapPath);

                    //Here you can write code for save this information in your database if you want
                    string contentType = GetContentType(System.IO.Path.GetExtension(file.FileName));

                    // Read the file and convert it to Byte Array
                    FileStream fs = new FileStream(mapPath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    br.Close();
                    fs.Close();

                    string sql = @"INSERT INTO [rnz].[DocumentStore](LinkId,FileBaseName,FileExtension, ContentType, BinaryData,UpdatedBy) 
                                                VALUES (@LinkId,@FileBaseName, @FileExtension,@ContentType, @BinaryData,@UpdatedBy)";

                    var dc = DocumentStore.GetSoliciationDocument(solicitaionNumber);
                    if (dc != null)
                    {
                        sql = @"UPDATE [rnz].[DocumentStore]
                                SET LinkId = @LinkId
                                    ,FileBaseName = @FileBaseName
                                    ,FileExtension = @FileExtension
                                    ,ContentType = @ContentType
                                    ,BinaryData = @BinaryData
                                    ,UpdatedBy = @UpdatedBy
                                    ,UpdateDate = getutcdate()
                                WHERE DocumentStoreId = @DocumentStoreId
                                ";

                        f.Message = "Replaced solicitation file.";
                    }
                    // and store it in the DB, along with other necessary identifying info
                    using (var sqlConn = new SqlConnection(CommonMethods._connectionString))
                    {

                        using (var insertRptsGenerated = new SqlCommand(sql))
                        {
                            insertRptsGenerated.Connection = sqlConn;
                            insertRptsGenerated.Parameters.Add
                            ("@LinkId", SqlDbType.VarChar, 200).Value = solicitaionNumber;
                            insertRptsGenerated.Parameters.Add
                            ("@FileBaseName", SqlDbType.VarChar, 200).Value = f.FileName;
                            insertRptsGenerated.Parameters.Add
                            ("@FileExtension", SqlDbType.VarChar, 20).Value = System.IO.Path.GetExtension(file.FileName);
                            insertRptsGenerated.Parameters.Add
                            ("@ContentType", SqlDbType.VarChar, 50).Value = contentType;
                            insertRptsGenerated.Parameters.Add
                            ("@BinaryData", SqlDbType.Binary).Value = bytes;
                            insertRptsGenerated.Parameters.Add
                            ("@UpdatedBy", SqlDbType.VarChar, 100).Value = Environment.UserName;

                            if (dc != null)
                            {
                                insertRptsGenerated.Parameters.Add
                                    ("@DocumentStoreId", SqlDbType.Int).Value = dc.DocumentStoreId;
                            }
                            sqlConn.Open();
                            insertRptsGenerated.ExecuteNonQuery();
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                f.FileUploaded = false;
                f.Message = message = ex.Message;
                logger.Fatal(ex);
            }
            if (files != null && files.Any())
                FileUploadLog.Upsert(f);

            return Json(new { success = true, message = f.Message, JsonRequestBehavior.AllowGet });

        }

        [HttpGet]
        public ActionResult DownloadSolicitation(string solicitationNo = null)
        {

            try
            {
                var dc = DocumentStore.GetSoliciationDocument(solicitationNo);

                if (dc == null)
                    return null;

                string filePath = dc.FileBaseName;
                string mapPath = GetFilePath("DOWNLOAD", filePath);
                byte[] fileContents;
                string selectStmt = "SELECT BinaryData FROM rnz.DocumentStore WHERE LinkId = @SolicitationNo";
                using (SqlConnection connection = new SqlConnection(CommonMethods._connectionString))
                using (SqlCommand cmdSelect = new SqlCommand(selectStmt, connection))
                {
                    cmdSelect.Parameters.Add("@SolicitationNo", SqlDbType.VarChar).Value = solicitationNo;
                    connection.Open();
                    fileContents = (byte[])cmdSelect.ExecuteScalar();
                    connection.Close();
                }

                if (System.IO.File.Exists(mapPath))
                {
                    System.IO.File.Delete(mapPath);
                }



                if (fileContents != null)
                {
                    using (Stream file = System.IO.File.OpenWrite(mapPath))
                    {
                        file.Write(fileContents, 0, fileContents.Length);
                    }
                }

                string contectType = GetContentType(dc.FileExtension);

                return File(mapPath, contectType, System.IO.Path.GetFileName(mapPath));
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
            return null;
        }

        public ActionResult Solicitation_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

                return new JsonResult()
                {
                    Data = Solicitation.GetAll().ToDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
                //return Json(result.OrderByDescending(x => x.Currency).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return null;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Solicitation_Update([DataSourceRequest] DataSourceRequest request, Solicitation mo)
        {
            string message = "Update successful";
            if (mo != null && ModelState.IsValid)
            {
                Solicitation.Update(mo);
            }
            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };


            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Solicitation_Destroy([DataSourceRequest] DataSourceRequest request, Solicitation mo)
        {
            string message = "Delete successful";
            if (mo != null)
            {
                Solicitation.Delete(mo);
            }
            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };
            return Json(result);
        }

        #endregion

        #region Document Store
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DocumentStore_Destroy([DataSourceRequest] DataSourceRequest request, DocumentStore mo)
        {
            string message = "Delete successful";
            if (mo != null)
            {
                DocumentStore.Delete(mo);
            }
            var dsResult = new[] { mo }.ToDataSourceResult(request, ModelState);
            var result = new
            {
                dsResult.AggregateResults,
                dsResult.Data,
                dsResult.Errors,
                dsResult.Total,
                myMessage = message
            };
            return Json(result);
        }
        #endregion


    }
}