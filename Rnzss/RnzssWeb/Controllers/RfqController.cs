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
using SportsNetworkWeb.Models;


namespace SportsNetworkWeb.Controllers
{
    public class RfqController : Controller
    {
        // GET: Rfq
        public ActionResult RfqEntry(string RFQNo = null)
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


        public ActionResult GetInstrumentName([DataSourceRequest]DataSourceRequest request, string sessionId)
        {
            string result = "Can't retrieve instrument name";
            try
            {
                result = "Hello From Server";
            }
            catch (Exception ex)
            {
            }

            return Json(new { success = true, message = result }, JsonRequestBehavior.AllowGet);
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

            return File(file, "application/vnd.ms-excel", Path.GetFileName(file));
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

                string sourceFile = Path.Combine(Server.MapPath("~/RfqFiles"), "RFQ_Template.xlsx");
                string destinationFile = Path.Combine(Server.MapPath("~/RfqFiles"), string.Format("RFQ_{0}.xlsx", rfq.RFQNo));
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



                        cell.CellValue = new CellValue(string.Format("RFQ No. {0} \n # Denia Zaman \n # XYZ Company Inc. \n # 54-25 91st Street \n # New Hyde Park NY 11254", rfq.RFQNo).Replace("\n", Environment.NewLine).Replace("#", "   "));
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
                                #region Line 1
                                switch (line)
                                {
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
                                    default:
                                        break;
                                }
                                #endregion
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
                return Json(new { success = false, message = string.Format("Failed to print RFQ. {0} ", ex.Message), JsonRequestBehavior.AllowGet });
            }

        }

        #endregion

    }
}