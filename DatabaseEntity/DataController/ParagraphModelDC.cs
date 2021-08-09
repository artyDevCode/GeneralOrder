using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    public class ParagraphModelDC
    {
        /// <summary>
        /// Import/modify paragraph style for Portfilios, Act Title and Act Comments
        /// </summary>
        /// <param name="res"></param>
        /// <param name="paragraphID"></param>
        /// <returns></returns>
        public static int GenOrderParagraphImport(DocViewModel res, string paragraphType, int paragraphID = 0)
        {

            var paragraphModels = new ParagraphModel();

            using (var dbConnect = new EntityDBM())
            {
                //Get Paragraph meta if it exists
                paragraphModels =  dbConnect.ParagraphModels.Where(r => r.ParagraphModelID == paragraphID).FirstOrDefault();
                //check the paragraph type based on comment type
                if (paragraphType == "GeneralOrderActComment")
                {
                    switch (res.bulletASCI)
                    {
                        case 183:
                            paragraphType = "GeneralOrderActComment_1";
                            break;
                        case 997:
                            paragraphType = "GeneralOrderActComment_FirstNoBullet";
                            break;
                        case 111:
                            paragraphType = "GeneralOrderActComment_2";
                            break;
                        case 998:
                            paragraphType = "GeneralOrderActComment_SecondNoBullet";
                            break;
                        case 167:
                            paragraphType = "GeneralOrderActComment_3";
                            break;
                        case 999:
                            paragraphType = "GeneralOrderActComment_ThirdNoBullet";
                            break;
                        case 996:
                            paragraphType = "GeneralOrderActComment_MainNoBullet";
                            break;
                        default:
                            paragraphType = "";
                            break;
                    }
                }

                //find the type id to link the paragraphmodeltype
                var result = dbConnect.ParagraphModelTypes
                   .Where(r => r.ParagraphModelTypeName == paragraphType)
                  .Select(r => r.ParagraphModelTypeID)
                  .FirstOrDefault();


                if (paragraphModels != null)
                {
                    paragraphModels.FontBold = res.fontBold;
                    paragraphModels.BulletChar = res.bulletChar;
                    paragraphModels.BulletASCII = res.bulletASCI;
                    paragraphModels.IndentationBy = res.indentationBy;
                    paragraphModels.TabHangingIndent = res.TabHangingIndent;
                    paragraphModels.IndentationLeft = res.IndentationLeft;
                    paragraphModels.IndentationRight = res.IndentationRight;
                    paragraphModels.ListSymbolFont = res.listSymbolFont;
                    paragraphModels.PageBreakBefore = res.pageBreakBefore;
                    paragraphModels.AlignmentType = res.paragraphAlignment;
                    paragraphModels.TabStopPosition = res.tabStopPosition;
                    paragraphModels.ParagraphModelTypeID = result; // the ParagraphModelTypeID from ParagraphModelType.


                    dbConnect.Entry(paragraphModels).State = EntityState.Modified;
                }
                else
                {
                    paragraphModels = dbConnect.ParagraphModels.Add(new ParagraphModel
                    {
                        FontBold = res.fontBold,
                        BulletChar = res.bulletChar,
                        BulletASCII = res.bulletASCI,
                        IndentationBy = res.indentationBy,
                        TabHangingIndent = res.TabHangingIndent,
                        IndentationLeft = res.IndentationLeft,
                        IndentationRight = res.IndentationRight,
                        ListSymbolFont = res.listSymbolFont,
                        PageBreakBefore = res.pageBreakBefore,
                        AlignmentType = res.paragraphAlignment,
                        TabStopPosition = res.tabStopPosition,
                        ParagraphModelTypeID = result
                    });

                }
                dbConnect.SaveChanges(); //Save the changes of the details
                return paragraphModels.ParagraphModelID; //Populate the ID back in GeneralOrderImportHeader
            }
        }

        /// <summary>
        /// Gets the standardised META for the paragraph types... portfolio, act and comments
        /// </summary>
        /// <param name="res"></param>
        /// <param name="paragraphType"></param>
        /// <returns></returns>
        public static DocViewModel GetStandardizedParagraphMeta(short bulletASCI, string paragraphType)
        {
            DocViewModel result = new DocViewModel();
            using (var dbConnect = new EntityDBM())
            {
                //check the paragraph type based on comment type
                if (paragraphType == "GeneralOrderActComment")
                {
                    switch (bulletASCI)
                    {
                        
                        case 183:
                            paragraphType = "GeneralOrderActComment_1";
                            break;
                        case 997:
                            paragraphType = "GeneralOrderActComment_FirstNoBullet";
                            break;
                        case 111:
                            paragraphType = "GeneralOrderActComment_2";
                            break;
                        case 998:
                            paragraphType = "GeneralOrderActComment_SecondNoBullet";
                            break;
                        case 167:
                            paragraphType = "GeneralOrderActComment_3";
                            break;
                        case 999:
                            paragraphType = "GeneralOrderActComment_ThirdNoBullet";
                            break;
                        case 996:
                            paragraphType = "GeneralOrderActComment_MainNoBullet";
                            break;
                        default:
                            paragraphType = "";
                            break;
                    }
                }

                //find the type id to link the paragraphmodeltype
                var paraResult =  dbConnect.ParagraphModelTypes
                    .Where(r => r.ParagraphModelTypeName == paragraphType)
                   .FirstOrDefault();

                result.paragraphAlignment = paraResult.AlignmentType;
                result.bulletASCI = paraResult.BulletASCII;
                result.bulletChar = paraResult.BulletChar;
                result.fontBold = paraResult.FontBold;
                result.indentationBy = (float)paraResult.IndentationBy;
                result.IndentationLeft = (float)paraResult.IndentationLeft;
                result.IndentationRight = (float)paraResult.IndentationRight;
                result.listSymbolFont = paraResult.ListSymbolFont;
                result.pageBreakBefore = paraResult.PageBreakBefore;
                result.TabHangingIndent = paraResult.TabHangingIndent;
                result.tabStopPosition = (float)paraResult.TabStopPosition;

            }
            return result;
        }
    }
}
