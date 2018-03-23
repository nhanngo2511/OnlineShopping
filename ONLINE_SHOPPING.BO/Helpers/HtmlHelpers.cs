using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ONLINE_SHOPPING.BO.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString MyPager(this HtmlHelper helper, int TotalRecords, int PageSize)
        {
            int TotalPage = (TotalRecords / PageSize);
            if (TotalRecords % PageSize != 0)
            {
                TotalPage = TotalPage + 1;
            }

            var varCurrentPageNumber = HttpContext.Current.Request.QueryString["page"];
            int CurrentPageNumber = (varCurrentPageNumber != null) ? Convert.ToInt32(varCurrentPageNumber.ToString()) : 1;

            int PageNumberDisplay = 3;
            string CurrentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            string ActiveClass = "";
            string href = "?page";
            string symbol = "?";

            int index = CurrentUrl.IndexOf("page");
            href = (index > 0) ? CurrentUrl.Substring(0, index - 1) : CurrentUrl;
            symbol = (href.Contains("?")) ? "&" : "?";
            href += symbol + "page";

            var sb = new StringBuilder();
            sb.AppendFormat("<div style='clear:both'></div>");
            sb.AppendFormat("<div id='mypager' style='float:right'> <ul class='pages pagination'>");

            if (TotalRecords > PageSize)
            {
                if (CurrentPageNumber > 1)
                {
                    int PageNumberPrevious = CurrentPageNumber - 1;
                    sb.AppendFormat("<li class='page-number'><a href=" + href + "=" + PageNumberPrevious.ToString() + "> < </a></li>");
                }

                if (CurrentPageNumber == 1)
                {
                    sb.AppendFormat("<li class='page-number active'> <a href=" + href + "=1> 1 </a></li>");
                }
                else
                {
                    sb.AppendFormat("<li class='page-number'> <a href=" + href + "=1> 1 </a></li>");
                }


                if (CurrentPageNumber > 4)
                {
                    sb.AppendFormat("<li class='page-number'><a href='#'>...</a></li>");
                }

                for (int i = (CurrentPageNumber - PageNumberDisplay); i < (CurrentPageNumber + PageNumberDisplay + 1); i++)
                {
                    if (i > 1 && i < TotalPage)
                    {
                        ActiveClass = (i == CurrentPageNumber) ? "active" : "";
                        sb.AppendFormat("<li class='page-number " + ActiveClass + "'><a href=" + href + "=" + i + ">" + i + "</a></li>");

                    }
                }

                if (CurrentPageNumber <= TotalPage - 4)
                {
                    sb.AppendFormat("<li class='page-number'><a href='#'>...</a></li>");
                }

                if (CurrentPageNumber == TotalPage)
                {
                    sb.AppendFormat("<li class='page-number active'> <a href=" + href + "=" + TotalPage + ">" + TotalPage + "</a></li>");
                }
                else
                {
                    sb.AppendFormat("<li class='page-number'> <a href=" + href + "=" + TotalPage + ">" + TotalPage + "</a></li>");
                }


                if (CurrentPageNumber < TotalPage)
                {
                    int PageNumberNext = CurrentPageNumber + 1;
                    sb.AppendFormat("<li class='page-number'><a href=" + href + "=" + PageNumberNext.ToString() + "> > </a></li>");
                }

                sb.AppendLine("</ul></div>");

            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString FormatMoney(this HtmlHelper helper, float OldMoney)
        {

            string result;
            result = (OldMoney != 0) ? String.Format("{0:0,0 VNĐ}", OldMoney) : String.Format("{0:0 VNĐ}", OldMoney);
            result = result.Replace(',', '.');

            return MvcHtmlString.Create(result);
        }

        
    }
}