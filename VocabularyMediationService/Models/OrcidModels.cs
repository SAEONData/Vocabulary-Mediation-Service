using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VocabularyMediationService.Models.Orcid
{
    //#############//
    // Main models //
    //#############//

    public class Record
    {
        [JsonProperty("orcid-identifier")]
        public OrcidIdentifier OrcidIdentifier { get; set; }

        [JsonProperty("preferences")]
        public Preferences Preferences { get; set; }

        [JsonProperty("history")]
        public History History { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }

        [JsonProperty("activities-summary")]
        public ActivitiesSummary ActivitiesSummary { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Result
    {
        [JsonProperty("orcid-identifier")]
        public OrcidIdentifier OrcidIdentifier { get; set; }
    }


    //###################//
    // Supporting models //
    //###################//

    public class OrcidIdentifier
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }
    }

    public class Preferences
    {
        [JsonProperty("locale")]
        public string Locale { get; set; }
    }

    public class SubmissionDate
    {
        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public class LastModifiedDate
    {
        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public class History
    {
        [JsonProperty("creation-method")]
        public string CreationMethod { get; set; }

        [JsonProperty("completion-date")]
        public object CompletionDate { get; set; }

        [JsonProperty("submission-date")]
        public SubmissionDate SubmissionDate { get; set; }

        [JsonProperty("last-modified-date")]
        public LastModifiedDate LastModifiedDate { get; set; }

        [JsonProperty("claimed")]
        public bool Claimed { get; set; }

        [JsonProperty("source")]
        public object Source { get; set; }

        [JsonProperty("deactivation-date")]
        public object DeactivationDate { get; set; }

        [JsonProperty("verified-email")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("verified-primary-email")]
        public bool VerifiedPrimaryEmail { get; set; }
    }

    public class CreatedDate
    {
        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public class GivenNames
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class FamilyName
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class CreditName
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Name
    {
        [JsonProperty("created-date")]
        public CreatedDate CreatedDate { get; set; }

        [JsonProperty("last-modified-date")]
        public LastModifiedDate LastModifiedDate { get; set; }

        [JsonProperty("given-names")]
        public GivenNames GivenNames { get; set; }

        [JsonProperty("family-name")]
        public FamilyName FamilyName { get; set; }

        [JsonProperty("credit-name")]
        public CreditName CreditName { get; set; }

        [JsonProperty("source")]
        public object Source { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class SourceOrcid
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }
    }

    public class SourceName
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Source
    {
        [JsonProperty("source-orcid")]
        public SourceOrcid SourceOrcid { get; set; }

        [JsonProperty("source-client-id")]
        public object SourceClientId { get; set; }

        [JsonProperty("source-name")]
        public SourceName SourceName { get; set; }
    }

    public class OtherName
    {
        [JsonProperty("created-date")]
        public CreatedDate CreatedDate { get; set; }

        [JsonProperty("last-modified-date")]
        public LastModifiedDate LastModifiedDate { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("put-code")]
        public int PutCode { get; set; }

        [JsonProperty("display-index")]
        public int DisplayIndex { get; set; }
    }

    public class OtherNames
    {
        [JsonProperty("last-modified-date")]
        public LastModifiedDate LastModifiedDate { get; set; }

        [JsonProperty("other-name")]
        public IList<OtherName> OtherName { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class ResearcherUrls
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("researcher-url")]
        public IList<object> ResearcherUrl { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Emails
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("email")]
        public IList<object> Email { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Addresses
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("address")]
        public IList<object> Address { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Keywords
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("keyword")]
        public IList<object> Keyword { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class ExternalIdentifiers
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("external-identifier")]
        public IList<object> ExternalIdentifier { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Person
    {
        [JsonProperty("last-modified-date")]
        public LastModifiedDate LastModifiedDate { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("other-names")]
        public OtherNames OtherNames { get; set; }

        [JsonProperty("biography")]
        public object Biography { get; set; }

        [JsonProperty("researcher-urls")]
        public ResearcherUrls ResearcherUrls { get; set; }

        [JsonProperty("emails")]
        public Emails Emails { get; set; }

        [JsonProperty("addresses")]
        public Addresses Addresses { get; set; }

        [JsonProperty("keywords")]
        public Keywords Keywords { get; set; }

        [JsonProperty("external-identifiers")]
        public ExternalIdentifiers ExternalIdentifiers { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Educations
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("education-summary")]
        public IList<object> EducationSummary { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Employments
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("employment-summary")]
        public IList<object> EmploymentSummary { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Fundings
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("group")]
        public IList<object> Group { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class PeerReviews
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("group")]
        public IList<object> Group { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Works
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("group")]
        public IList<object> Group { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class ActivitiesSummary
    {
        [JsonProperty("last-modified-date")]
        public object LastModifiedDate { get; set; }

        [JsonProperty("educations")]
        public Educations Educations { get; set; }

        [JsonProperty("employments")]
        public Employments Employments { get; set; }

        [JsonProperty("fundings")]
        public Fundings Fundings { get; set; }

        [JsonProperty("peer-reviews")]
        public PeerReviews PeerReviews { get; set; }

        [JsonProperty("works")]
        public Works Works { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class SearchResult
    {
        [JsonProperty("result")]
        public IList<Result> Result { get; set; }

        [JsonProperty("num-found")]
        public int NumFound { get; set; }
    }


}
