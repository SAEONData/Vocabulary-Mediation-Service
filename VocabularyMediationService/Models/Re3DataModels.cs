using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyMediationService.Models.Re3DataModels
{
    public class Repositories
    {
        //[JsonProperty("?xml")]
        //public Xml Xml { get; set; }

        [JsonProperty("list")]
        public List List { get; set; }
    }

    public class R3dDataObject
    {

        //[JsonProperty("?xml")]
        //public Xml Xml { get; set; }

        [JsonProperty("r3d:re3data")]
        public R3dRe3data R3dRe3data { get; set; }
    }


    //public class Xml
    //{
    //    [JsonProperty("@version")]
    //    public string Version { get; set; }

    //    [JsonProperty("@encoding")]
    //    public string Encoding { get; set; }
    //}

    public class Link
    {
        [JsonProperty("@href")]
        public string Href { get; set; }

        [JsonProperty("@rel")]
        public string Rel { get; set; }
    }

    public class Repository
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("link")]
        public Link Link { get; set; }
    }

    public class List
    {
        [JsonProperty("repository")]
        public IList<Repository> Repository { get; set; }
    }

    public class R3dRepositoryName
    {

        [JsonProperty("@language")]
        public string Language { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dAdditionalName
    {

        [JsonProperty("@language")]
        public string Language { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dDescription
    {

        [JsonProperty("@language")]
        public string Language { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dSize
    {

        [JsonProperty("@updated")]
        public string Updated { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dSubject
    {

        [JsonProperty("@subjectScheme")]
        public string SubjectScheme { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dContentType
    {

        [JsonProperty("@contentTypeScheme")]
        public string ContentTypeScheme { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dInstitutionName
    {

        [JsonProperty("@language")]
        public string Language { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dInstitutionAdditionalName
    {

        [JsonProperty("@language")]
        public string Language { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dInstitution
    {

        [JsonProperty("r3d:institutionName")]
        public R3dInstitutionName R3dInstitutionName { get; set; }

        [JsonProperty("r3d:institutionAdditionalName")]
        public R3dInstitutionAdditionalName R3dInstitutionAdditionalName { get; set; }

        [JsonProperty("r3d:institutionCountry")]
        public string R3dInstitutionCountry { get; set; }

        [JsonProperty("r3d:responsibilityType")]
        public string R3dResponsibilityType { get; set; }

        [JsonProperty("r3d:institutionType")]
        public string R3dInstitutionType { get; set; }

        [JsonProperty("r3d:institutionURL")]
        public string R3dInstitutionURL { get; set; }

        [JsonProperty("r3d:responsibilityStartDate")]
        public string R3dResponsibilityStartDate { get; set; }

        [JsonProperty("r3d:responsibilityEndDate")]
        public string R3dResponsibilityEndDate { get; set; }

        [JsonProperty("r3d:institutionContact")]
        public object R3dInstitutionContact { get; set; }
    }

    public class R3dPolicy
    {

        [JsonProperty("r3d:policyName")]
        public string R3dPolicyName { get; set; }

        [JsonProperty("r3d:policyURL")]
        public string R3dPolicyURL { get; set; }
    }

    public class R3dDatabaseAccess
    {

        [JsonProperty("r3d:databaseAccessType")]
        public string R3dDatabaseAccessType { get; set; }
    }

    public class R3dDatabaseLicense
    {

        [JsonProperty("r3d:databaseLicenseName")]
        public string R3dDatabaseLicenseName { get; set; }

        [JsonProperty("r3d:databaseLicenseURL")]
        public string R3dDatabaseLicenseURL { get; set; }
    }

    public class R3dDataAccess
    {

        [JsonProperty("r3d:dataAccessType")]
        public string R3dDataAccessType { get; set; }
    }

    public class R3dDataLicense
    {

        [JsonProperty("r3d:dataLicenseName")]
        public string R3dDataLicenseName { get; set; }

        [JsonProperty("r3d:dataLicenseURL")]
        public string R3dDataLicenseURL { get; set; }
    }

    public class R3dDataUpload
    {

        [JsonProperty("r3d:dataUploadType")]
        public string R3dDataUploadType { get; set; }

        [JsonProperty("r3d:dataUploadRestriction")]
        public string R3dDataUploadRestriction { get; set; }
    }

    public class R3dSoftware
    {

        [JsonProperty("r3d:softwareName")]
        public string R3dSoftwareName { get; set; }
    }

    public class R3dApi
    {

        [JsonProperty("@apiType")]
        public string ApiType { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dMetadataStandardName
    {

        [JsonProperty("@metadataStandardScheme")]
        public string MetadataStandardScheme { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R3dMetadataStandard
    {

        [JsonProperty("r3d:metadataStandardName")]
        public R3dMetadataStandardName R3dMetadataStandardName { get; set; }

        [JsonProperty("r3d:metadataStandardURL")]
        public string R3dMetadataStandardURL { get; set; }
    }

    public class R3dRepository
    {

        [JsonProperty("r3d:re3data.orgIdentifier")]
        public string R3dRe3dataOrgIdentifier { get; set; }

        [JsonProperty("r3d:repositoryName")]
        public R3dRepositoryName R3dRepositoryName { get; set; }

        [JsonProperty("r3d:additionalName")]
        public R3dAdditionalName R3dAdditionalName { get; set; }

        [JsonProperty("r3d:repositoryURL")]
        public string R3dRepositoryURL { get; set; }

        [JsonProperty("r3d:description")]
        public R3dDescription R3dDescription { get; set; }

        [JsonProperty("r3d:repositoryContact")]
        public string R3dRepositoryContact { get; set; }

        [JsonProperty("r3d:type")]
        public IList<string> R3dType { get; set; }

        [JsonProperty("r3d:size")]
        public R3dSize R3dSize { get; set; }

        [JsonProperty("r3d:startDate")]
        public string R3dStartDate { get; set; }

        [JsonProperty("r3d:endDate")]
        public string R3dEndDate { get; set; }

        [JsonProperty("r3d:repositoryLanguage")]
        public string R3dRepositoryLanguage { get; set; }

        [JsonProperty("r3d:subject")]
        public IList<R3dSubject> R3dSubject { get; set; }

        [JsonProperty("r3d:missionStatementURL")]
        public string R3dMissionStatementURL { get; set; }

        [JsonProperty("r3d:contentType")]
        public IList<R3dContentType> R3dContentType { get; set; }

        [JsonProperty("r3d:providerType")]
        public string R3dProviderType { get; set; }

        [JsonProperty("r3d:keyword")]
        public IList<string> R3dKeyword { get; set; }

        [JsonProperty("r3d:institution")]
        public IList<R3dInstitution> R3dInstitution { get; set; }

        [JsonProperty("r3d:policy")]
        public IList<R3dPolicy> R3dPolicy { get; set; }

        [JsonProperty("r3d:databaseAccess")]
        public R3dDatabaseAccess R3dDatabaseAccess { get; set; }

        [JsonProperty("r3d:databaseLicense")]
        public R3dDatabaseLicense R3dDatabaseLicense { get; set; }

        [JsonProperty("r3d:dataAccess")]
        public R3dDataAccess R3dDataAccess { get; set; }

        [JsonProperty("r3d:dataLicense")]
        public R3dDataLicense R3dDataLicense { get; set; }

        [JsonProperty("r3d:dataUpload")]
        public R3dDataUpload R3dDataUpload { get; set; }

        [JsonProperty("r3d:software")]
        public R3dSoftware R3dSoftware { get; set; }

        [JsonProperty("r3d:versioning")]
        public string R3dVersioning { get; set; }

        [JsonProperty("r3d:api")]
        public IList<R3dApi> R3dApi { get; set; }

        [JsonProperty("r3d:pidSystem")]
        public string R3dPidSystem { get; set; }

        [JsonProperty("r3d:citationGuidelineURL")]
        public string R3dCitationGuidelineURL { get; set; }

        [JsonProperty("r3d:enhancedPublication")]
        public string R3dEnhancedPublication { get; set; }

        [JsonProperty("r3d:qualityManagement")]
        public string R3dQualityManagement { get; set; }

        [JsonProperty("r3d:metadataStandard")]
        public R3dMetadataStandard R3dMetadataStandard { get; set; }

        [JsonProperty("r3d:remarks")]
        public string R3dRemarks { get; set; }

        [JsonProperty("r3d:entryDate")]
        public string R3dEntryDate { get; set; }

        [JsonProperty("r3d:lastUpdate")]
        public string R3dLastUpdate { get; set; }
    }

    public class R3dRe3data
    {

        [JsonProperty("@xmlns:r3d")]
        public string XmlnsR3d { get; set; }

        [JsonProperty("@xmlns:xsi")]
        public string XmlnsXsi { get; set; }

        [JsonProperty("@xsi:schemaLocation")]
        public string XsiSchemaLocation { get; set; }

        [JsonProperty("r3d:repository")]
        public R3dRepository R3dRepository { get; set; }
    }
}
