using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;
using System.Linq;

namespace UMPG.USL.Common.Mappers
{
    public class LicenseSearchCriteriaMapper : IMapper<string, LicenseRequest>
    {
        public string Map(LicenseRequest source)
        {
            var queryStringCriteria = new NameValueCollection();
            var start = source.PageNo * source.PageSize;

            if (!string.IsNullOrEmpty(source.SearchTerm))
            {
                queryStringCriteria.Add("q", source.SearchTerm);
            }
            else
            {
                queryStringCriteria.Add("q", "*:*");
            }


            //Check if search term is for a licenseNumber, if it is, return
            if (isLicenseNumber(source.SearchTerm))
            {
                return ReturnQueryString(source.SearchTerm);
            }


            // SEARCH TERM
            var qfstring = "";
            var fqstring = "";
            if (!string.IsNullOrEmpty(source.SearchTerm))
            {
                if (source.IncludeRecordingTitle)
                {
                    qfstring = qfstring + "trackTitle^30  trackTileExact^30  trackTitlePartial^30  ";
                }

                if (source.IncludeProductTitle)
                {
                    qfstring = qfstring + "productTitle^90  productTitleExact^90  productTitlePartial^90  ";
                }

                if (source.IncludeLicenseTitle ||
                    (!source.IncludeRecordingTitle && !source.IncludeProductTitle && !source.IncludeLicenseTitle &&
                     !source.IncludeArtist && !source.IncludeWriter && !source.IncludePipsCode && !source.IncludeUpcCode && !source.IncludeHFALicense && !source.IncludeClientCode))
                {
                    qfstring = qfstring + "licenseTitle^900  licenseTitleExact^900  licenseTitlePartial^900  ";
                }

                if (source.IncludeArtist)
                {
                    qfstring = qfstring + "artistName^270  artistNameExact^270 artistNamePartial^270  ";
                }
                if (source.IncludeWriter)
                {
                    qfstring = qfstring + "writer^10  writerExact^10  writerPartial^10  ";
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
                    qfstring = qfstring + "licenseNumber";
                }
                qfstring += "-writerRate:*";
                if (qfstring.Length > 0)
                {
                    queryStringCriteria.Add("qf", qfstring);
                }


            }
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
            if (source.LicenseStatuses.Count > 0)
            {
                var licenseStatusList = "";
                int licenseStatusCount = source.LicenseStatuses.Count;
                for (int i = 0; i < licenseStatusCount; i++)
                {
                    licenseStatusList += source.LicenseStatuses[i];
                    if (i < licenseStatusCount- 1)
                    {
                        licenseStatusList += " OR ";
                    }

                }
                if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                fqstring = fqstring + "licenseStatusId:(" + licenseStatusList+ ") ";
            }
            if (source.LicenseTypes.Count > 0)
            {
                var licenseTypesList = "";
                int licenseTypesCount = source.LicenseTypes.Count;
                for (int i = 0; i < licenseTypesCount; i++)
                {
                    licenseTypesList += source.LicenseTypes[i];
                    if (i < licenseTypesCount - 1)
                    {
                        licenseTypesList += " OR ";
                    }

                }
                if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                fqstring = fqstring + "licenseTypeId:(" + licenseTypesList + ") ";
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
                fqstring = fqstring + " productConfigurationIds:(" + configurationsList + ") ";

            }
            if (source.Labels.Count > 0)
            {
                var licenseLabelsList = "";
                int labelsCount = source.Labels.Count;
                for (int i = 0; i < labelsCount; i++)
                {
                    licenseLabelsList += source.Labels[i];
                    if (i < labelsCount - 1)
                    {
                        licenseLabelsList += " OR ";
                    }

                }
                if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                fqstring = fqstring + "labelId:(" + licenseLabelsList + ") ";
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
            if (source.SpecialStatuses.Count > 0)
            {
                var specialStatusList = "";
                int specialCount = source.SpecialStatuses.Count;
                for (int i = 0; i < specialCount; i++)
                {
                    specialStatusList += source.SpecialStatuses[i];
                    if (i < specialCount - 1)
                    {
                        specialStatusList += " OR ";
                    }

                }
                if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";
                fqstring = fqstring + "writerSpecialStatusId:(" + specialStatusList + ") ";
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
                fqstring += String.Format("  releaseDate:[* TO {0}]",to);
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

            // USL-1093 compensate for slider rate to value being null when selected as '0.1+'
            if (!source.WriterRateTo.HasValue)
            {
                source.WriterRateTo = 1000000;
            }
            if (!source.WriterRateFrom.HasValue)
            {
                source.WriterRateFrom = 0;
            }
            //USL-1217 ::Searching w/ writerRate caused licenses w/ no rates not to be returned.
            if (source.WriterRateFrom.HasValue && source.WriterRateTo.HasValue)
            {
                if (source.WriterRateFrom.Value == 0 && source.WriterRateTo.Value == 1000000)
                {
                    //USL-1217: if this is hit, slider is set to 'show all', return all writer rates.
                }
                 else 
                {
                    if (!string.IsNullOrEmpty(fqstring)) fqstring += " AND ";

                    fqstring += String.Format("  writerRate:[{0} TO {1}]", source.WriterRateFrom.Value,
                        source.WriterRateTo.Value);
                }

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
            if (fqstring.Length > 0)
            {
                queryStringCriteria.Add("fq", fqstring);
            }


            /* sorting */
            // has to come before rows, start
            // just use a single column sort for now since that is all the UI currently supports
            string orderBy = string.Empty;
            string sortOrder = string.Empty;
            if (string.IsNullOrEmpty(source.SortOrder))
            {
                sortOrder = "desc";
            }
            else
            {
                sortOrder = source.SortOrder;
            }
            if (!string.IsNullOrEmpty(source.OrderBy))
            {
                if (source.OrderBy == "asignee")
                {
                    orderBy = "licenseAssigneeSortable";
                }
                else if (source.OrderBy == "licenseNumber")
                {
                    orderBy = "licenseNumberSortable";
                }
                else if (source.OrderBy == "title")
                {
                    orderBy = "licenseTitleSortable";
                }
                else if (source.OrderBy == "status")
                {
                    orderBy = "licenseStatusSortable";
                }
                else if (source.OrderBy == "artistRollup")
                {
                    orderBy = "artistNameSortable";
                }
                else if (source.OrderBy == "licensee")
                {
                    orderBy = "licenseeNameSortable";
                }
                else if (source.OrderBy == "method")
                {
                    orderBy = "licenseMethodNameSortable";
                }
                else if (source.OrderBy == "licenseConfigurationRollup")
                {
                    orderBy = "licenseModifiedDate"; // not implemented yet by solr search
                }
                else if (source.OrderBy == "createdby")
                {
                    orderBy = "licenseCreatedBySortable";
                }
                else if (source.OrderBy == "modifiedDate")
                {
                    orderBy = "licenseModifiedDate";
                }
                else if (source.OrderBy == "type")
                {
                    orderBy = "licenseTypeNameSortable";
                }
                else if (source.OrderBy == "createdDate")
                {
                    orderBy = "licenseCreatedDate";
                }
                else if (source.OrderBy == "signedDate")
                {
                    orderBy = "licenseSignedDate";
                }
            }
            else if (String.IsNullOrEmpty(source.SearchTerm))
            {
                orderBy = "licenseModifiedDate";
            }
            if(!string.IsNullOrEmpty(orderBy))
            queryStringCriteria.Add("sort", orderBy + " " + sortOrder);
            //example- sort=licenseCount desc 
            /* end of sorting */

            queryStringCriteria.Add("rows", source.PageSize.ToString());
            queryStringCriteria.Add("start", start.ToString());

            List<String> items = new List<String>();
            foreach (String name in queryStringCriteria)
            {
                items.Add(String.Concat(name, "=", queryStringCriteria[name]));//needs url encoding
            }
            return String.Join("&", items.ToArray());
        }

        private bool isLicenseNumber(string searchString)
        {
            if (searchString.Length >= 4 && searchString.Length <= 7)
            {
                return IsNumeric(searchString);
            }
            else
            {
                return false;
            }
        }

        private bool IsNumeric(string str)
        {
            return str.All(c => "0123456789-".Contains(c));
        }

        private string ReturnQueryString(string licenseNumber)
        {
            return "q="+ licenseNumber + "&qf=licenseNumber trackTitle^30  trackTileExact^30  trackTitlePartial^30  productTitle^90  productTitleExact^90  productTitlePartial^90  licenseTitle^900  licenseTitleExact^900  licenseTitlePartial^900  artistName^270  artistNameExact^270 artistNamePartial^270  writer^10  writerExact^10  writerPartial^10  localClientCode pipsCode upc -writerRate:*&rows=10&start=0";
        }
    }
}