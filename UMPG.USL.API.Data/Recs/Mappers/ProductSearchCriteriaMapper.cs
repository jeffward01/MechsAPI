using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.Common.Mappers
{
    public class ProductSearchCriteriaMapper : IMapper<string, ProductRequest>
    {
        public string Map(ProductRequest source)
        {
            var queryStringCriteria = new NameValueCollection();
            var start = source.PageNo*source.PageSize; 
            var qfstring = "";
            var fqstring = "";

            if (!string.IsNullOrEmpty(source.SearchTerm))
            {
                queryStringCriteria.Add("q",source.SearchTerm);

                if (source.IncludeRecordingTitle)
                {
                    qfstring = qfstring + "trackTitle^90 trackTileExact^90 trackTitlePartial^90 ";
                }

                if (source.IncludeProductTitle || (!source.IncludeRecordingTitle && !source.IncludeProductTitle && !source.IncludeLicenseTitle
                    && !source.IncludeArtist && !source.IncludeWriter &&!source.IncludePipsCode && !source.IncludeClientCode && !source.IncludeUpcCode && !source.IncludeHFALicense))
                {
                    qfstring = qfstring + "productTitle^2430 productTitleExact^2430  productTitlePartial^2430 ";
                }

                if (source.IncludeLicenseTitle)
                {
                    qfstring = qfstring + "licenseTitle^270 licenseTitleExact^270  licenseTitlePartial^270  ";
                }

                if (source.IncludeArtist)
                {
                    qfstring = qfstring + "artistName^810 artistNameExact^810 artistNamePartial^810  ";
                }
                if (source.IncludeWriter)
                {
                    qfstring = qfstring + "writer^30  writerExact^30  writerPartial^30  ";
                }
                if (source.IncludeClientCode)
                {
                    qfstring = qfstring + "localClientCode ";
                }
                if (source.IncludeHFALicense)
                {
                    qfstring = qfstring + "hpaLicenceNumber ";
                }
                if (source.IncludePipsCode)
                {
                    qfstring = qfstring + "pipsCode ";
                }
                if (source.IncludeUpcCode)
                {
                    qfstring = qfstring + "upc ";
                }
                if (source.IncludeLicenseNumber)
                {
                    qfstring = qfstring + "licenseNumber ";
                }
                queryStringCriteria.Add("qf", qfstring);
            }
            else
            {
                queryStringCriteria.Add("q", "*:*");
            }

            // SEARCH TERM

               

                //ASSIGNEE
                if (source.Assignees.Count > 0)
                {
                    var assigneelist = "";
                    int assigneecnt = source.Assignees.Count;
                    for (int i = 0; i < assigneecnt; i++)
                    {
                        assigneelist = assigneelist + source.Assignees[i];
                        if (i < assigneecnt - 1)
                        {
                            assigneelist = assigneelist + " OR ";
                        }

                    }
                    fqstring = fqstring + "licenseAssigneeId:(" + assigneelist + ") ";
                }
                
                //LICENSEE
                if (source.Licensees.Count > 0)
                {
                    var licenseelist = "";
                    int licenseecnt = source.Licensees.Count;
                    for (int i = 0; i < licenseecnt; i++)
                    {
                        licenseelist = licenseelist + source.Licensees[i];
                        if (i < licenseecnt - 1)
                        {
                            licenseelist = licenseelist + " OR ";
                        }

                    }
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring = fqstring + "licenseLicenseeId :(" + licenseelist + ") ";
                }

                //LICENSEMETHOD
                if (source.LicMethods.Count > 0)
                {
                    var licensemethodslist = "";
                    int licensemethodcnt = source.LicMethods.Count;
                    for (int i = 0; i < licensemethodcnt; i++)
                    {
                        licensemethodslist = licensemethodslist + source.LicMethods[i];
                        if (i < licensemethodcnt - 1)
                        {
                            licensemethodslist = licensemethodslist + " OR ";
                        }

                    }
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring = fqstring + "licenseMethodId:(" + licensemethodslist + ") ";
                }
                if (source.Labels.Count > 0)
                {
                    var labelsList = "";
                    int labelsListCount = source.Labels.Count;
                    for (int i = 0; i < labelsListCount; i++)
                    {
                        labelsList += source.Labels[i];
                        if (i < labelsListCount - 1)
                        {
                            labelsList += " OR ";
                        }

                    }
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring = fqstring + "labelId:(" + labelsList + ") ";
                }
                if (source.LabelGroups.Count > 0)
                {
                    var licenseLabelsList = "";
                    int labelsGroupCount = source.LabelGroups.Count;
                    for (int i = 0; i < labelsGroupCount; i++)
                    {
                        licenseLabelsList += source.LabelGroups[i];
                        if (i < labelsGroupCount - 1)
                        {
                            licenseLabelsList += " OR ";
                        }

                    }
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring = fqstring + "labelGroupId:(" + licenseLabelsList + ") ";
                }
                if (source.Publishers.Count > 0)
                {
                    var publishersList = "";
                    int publisherListCount = source.Publishers.Count;
                    for (int i = 0; i < publisherListCount; i++)
                    {
                        publishersList += source.Publishers[i];
                        if (i < publisherListCount - 1)
                        {
                            publishersList += " OR ";
                        }

                    }
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring = fqstring + "publisherIpCode:(" + publishersList + ") ";
                }
                if (source.RateTypes.Count > 0)
                {
                    var rateTypesList = "";
                    int rateCount = source.RateTypes.Count;
                    for (int i = 0; i < rateCount; i++)
                    {
                        rateTypesList += source.RateTypes[i];
                        if (i < rateCount - 1)
                        {
                            rateTypesList += " OR ";
                        }

                    }
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring = fqstring + " writerRateTypeId :(" + rateTypesList + ") ";
                }
                if (source.LicenseTypes.Count > 0)
                {
                    var licenseTypesList = "";
                    int typesCount = source.LicenseTypes.Count;
                    for (int i = 0; i < typesCount; i++)
                    {
                        licenseTypesList += source.LicenseTypes[i];
                        if (i < typesCount - 1)
                        {
                            licenseTypesList += " OR ";
                        }

                    }
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring = fqstring + " licenseTypeId:(" + licenseTypesList + ") ";
                }
                if (source.Configurations.Count > 0)
                {
                    var configurationsList = "";
                    int configurationListCount = source.Configurations.Count;
                    for (int i = 0; i < configurationListCount; i++)
                    {
                        configurationsList += source.Configurations[i];
                        if (i < configurationListCount - 1)
                        {
                            configurationsList += " OR ";
                        }

                    }
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring = fqstring + " productConfigurationIds :(" + configurationsList + ") ";

                }
            if (source.SpecialStatuses.Count > 0)
            {
                var specialStatusList = "";
                int specialStatusCount = source.SpecialStatuses.Count;
                for (int i = 0; i < specialStatusCount; i++)
                {
                    specialStatusList += source.SpecialStatuses[i];
                    if (i < specialStatusCount - 1)
                    {
                        specialStatusList += " OR ";
                    }

                }
                if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                fqstring = fqstring + " writerSpecialStatusId :(" + specialStatusList + ") ";

            }
            if (source.LicenseStatuses.Count > 0)
            {
                var licenseStatusList = "";
                int statusCount = source.LicenseStatuses.Count;
                for (int i = 0; i < statusCount; i++)
                {
                    licenseStatusList += source.LicenseStatuses[i];
                    if (i < statusCount - 1)
                    {
                        licenseStatusList += " OR ";
                    }

                }
                if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                fqstring = fqstring + " licenseStatusId :(" + licenseStatusList + ") ";
            }
            if (source.Publishers.Count > 0)
            {
                var publishersList = "";
                int publisherCount = source.Publishers.Count;
                for (int i = 0; i < publisherCount; i++)
                {
                    publishersList += source.Publishers[i];
                    if (i < publisherCount - 1)
                    {
                        publishersList += " OR ";
                    }

                }
                if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                fqstring = fqstring + "publisherIpCode:(" + publishersList + ") ";
            }
                if (source.ReleaseDateFrom.HasValue && !source.ReleaseDateTo.HasValue)
                {
                    var from = StringHelper.ToSolrDate(source.ReleaseDateFrom.Value);
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring += String.Format("  releaseDate:[{0} TO *]", from);
                }
                else if (source.ReleaseDateFrom.HasValue && source.ReleaseDateTo.HasValue)
                {
                    var from = StringHelper.ToSolrDate(source.ReleaseDateFrom.Value);
                    var to = StringHelper.ToSolrDate(source.ReleaseDateTo.Value);
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring += String.Format("  releaseDate:[{0} TO {1}]", from, to);
                }
                else if (!source.ReleaseDateFrom.HasValue && source.ReleaseDateTo.HasValue)
                {
                    var to = StringHelper.ToSolrDate(source.ReleaseDateTo.Value);
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring += String.Format("  releaseDate:[* TO {0}]", to);
                }

                if (source.SignedDateFrom.HasValue && !source.SignedDateTo.HasValue)
                {
                    var from = StringHelper.ToSolrDate(source.SignedDateFrom.Value);
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring += String.Format("  licenseSignedDate:[{0} TO *]", from);
                }
                else if (source.SignedDateFrom.HasValue && source.SignedDateTo.HasValue)
                {
                    var from = StringHelper.ToSolrDate(source.SignedDateFrom.Value);
                    var to = StringHelper.ToSolrDate(source.SignedDateTo.Value);
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring += String.Format("  licenseSignedDate:[{0} TO {1}]", from, to);
                }
                else if (!source.SignedDateFrom.HasValue && source.SignedDateTo.HasValue)
                {
                    var to = StringHelper.ToSolrDate(source.SignedDateTo.Value);
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring += String.Format("  licenseSignedDate:[* TO {0}]", to);
                }

                if (source.WriterRateFrom.HasValue && source.WriterRateTo.HasValue)
                {
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring += String.Format("  writerRate:[{0} TO {1}]", source.WriterRateFrom.Value, source.WriterRateTo.Value);
                }
                if (source.WritersConsentTypes.Count > 0)
                {
                    var consentTypesList = "";
                    int consentCount = source.WritersConsentTypes.Count;
                    for (int i = 0; i < consentCount; i++)
                    {
                        consentTypesList += source.WritersConsentTypes[i];
                        if (i < consentCount - 1)
                        {
                            consentTypesList += " OR ";
                        }

                    }
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring = fqstring + "writersConsentTypeId:(" + consentTypesList + ") ";
                }
                if (source.FilterPriorityReport)
                {
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring += " mechsPriority:true";
                }
                if (source.FilterStatusReport)
                {
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                    fqstring += " statusReport:true";
                }

                if (fqstring.Length > 0)
                {
                    queryStringCriteria.Add("fq", fqstring);
                }
            
            
            /* sorting */
            // has to come before rows, start
            // just use a single column sort for now since that is all the UI currently supports
            string orderBy = string.Empty;
            string sortOrder = string.Empty;
            if (source.SortOrder.Length == 0) {
                sortOrder = "desc";
            }
            else
            {
                sortOrder = source.SortOrder;
            }
            if (source.OrderBy.Length > 0) {
                if (source.OrderBy == "title") {
                    orderBy = "productTitleSortable";
                }
                else if (source.OrderBy == "artist") {
                    orderBy = "artistNameSortable";
                }
                else if (source.OrderBy == "licenses") {
                    orderBy = "licenseCount";
                }
                else if (source.OrderBy == "label") {
                    orderBy = "labelNameSortable";
                }
                else if (source.OrderBy == "date")
                {
                    orderBy = "id";  //tomh changed to Id desc for default... releasedate was an error
                }
               
               
            }
            else if (String.IsNullOrEmpty(source.SearchTerm))
            {
                orderBy = "id";
            }
           if(orderBy.Length > 0)
            queryStringCriteria.Add("sort", orderBy + " " + sortOrder);
            //example- sort=licenseCount desc 
            /* end of sorting */

            queryStringCriteria.Add("rows",source.PageSize.ToString());
            queryStringCriteria.Add("start",start.ToString());

            List<String> items = new List<String>();
            foreach (String name in queryStringCriteria)
            {
                items.Add(String.Concat(name, "=", queryStringCriteria[name]));//needs url encoding
            }
            return String.Join("&", items.ToArray());
        }
    }
}