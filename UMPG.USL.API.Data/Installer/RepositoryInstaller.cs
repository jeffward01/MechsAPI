using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UMPG.USL.API.Data.Configuration;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.API.Data.Reports;
using UMPG.USL.Common.Mappers;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.API.Data.AuditData;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.API.Data.Token;
using UMPG.USL.Models.LicenseSearchModel;

namespace UMPG.USL.API.Data.Installer
{
    public class RepositoryInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAuthRepository>().ImplementedBy<AuthRepository>());
            container.Register(Component.For<ILicenseRepository>().ImplementedBy<LicenseRepository>());
            container.Register(Component.For<IContactRepository>().ImplementedBy<ContactRepository>());
            container.Register(Component.For<IRoleRepository>().ImplementedBy<RoleRepository>());

            container.Register(Component.For<IContactDefaultRepository>().ImplementedBy<ContactDefaultRepository>());
            container.Register(Component.For<IContactGenerateLicenseQueueRepository>().ImplementedBy<ContactGenerateLicenseQueueRepository>());
            container.Register(Component.For<IGenerateLicenseQueueRepository>().ImplementedBy<GenerateLicenseQueueRepository>());
            container.Register(Component.For<IAddressRepository>().ImplementedBy<AddressRepository>());
            container.Register(Component.For<ILicenseeRepository>().ImplementedBy<LicenseeRepository>());
            container.Register(Component.For<ILicenseAttachmentRepository>().ImplementedBy<LicenseAttachmentRepository>());
            container.Register(Component.For<ILicenseNoteRepository>().ImplementedBy<LicenseNoteRepository>());
            container.Register(Component.For<ILicensePRWriterNoteRepository>().ImplementedBy<LicensePRWriterNoteRepository>());
            container.Register(Component.For<ILicenseMethodRepository>().ImplementedBy<LicenseMethodRepository>());
            container.Register(Component.For<IPriorityRepository>().ImplementedBy<PriorityRepository>());
            container.Register(Component.For<ILicenseProductRepository>().ImplementedBy<LicenseProductRepository>());
            container.Register(Component.For<ILicenseProductRecordingRepository>().ImplementedBy<LicenseProductRecordingRepository>());
            container.Register(Component.For<ILicensePRWriterRepository>().ImplementedBy<LicensePRWriterRepository>());
            container.Register(Component.For<ILicensePRWriterRateRepository>().ImplementedBy<LicensePRWriterRateRepository>());
            //container.Register(Component.For<ILicensePRWriterRateNoteRepository>().ImplementedBy<LicensePRWriterRateNoteRepository>());
            container.Register(Component.For<ILicensePRWriterRateStatusRepository>().ImplementedBy<LicensePRWriterRateStatusRepository>());
            container.Register(Component.For<ILicenseUploadPreviewLicenseRepository>().ImplementedBy<LicenseUploadPreviewLicenseRepository>());
            container.Register(Component.For<ILicenseSequenceRepository>().ImplementedBy<LicenseSequenceRepository>());
            container.Register(Component.For<IRateTypeRepository>().ImplementedBy<RateTypeRepository>());
            container.Register(Component.For<IScheduleRepository>().ImplementedBy<ScheduleRepository>());
            container.Register(Component.For<IPaidQuarterRepository>().ImplementedBy<PaidQuarterRepository>());
            container.Register(Component.For<IWritersConsentTypeRepository>().ImplementedBy<WritersConsentTypeRepository>());
            container.Register(Component.For<ISpecialStatusRepository>().ImplementedBy<SpecialStatusRepository>());
            container.Register(Component.For<ILicenseStatusRepository>().ImplementedBy<LicenseStatusRepository>());
            container.Register(Component.For<ILicenseTypeRepository>().ImplementedBy<LicenseTypeRepository>());
            container.Register(Component.For<ILicenseRecordingMedleyRepository>().ImplementedBy<LicenseRecordingMedleyRepository>());
            container.Register(Component.For<IRecordingMedleyRepository>().ImplementedBy<RecordingMedleyRepository>());

            container.Register(Component.For<INoteTypeRepository>().ImplementedBy<NoteTypeRepository>());
            container.Register(Component.For<ITrackTypeRepository>().ImplementedBy<TrackTypeRepository>());

            container.Register(Component.For<IAuditRepository>().ImplementedBy<AuditRepository>());

            
            container.Register(Component.For<IContactEmailRepository>().ImplementedBy<ContactEmailRepository>());
            container.Register(Component.For<IPhoneRepository>().ImplementedBy<PhoneRepository>());

            container.Register(Component.For<ISearchProvider>().ImplementedBy<SearchProvider>());
            container.Register(Component.For<IRecsDataProvider>().ImplementedBy<RecsDataProvider>());

            container.Register(Component.For<IRecs>().ImplementedBy<Recs.Recs>());
            container.Register(Component.For<ISolrSearch>().ImplementedBy<Solr>());
            container.Register(Component.For<ISolrUpdate>().ImplementedBy<SolrUpdate>());
            container.Register(Component.For<IRecsConfigurationRetriever>().ImplementedBy<RecsConfigurationRetriever>());
            container.Register(Component.For<ISolrConfigurationRetriever>().ImplementedBy<SolrConfigurationRetriever>());
            container.Register(Component.For<ISolrUpdateServerConfigurationRetriever>().ImplementedBy<SolrUpdateServerConfigurationRetriever>());
            container.Register(Component.For<ILicenseProductConfigurationRepository>().ImplementedBy<LicenseProductConfigurationRepository>());

            container.Register(Component.For<IAgreementStatutoryRateRepository>().ImplementedBy<AgreementStatutoryRateRepository>());
            container.Register(Component.For<IStatRateRepository>().ImplementedBy<StatRateRepository>());

            // Audit tables
            container.Register(Component.For<IAuditLicenseRepository>().ImplementedBy<AuditLicenseRepository>());
            container.Register(Component.For<IAuditLicenseProductConfigurationRepository>().ImplementedBy<AuditLicenseProductConfigurationRepository>());
            container.Register(Component.For<IAuditLicenseeRepository>().ImplementedBy<AuditLicenseeRepository>());

            container.Register(Component.For<IAuditLicenseAttachmentRepository>().ImplementedBy<AuditLicenseAttachmentRepository>());
            container.Register(Component.For<IAuditLicenseNoteRepository>().ImplementedBy<AuditLicenseNoteRepository>());
            container.Register(Component.For<IAuditLicenseProductRecordingRepository>().ImplementedBy<AuditLicenseProductRecordingRepository>());
            container.Register(Component.For<IAuditLicenseProductRepository>().ImplementedBy<AuditLicenseProductRepository>());
            container.Register(Component.For<IAuditLicensePRWriterNoteRepository>().ImplementedBy<AuditLicensePRWriterNoteRepository>());
            container.Register(Component.For<IAuditLicensePRWriterRateRepository>().ImplementedBy<AuditLicensePRWriterRateRepository>());
            container.Register(Component.For<IAuditLicensePRWriterRepository>().ImplementedBy<AuditLicensePRWriterRepository>());

            container.Register(Component.For<IAuditLicensePRWriterStatusRepository>().ImplementedBy<AuditLicensePRWriterStatusRepository>());
            container.Register(Component.For<IAuditLicenseRecordingMedleyRepository>().ImplementedBy<AuditLicenseRecordingMedleyRepository>());
            container.Register(Component.For<IReportQueueRepository>().ImplementedBy<ReportQueueRepository>());
            container.Register(Component.For<ITokenRepository>().ImplementedBy<TokenRepository>());

            container.Register(Component.For<ISolrIndexQueueRepository>().ImplementedBy<SolrIndexQueueRepository>());
            container.Register(Component.For<IAttachmentTypeRepository>().ImplementedBy<AttachmentTypeRepository>());

            //Dataharmonization
            container.Register(Component.For<ISnapshotLicenseProductRepository>().ImplementedBy<SnapshotLicenseProductRepository>());
            container.Register(Component.For<ISnapshotLicenseRepository>().ImplementedBy<SnapshotLicenseRepository>());
            container.Register(Component.For<ISnapshotArtistRecsRepository>().ImplementedBy<SnapshotArtistRecsRepository>());
            container.Register(Component.For<ISnapshotConfigurationRepository>().ImplementedBy<SnapshotConfigurationRepository>());
            container.Register(Component.For<ISnapshotContactRepository>().ImplementedBy<SnapshotContactRepository>());
            container.Register(Component.For<ISnapshotLabelRepository>().ImplementedBy<SnapshotLabelRepository>());
            container.Register(Component.For<ISnapshotLabelGroupRepository>().ImplementedBy<SnapshotLabelGroupRepository>());
            container.Register(Component.For<ISnapshotLicenseNoteRepository>().ImplementedBy<SnapshotLicenseNoteRepository>());
            container.Register(Component.For<ISnapshotLicenseProductConfigurationRepository>().ImplementedBy<SnapshotLicenseProductConfigurationRepository>());
            container.Register(Component.For<ISnapshotProductHeaderRepository>().ImplementedBy<SnapshotProductHeaderRepository>());
            container.Register(Component.For<ISnapshotRecsConfiguration>().ImplementedBy<SnapshotRecsConfiguration>());
            container.Register(Component.For<ISnapshotRoleRepository>().ImplementedBy<SnapshotRoleRepository>());
            container.Register(Component.For<ISnapshotWorksRecordingRepository>().ImplementedBy<SnapshotWorksRecordingRepository>());


            container.Register(Component.For<IMapper<string, ProductRequest>>().ImplementedBy<ProductSearchCriteriaMapper>().LifestyleSingleton());
            container.Register(Component.For<IMapper<string, LicenseRequest>>().ImplementedBy<LicenseSearchCriteriaMapper>().LifestyleSingleton());
            container.Register(Component.For<IMapper<License, LicenseSOLR>>().ImplementedBy<LicenseSOLRMapper>().LifestyleSingleton());
            container.Register(Component.For<IMapper<Product, ProductSOLR>>().ImplementedBy<ProductSOLRMapper>().LifestyleSingleton());
            container.Register(Component.For<IMapper<Recording, WorksRecording>>().ImplementedBy<TracksRecsMapper>().LifestyleSingleton());
            container.Register(Component.For<IMapper<Writer, WorksWriter>>().ImplementedBy<WritersRecsRMapper>().LifestyleSingleton());
        }
    }
}
