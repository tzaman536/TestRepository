﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RnzssWeb.Models
{
    public class FileUploadLog
    {
        public int FileUploadLogId { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; }
        public bool FileUploaded { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }



        public static bool Delete(FileUploadLog p)
        {

            p.UpdatedBy = Environment.UserName;

            // TODO: Check if this RFQNo and part number exists alreayd then call update and return from here
            //if (ProductExists(p.RFQNo,p.PartNumber))
            //{
            //    return true;
            //}


            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                    DELETE FROM [Carry].[FileUploadLog]
                                                    WHERE FileUploadLogId = @FileUploadLogId
                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;

        }
        public static bool Update(FileUploadLog p)
        {

            p.UpdatedBy = Environment.UserName;

            // TODO: Check if this RFQNo and part number exists alreayd then call update and return from here
            //if (ProductExists(p.RFQNo,p.PartNumber))
            //{
            //    return true;
            //}


            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"

                                                UPDATE [Carry].[FileUploadLog]
                                                   SET [FileName] = @FileName
                                                      ,[Message] = @Message
                                                      ,[FileUploaded] = @FileUploaded
                                                      ,[UpdatedBy] = @UpdatedBy
                                                      ,[UpdateDate] = getutcdate()
                                                 WHERE FileUploadLogId = @FileUploadLogId

                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;

        }

        public static bool Upsert(FileUploadLog p)
        {

            p.UpdatedBy = Environment.UserName;
            var dbP = FileExists(p);

            if (dbP != null)
            {
                p.FileUploadLogId = dbP.FileUploadLogId;
                return Update(p);
            }


            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                            INSERT INTO [Carry].[FileUploadLog]
                                                       ([FileName]
                                                       ,[Message]
                                                       ,[FileUploaded]
                                                       ,[UpdatedBy]
                                                       )
                                                 VALUES
                                                       (@FileName
                                                       ,@Message
                                                       ,@FileUploaded
                                                       ,@UpdatedBy
                                                       )                                               

                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;

        }

        public static FileUploadLog FileExists(FileUploadLog p)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Query<FileUploadLog>(@"
                                                        select *     
                                                        from [Carry].[FileUploadLog]
                                                        where FileName = @FileName
                                                          and UpdatedBy = @UpdatedBy
                                                        ", p, commandTimeout: 0);
                    if (result != null && result.Any())
                        return result.FirstOrDefault();
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }

            return null;

        }
        public static IEnumerable<FileUploadLog> GetAll()
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<FileUploadLog>(@"
                                                        select *     
                                                        from [Carry].[FileUploadLog]

                                                        ", commandTimeout: 0);
                }
                catch (Exception ex)
                {

                }

            }

            return null;

        }

        public static FileUploadLog GetFileInfo(string fileName, string uploadedBy)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<FileUploadLog>(@"
                                                        select *     
                                                        from [Carry].[FileUploadLog]
                                                        where FileName = @fileName
                                                          and UpdatedBy = @uploadedBy

                                                        ", new { @fileName = fileName, @uploadedBy = uploadedBy }, commandTimeout: 0).FirstOrDefault();
                }
                catch (Exception ex)
                {

                }

            }

            return null;

        }

    }
}