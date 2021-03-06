﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UMPG.USL.API.Business.DataHarmonization;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Licenses
{
    public class LicenseProductConfigurationManager : ILicenseProductConfigurationManager
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly ILicenseProductRepository _licenseProductRepository;
        private readonly ILicenseProductConfigurationRepository _licenseProductConfigurationRepository;
        private readonly ILicenseProductRecordingRepository _licenseProductRecordingRepository;
        private readonly ISnapshotConfigurationRepository _snapshotConfigurationRepository;
        //private readonly ILicensePRWriterRateNoteRepository _licensePRWriterRateNoteRepository;
        private readonly ILicensePRWriterNoteRepository _licensePRWriterNoteRepository;
        private readonly ISnapshotRecsConfigurationRepository _snapshotRecsConfigurationRepository;
        private readonly ILicensePRWriterRateRepository _licensePRWriterRateRepository;
        private readonly ILicensePRWriterRepository _licensePRWriterRepository;
        private readonly ILicensePRWriterRateStatusRepository _licensePRWriterRateStatusRepository;
        private readonly IRecs _recsRepository;
        private readonly IRecordingMedleyRepository _recordingMedleyRepository;
        private readonly ILicenseRecordingMedleyRepository _licenseRecordingMedleyRepository;
        private readonly IDataHarmonizationManager _dataHarmonizationManager;
        private readonly ILicenseSolrManager _licenseSolrManager;
        private readonly ISnapshotManager _snapshotManager;
        private readonly ISnapshotLicenseProductRepository _snapshotLicenseProductRepository;
        private readonly ISnapshotProductHeaderRepository _snapshotProductHeaderRepository;
        public LicenseProductConfigurationManager
        (
            ISnapshotRecsConfigurationRepository snapshotRecsConfigurationRepository,
        ISnapshotConfigurationRepository snapshotConfigurationRepository,
            ISnapshotProductHeaderRepository snapshotProductHeaderRepository,
        ISnapshotManager snapshotManager,
            IDataHarmonizationManager dataHarmonizationManager,
            ILicenseRepository licenseRepository,
            ILicenseProductRepository licenseProductRepository,
            ILicenseProductConfigurationRepository licenseProductConfigurationRepository,
            ILicenseProductRecordingRepository licenseProductRecordingRepository,
            //ILicensePRWriterRateNoteRepository licensePRWriterRateNoteRepository,
            ILicensePRWriterNoteRepository licensePRWriterNoteRepository,
            ILicensePRWriterRateRepository licensePRWriterRateRepository,
            ILicensePRWriterRepository licensePRWriterRepository,
            ILicensePRWriterRateStatusRepository licensePRWriterRateStatusRepository,
            IRecs recsRepository,
            IRecordingMedleyRepository recordingMedleyRepository,
            ILicenseRecordingMedleyRepository licenseRecordingMedleyRepository,
            ILicenseSolrManager licenseSolrManager,
            ISnapshotLicenseProductRepository snapshotLicenseProductRepository
        )
        {
            _snapshotRecsConfigurationRepository = snapshotRecsConfigurationRepository;
            _snapshotConfigurationRepository = snapshotConfigurationRepository;
            _snapshotProductHeaderRepository = snapshotProductHeaderRepository;
            _snapshotLicenseProductRepository = snapshotLicenseProductRepository;
            _snapshotManager = snapshotManager;
            _dataHarmonizationManager = dataHarmonizationManager;
            _licenseRepository = licenseRepository;
            _licenseProductRepository = licenseProductRepository;
            _licenseProductConfigurationRepository = licenseProductConfigurationRepository;
            _licenseProductRecordingRepository = licenseProductRecordingRepository;
            //_licensePRWriterRateNoteRepository = licensePRWriterRateNoteRepository;
            _licensePRWriterNoteRepository = licensePRWriterNoteRepository;
            _licensePRWriterRateRepository = licensePRWriterRateRepository;
            _licensePRWriterRepository = licensePRWriterRepository;
            _licensePRWriterRateStatusRepository = licensePRWriterRateStatusRepository;
            _recsRepository = recsRepository;
            _recordingMedleyRepository = recordingMedleyRepository;
            _licenseRecordingMedleyRepository = licenseRecordingMedleyRepository;
            _licenseSolrManager = licenseSolrManager;
        }

        public List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseproductId)
        {
            return _licenseProductConfigurationRepository.GetLicenseProductConfigurations(licenseproductId);
        }

        public List<LicenseProductConfiguration> GetLicenseConfigurationList(List<int> licenseProductIds)
        {
            return _licenseProductConfigurationRepository.GetLicenseConfigurationList(licenseProductIds);
        }

        public LicenseProductConfiguration GetLicenseProductConfigurationByProductConfigurationId(int licensePRoductId,int productConfigId)
        {
            return
                _licenseProductConfigurationRepository
                    .GetLicenseProductConfigurationByProductIdAndLicenseProductConfigurationId(licensePRoductId,productConfigId);
        }

        public List<UpdateLicenseProductConfigurationResult> UpdateLicenseProductConfiguration(List<UpdateLicenseProductConfigurationRequest> requests)
        {
            var licenseProductConfigurationResults = new List<UpdateLicenseProductConfigurationResult>();
            foreach (var request in requests)
            {
                licenseProductConfigurationResults.Add(AddLicenseProductConfiguration(request));
            }
            return licenseProductConfigurationResults;
        }

        //This method 'saves' a new LicenseProduct and configs
        public UpdateLicenseProductConfigurationResult AddLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request)
        {

            var updateLicenseProductConfigurationResult = CreateUpdateLicenseProductConfigurationResult();
            // need this in case we need to delete due to error prior
            var deleteLicenseProductConfigurationRequest = new UpdateLicenseProductConfigurationRequest();


            //request.action = "ADDNEW"|"ADDEXISTING"|"UPDATE"
            var productHeader = _recsRepository.RetrieveProductHeader(request.productId); // Hit needed

            try
            {
                var createdDate = DateTime.Now;
                var createdBy = 1;
                var product_configuration_id = 0;
                //add config to recs
                //var newProductHeader = _recsRepository.UpdateProduct(product);
                ProductHeader newProductHeader = new ProductHeader();
                try
                {
                    dynamic product = UpdateRecsProductHeader(productHeader,request);
                    newProductHeader = _recsRepository.UpdateProduct(product); //Hit needed
                                                                               //check if productHeader exists
                   // if (DoesProductHeaderSnapshotExistCheckByProductHeaderId((int)newProductHeader.Id))
                   if(DoesLicenseProductSnapshotExist((int)request.licenseProductId))
                    {
                        //update snapshot with new productHeader, only update meta data.  dont add config here.
                        _snapshotManager.UpdateProductHeaderSnapshot(newProductHeader, request);
                    }
                    //    newProductHeader

                    //save productHeader
                }
                catch (Exception ex)
                {
                    updateLicenseProductConfigurationResult.success = false;
                    updateLicenseProductConfigurationResult.errorMessage = "update product error: " + ex.Message.ToString();
                    return updateLicenseProductConfigurationResult;
                }
     
                /*
                var response = _recsRepository.UpdateProduct(product);
                //need to get deserialized response.responseBody into a ProductHeader model rather than calling get product header again...
                var newProductHeader = _recsRepository.RetrieveProductHeader(request.productId);
                //product_configuration_id = Convert.ToInt32(newProductHeader.Configurations.Where(recsconfig => (int)recsconfig.Configuration.ConfigId == (int)request.configuration_id).FirstOrDefault());
                */

                if (request.product_configuration_id == 0)
                {
                    /*
                    foreach (var recsconfig in newProductHeader.Configurations)
                    {
                        if (recsconfig.Configuration.ConfigId == request.configuration_id)
                        {
                            product_configuration_id = (int)recsconfig.configuration_id;
                            break;
                        }
                    }
                    */

                    long matchid = 0;
                    foreach (var recsconfig in newProductHeader.Configurations.Where(r => r.Configuration.ConfigId == request.configuration_id))
                    {
                        matchid = 0;
                        foreach (var originalconfig in productHeader.Configurations.Where(o => o.Configuration.ConfigId == request.configuration_id))
                        {
                            if (recsconfig.configuration_id == originalconfig.configuration_id)
                            {
                                matchid = originalconfig.configuration_id;
                                break;
                            }
                        }
                        if (matchid == 0)
                        {
                            product_configuration_id = (int)recsconfig.configuration_id;
                            break;
                        }
                    }
                }
                else
                {
                    product_configuration_id = request.product_configuration_id;
                }

                //
                //  update License tables
                //
                var CatalogNumber = string.Empty;
                LicenseProductConfiguration licenseProductConfiguration = null;
                Snapshot_LicenseProduct savedLicenseProductSnapshot = null;
                var licenseProductId = 0;

                //Update licenseProductConfiguration
                if (request.action == "UPDATE")
                {
                    // just update LicenseProductConfiguration
                    UpdateLicenseProductConfiguration(request);
                }

                //Create new licenseProductConfiguration Entry
                else
                {
                    //If first config (no product on license), create product first.
                    if (request.licenseId == 0)  //add licenseProduct first, then add config
                    {
                        var licenseProduct = _licenseProductRepository.Add(CreateLicenseProduct(request)); //Hit needed
                        licenseProductId = licenseProduct.LicenseProductId;

                        //snapshotLicenseProduct
                        savedLicenseProductSnapshot = _dataHarmonizationManager.TakeLicenseProductSnapshotLite(licenseProduct);

                        //add LicenseRecording records for each recording in the newly added product
                        AddLicenseRecordingForProduct(request.productId, createdDate, createdBy, licenseProductId);
                    }
                    //if there is a license, but (no product on license), create product first.
                    //this is hit when licenseID != 0, but LicenseProductId == 0 which throws an error w/o this block

                    if (request.licenseProductId == 0 && request.licenseId != 0)
                    {

                        var licenseProduct = _licenseProductRepository.Add(CreateLicenseProduct(request)); //Hit needed
                        licenseProductId = licenseProduct.LicenseProductId;

                        //snapshotLicenseProduct
                        savedLicenseProductSnapshot = _dataHarmonizationManager.TakeLicenseProductSnapshotLite(licenseProduct);

                        //add LicenseRecording records for each recording in the newly added product
                        AddLicenseRecordingForProduct(request.productId, createdDate, createdBy, licenseProductId);
                    }
                    else
                    {
                        licenseProductId = request.licenseProductId;
                    }

                    //Check if licensepRoductSnapshot exists
                    if (!DoesLicenseProductSnapshotExist(licenseProductId))
                    {
                        //if it doesnt exists, build a new licensePRoductSnapshot


                        //Build new license product snapshot if null, it not null continue
                        var newLicenseProduct = CreateLicenseProduct(request);
                        //snapshotLicenseProduct
                        savedLicenseProductSnapshot = _dataHarmonizationManager.TakeLicenseProductSnapshotLite(newLicenseProduct);
                    }

                    //check if productHeader exists
                    if (!DoesProductHeaderSnapshotExist(licenseProductId))
                    {
                        //create it if it doesntExist
                        var savedProudctHeaderSnapshot = _snapshotManager.SnapshotProductHeader(newProductHeader);

                        //link to snapshotLicenseProduct
                        var snapshotLicenseProduct =
                            _snapshotLicenseProductRepository.GetLicenseProductByLicenseProductId(licenseProductId);
                        snapshotLicenseProduct.SnapshotProductHeaderId =
                            savedProudctHeaderSnapshot.SnapshotProductHeaderId;

                        //link it
                        _snapshotLicenseProductRepository.UpdateSnapshotLicenseProduct(snapshotLicenseProduct);
                    }

                   

                    if (request.catalogNumber != null)
                    {
                        CatalogNumber = request.catalogNumber.ToString();
                    }

                    //update or add config

                    //if update we are only updating the CatalogNumber currently, no need to change writer/rate info
                    //if request.licenseProductConfigurationId

                    //create LicenseProductConfiguration Record
                    var newlicenseProductConfiguration = new LicenseProductConfiguration
                    {
                        LicenseProductId = licenseProductId, //request.licenseProductId,
                        configuration_id = request.configuration_id,
                        configuration_name = request.configuration_name,
                        product_configuration_id = product_configuration_id, //request.product_configuration_id,
                        CatalogNumber = CatalogNumber,
                        PriorityReport = false,
                        StatusReport = false,
                        CreatedDate = createdDate,
                        CreatedBy = createdBy
                    };

                    //var licenseProductConfiguration = _licenseProductConfigurationRepository.Add(newlicenseProductConfiguration);
                    licenseProductConfiguration = _licenseProductConfigurationRepository.Add(newlicenseProductConfiguration);

                    //license
                    var licenseId = request.licenseId;
                    if (licenseId == 0)
                    {
                        licenseId = request.addTolicenseId;
                    }

                    var license = _licenseRepository.GetLite(licenseId); // hit needed
                    var writerRateInclude = false;
                    if (license.LicenseTypeId == 4)  //gratis
                    {
                        writerRateInclude = true;
                    }
                    //moved check for productHeaderSnapshot and LPSnapshot

                   

                    var snapshotProductHeaderId = GetSnapshotProductHeaderId(licenseProductId);

                    //add config to snapshot if its not there
                    AddConfigToSnapshot(request, licenseProductConfiguration);

                    //link licenseProductConfigurationId to ProductHeader
                    _snapshotManager.LinkProductConfigurationToProductHeader(licenseProductConfiguration, snapshotProductHeaderId);

                    //Add product header snapshot to LicenseProductSnapshot
                    //ensure licenseProduct is updated to see productHeader
                    UpdateSnapshotLicenseProduct(savedLicenseProductSnapshot, snapshotProductHeaderId);

                    //create LicenseWriterRate records
                    createLicenseProductRecordingIds(licenseProductId, request, createdDate, createdBy,
                        product_configuration_id, writerRateInclude);
                }


                SetUpdateLicenseProductConfigurationResult(updateLicenseProductConfigurationResult,
                    licenseProductConfiguration, newProductHeader);

                if (request.licenseId == 0)
                {
                    UpdateLicenseProductRollups(request.addTolicenseId);
                }
                else
                {
                    UpdateLicenseProductRollups(request.licenseId);
                }

                return updateLicenseProductConfigurationResult;
                //return licenseProductConfiguration;
            }
            catch (Exception ex)
            {
                HandleFailedUpdateLicenseProductConfigurationResult(updateLicenseProductConfigurationResult, request, ex);
                return updateLicenseProductConfigurationResult;

                //return new LicenseProductConfiguration();
            }
        }

        private bool DeleteLicenseProduct(int licenseId, int productId)
        {
            var rv = false;

            var modifiedDate = DateTime.Now;
            var modifiedBy = 1;

            var licenseProduct = _licenseProductRepository.GetLicenseProduct(licenseId, productId);
            if (licenseProduct != null)
            {
                var licenseProductIds = new List<int> { licenseProduct.LicenseProductId };
                var licenseProductRecordings =
                    _licenseProductRecordingRepository.GetLicenseRecordingsList(licenseProductIds);

                foreach (var licenseProductRecording in licenseProductRecordings)
                {
                    var licenseWriters =
                        _licensePRWriterRepository.GetLicenseWriters(licenseProductRecording.LicenseRecordingId);
                    //var licenseWriterIds = new List<int>();
                    var licenseWriterIds = licenseWriters.Select(w => w.LicenseWriterId).Distinct().ToList();

                    var licenseWriterRateIds = new List<int>();

                    foreach (var licenseWriterId in licenseWriterIds)
                    {
                        var licenseWriterRates =
                            _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRates(licenseWriterId);
                        foreach (var licenseWriterRate in licenseWriterRates)
                        {
                            if (!licenseWriterRateIds.Contains(Convert.ToInt32(licenseWriterRate.LicenseWriterRateId)))
                            {
                                licenseWriterRateIds.Add(licenseWriterRate.LicenseWriterRateId);
                            }
                            // LicenseWriterRate
                            licenseWriterRate.Deleted = modifiedDate;
                            licenseWriterRate.ModifiedBy = modifiedBy;
                            licenseWriterRate.ModifiedDate = modifiedDate;
                            _licensePRWriterRateRepository.Update(licenseWriterRate);
                        }
                    }
                    var licenseWriterRateStatuses = _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatus(licenseWriterRateIds);
                    foreach (var licenseWriterRateStatus in licenseWriterRateStatuses)
                    {
                        // LicenseWriterRateStatus
                        licenseWriterRateStatus.Deleted = modifiedDate;
                        licenseWriterRateStatus.ModifiedBy = modifiedBy;
                        licenseWriterRateStatus.ModifiedDate = modifiedDate;
                        _licensePRWriterRateStatusRepository.Update(licenseWriterRateStatus);
                    }

                    var licenseWriterNotes =
                        _licensePRWriterNoteRepository.GetLicenseProductRecordingWriterNotes(
                            licenseWriterRateIds);
                    foreach (var licenseWriterNote in licenseWriterNotes)
                    {
                        // LicenseWriterRateNote
                        licenseWriterNote.Deleted = modifiedDate;
                        licenseWriterNote.ModifiedBy = modifiedBy;
                        licenseWriterNote.ModifiedDate = modifiedDate;
                        _licensePRWriterNoteRepository.Update(licenseWriterNote);
                    }

                    foreach (var licenseWriter in licenseWriters)
                    {
                        //  LicenseWriter
                        licenseWriter.Deleted = modifiedDate;
                        licenseWriter.ModifiedBy = modifiedBy;
                        licenseWriter.ModifiedDate = modifiedDate;
                        _licensePRWriterRepository.Update(licenseWriter);
                    }

                    //  License Recording
                    licenseProductRecording.Deleted = modifiedDate;
                    licenseProductRecording.ModifiedBy = modifiedBy;
                    licenseProductRecording.ModifiedDate = modifiedDate;
                    _licenseProductRecordingRepository.Update(licenseProductRecording);
                }

                //LicenseProductConfiguration
                var licenseProductConfigurations =
                    _licenseProductConfigurationRepository.GetLicenseProductConfigurations(
                        licenseProduct.LicenseProductId);
                foreach (var licenseProductConfiguration in licenseProductConfigurations)
                {
                    licenseProductConfiguration.Deleted = modifiedDate;
                    licenseProductConfiguration.ModifiedBy = modifiedBy;
                    licenseProductConfiguration.ModifiedDate = modifiedDate;
                    _licenseProductConfigurationRepository.Update(licenseProductConfiguration);
                }

                // LicenseProduct
                licenseProduct.Deleted = modifiedDate;
                licenseProduct.ModifiedDate = modifiedDate;
                licenseProduct.ModifiedBy = modifiedBy;
                _licenseProductRepository.Update(licenseProduct);

                rv = true;
            }

            UpdateLicenseProductRollups(licenseId);

            return rv;
        }

        public bool DeleteLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request)
        {
            // need to delete
            // LicenseProductConfiguration
            // LicenseWriterRate
            // LicenseWriterRateNote

            //success
            //productHeader
            //errors

            try
            {
                var modifiedDate = DateTime.Now;
                var modifiedBy = 1;

                var licenseProductConfigurationId = request.licenseProductConfigurationId;
                var licenseProductConfiguration = _licenseProductConfigurationRepository.Get((int)licenseProductConfigurationId);

                var licenseProductId = licenseProductConfiguration.LicenseProductId;
                //var configuration_id = licenseProductConfiguration.configuration_id;
                var product_configuration_id = licenseProductConfiguration.product_configuration_id;

                licenseProductConfiguration.Deleted = modifiedDate;
                licenseProductConfiguration.ModifiedDate = modifiedDate;
                licenseProductConfiguration.ModifiedBy = modifiedBy;

                // LicenseProductConfiguration
                _licenseProductConfigurationRepository.Update(licenseProductConfiguration);

                var licenseProductRecordingIds = _licenseProductRecordingRepository.GetLicenseRecordingsIdsByLicenseProductId(licenseProductId);

                if (licenseProductRecordingIds.Count > 0)
                {
                    var licenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(licenseProductRecordingIds);

                    if (licenseWriterIds.Count > 0)
                    {
                        //var licenseWriterRates = _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRatesByWriterIdsConfig((List<int>)licenseWriterIds, (int)configuration_id);
                        var licenseWriterRates = _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRatesByWriterIdsConfig((List<int>)licenseWriterIds, (int)product_configuration_id);

                        // licenseWriterRate
                        foreach (var licenseWriterRate in licenseWriterRates)
                        {
                            licenseWriterRate.Deleted = modifiedDate;
                            licenseWriterRate.ModifiedDate = modifiedDate;
                            licenseWriterRate.ModifiedBy = modifiedBy;
                            _licensePRWriterRateRepository.Update(licenseWriterRate);
                        }

                        var licenseWriterNotes = _licensePRWriterNoteRepository.GetLicenseProductRecordingWriterNotes(licenseWriterIds);
                        foreach (var licenseWriterNote in licenseWriterNotes)
                        {
                            // LicenseWriterRateNote
                            licenseWriterNote.Deleted = modifiedDate;
                            licenseWriterNote.ModifiedBy = modifiedBy;
                            licenseWriterNote.ModifiedDate = modifiedDate;
                            _licensePRWriterNoteRepository.Update(licenseWriterNote);
                        }
                    }
                }

                UpdateLicenseProductRollups(request.licenseId);
                DeleteLicenseConfigurationFromSnapshot(request, licenseProductConfiguration);




                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void UpdateLicenseProductRollups(int licenseId)
        {
            var artistRollup = string.Empty;
            var configRollup = string.Empty;
            var artistsDone = false;
            var configsDone = false;
            var products = _licenseProductRepository.GetLicenseProducts(licenseId);
            var license = _licenseRepository.GetLite(licenseId);

            var currentLicenseConfigurationRollup = "";

            foreach (var licenseProduct in products)
            {
                var productConfigurations = _licenseProductConfigurationRepository.GetLicenseProductConfigurations(licenseProduct.LicenseProductId);
                var productHeader = _recsRepository.RetrieveProductHeader(licenseProduct.ProductId);

                foreach (var productConfiguration in productConfigurations)
                {
                    foreach (var recsConfig in productHeader.Configurations)
                    {
                        if (productConfiguration.product_configuration_id == recsConfig.configuration_id)
                        {
                            var mechsConfigRollup = "";
                            var c = recsConfig.Configuration.type.Substring(0, 1);

                            switch (c)
                            {
                                case "A":
                                    mechsConfigRollup = "B";
                                    ; break;
                                case "D":
                                    mechsConfigRollup = "D";
                                    break;

                                case "P":
                                    mechsConfigRollup = "P";
                                    break;
                            }

                            if (!string.IsNullOrEmpty(currentLicenseConfigurationRollup))
                            {
                                if (currentLicenseConfigurationRollup == "P" && mechsConfigRollup != "P")
                                {
                                    currentLicenseConfigurationRollup = "B";
                                }
                                else
                                {
                                    if (currentLicenseConfigurationRollup == "D" && mechsConfigRollup != "D")
                                    {
                                        currentLicenseConfigurationRollup = "B";
                                    }
                                }
                            }
                            else
                            {
                                currentLicenseConfigurationRollup = mechsConfigRollup;
                            }
                        }
                    }
                }

                configRollup = currentLicenseConfigurationRollup;

                if (!artistsDone)
                {
                    if (artistRollup.Length == 0)
                    {
                        artistRollup = productHeader.Artist.name;
                    }
                    else if (artistRollup != productHeader.Artist.name)
                    {
                        artistRollup = "Various Artists";
                        artistsDone = true;
                    }
                }

                if (configRollup == "B")
                {
                    configsDone = true;
                }

                //if already at artistRollup=Various Artists and configRollup=B we can exit as they will not change
                if (artistsDone && configsDone)
                {
                    break;
                }
            }

            var updateLicense = false;
            if (license.ArtistRollup != artistRollup)
            {
                license.ArtistRollup = artistRollup;
                updateLicense = true;
            }
            if (license.LicenseConfigurationRollup != configRollup)
            {
                license.LicenseConfigurationRollup = configRollup;
                updateLicense = true;
            }
            if (updateLicense)
            {
                _licenseRepository.UpdateLicense(license);
            }
        }

        public void UpdateLicenseProductRollups2(int licenseId)
        {
            var artistRollup = string.Empty;
            var configRollup = string.Empty;
            var artistsDone = false;
            var configsDone = false;
            var products = _licenseProductRepository.GetLicenseProducts(licenseId);
            var license = _licenseRepository.GetLite(licenseId);

            var currentLicenseConfigurationRollup = "";

            foreach (var licenseProduct in products)
            {
                var productConfigurations = _licenseProductConfigurationRepository.GetLicenseProductConfigurations(licenseProduct.LicenseProductId);
                var productHeader = _recsRepository.RetrieveProductHeader(licenseProduct.ProductId);

                foreach (var productConfiguration in productConfigurations)
                {
                    foreach (var recsConfig in productHeader.Configurations)
                    {
                        if (productConfiguration.product_configuration_id == recsConfig.configuration_id)
                        {
                            var mechsConfigRollup = "";
                            var c = recsConfig.Configuration.type.Substring(0, 1);

                            switch (c)
                            {
                                case "A":
                                    mechsConfigRollup = "B";
                                    ; break;
                                case "D":
                                    mechsConfigRollup = "D";
                                    break;

                                case "P":
                                    mechsConfigRollup = "P";
                                    break;
                            }

                            if (!string.IsNullOrEmpty(currentLicenseConfigurationRollup))
                            {
                                if (currentLicenseConfigurationRollup == "P" && mechsConfigRollup != "P")
                                {
                                    currentLicenseConfigurationRollup = "B";
                                }
                                else
                                {
                                    if (currentLicenseConfigurationRollup == "D" && mechsConfigRollup != "D")
                                    {
                                        currentLicenseConfigurationRollup = "B";
                                    }
                                }
                            }
                            else
                            {
                                currentLicenseConfigurationRollup = mechsConfigRollup;
                            }
                        }
                    }
                }

                configRollup = currentLicenseConfigurationRollup;

                if (!artistsDone)
                {
                    if (artistRollup.Length == 0)
                    {
                        artistRollup = productHeader.Artist.name;
                    }
                    else if (artistRollup != productHeader.Artist.name)
                    {
                        artistRollup = "Various Artists";
                        artistsDone = true;
                    }
                }

                if (configRollup == "B")
                {
                    configsDone = true;
                }

                //if already at artistRollup=Various Artists and configRollup=B we can exit as they will not change
                if (artistsDone && configsDone)
                {
                    break;
                }
            }

            var updateLicense = false;
            if (license.ArtistRollup != artistRollup)
            {
                license.ArtistRollup = artistRollup;
                updateLicense = true;
            }
            if (license.LicenseConfigurationRollup != configRollup)
            {
                license.LicenseConfigurationRollup = configRollup;
                updateLicense = true;
            }

            updateLicense = true;
            if (updateLicense)
            {
                _licenseRepository.UpdateLicense(license);
                _licenseSolrManager.UpdateLicense(license.LicenseId, true);
            }
        }

        public bool UpdateAllLicensesConfiguration(int startLicenseIdIndex, int endLicenseIdIndex)
        {
            var licenses = _licenseRepository.GetAll(startLicenseIdIndex, endLicenseIdIndex);
            var returnedValue = true;
            var i = 0;
            while (i < licenses.Count)
            {
                try
                {
                    UpdateLicenseProductRollups2(licenses[i].LicenseId);
                }
                catch (Exception ex)
                {
                    returnedValue = false;
                }

                i++;
            }
            return returnedValue;
        }

        //Helper Methods
        private void createLicenseProductRecordingIds(int licenseProductId, UpdateLicenseProductConfigurationRequest request, DateTime createdDate, int createdBy, int product_configuration_id, bool writerRateInclude)
        {
            var licenseProductRecordingIds = _licenseProductRecordingRepository.GetLicenseRecordingsIdsByLicenseProductId(licenseProductId); //request.licenseProductId);

            if (licenseProductRecordingIds.Count > 0)
            {
                if (licenseProductRecordingIds[0] != 0)
                {
                    var licenseWriterIds =
                        _licensePRWriterRepository.GetLicenseRecordingWriterIds(licenseProductRecordingIds);
                    foreach (var licenseWriterId in licenseWriterIds)
                    {
                        if (request.product_configuration_id == 0)
                        {
                        }
                        var licenseProductRecordingWriterRate = new LicenseProductRecordingWriterRate
                        {
                            LicenseWriterId = licenseWriterId,
                            product_configuration_id = product_configuration_id,
                            configuration_id = request.configuration_id,
                            configuration_name = request.configuration_name,
                            CreatedBy = createdBy,
                            CreatedDate = createdDate,
                            writersConsentTypeId = 5,
                            Rate = Convert.ToDecimal(0),
                            PerSongRate = Convert.ToDecimal(0),
                            ProRataRate = Convert.ToDecimal(0),
                            RateTypeId = 12,
                            //NA   //was 1 for Stat Rate temp/default for now so config rates show up
                            WriterRateInclude = writerRateInclude
                        };

                        licenseProductRecordingWriterRate =
                            _licensePRWriterRateRepository.Add(licenseProductRecordingWriterRate);
                    }
                }
            }
        }

        private void AddLicenseWriters(List<WorksWriter> medleywriters, DateTime createdDate, int createdBy, int licenseRecordingId)
        {
            foreach (var worksWriter in medleywriters)
            {
                var newLicenseProductRecordingWriter = new LicenseProductRecordingWriter()
                {
                    LicenseRecordingId = licenseRecordingId,
                    CAECode = worksWriter.CaeNumber,
                    ExecutedSplit = Convert.ToDecimal(worksWriter.Contribution),
                    CreatedBy = createdBy,
                    CreatedDate = createdDate
                };

                var licenseProductRecordingWriter = _licensePRWriterRepository.Add(newLicenseProductRecordingWriter); //Hit needed
            }
        }

        private void ProcessSamplesAndMedleys(List<LicenseRecordingMedley> licenseRecordingMedleys, DateTime createdDate, int createdBy, int licenseProductId)
        {
            foreach (var licenseRecordingMedley in licenseRecordingMedleys)
            {
                //add license recordingid
                var newMedleyLicenseProductRecording = new LicenseProductRecording()
                {
                    LicenseProductId = licenseProductId,
                    TrackId = (int)licenseRecordingMedley.MedleyTrackId,
                    TrackTypeId = licenseRecordingMedley.TrackTypeId,
                    CreatedBy = createdBy,
                    CreatedDate = createdDate
                };

                var medleyLicenseProductRecording = _licenseProductRecordingRepository.Add(newMedleyLicenseProductRecording); //Hit needed

                var recordingMedley = new RecordingMedley()
                {
                    LicenseRecordingId = medleyLicenseProductRecording.LicenseRecordingId,
                    LicenseRecordingMedleyId = licenseRecordingMedley.LicenseRecordingMedleyId
                };

                recordingMedley = _recordingMedleyRepository.Add(recordingMedley); //Hit needed
                var listOfIds = new List<long>();
                listOfIds.Add(licenseRecordingMedley.MedleyTrackId);
                var tracks = _recsRepository.RetrieveTracks(listOfIds, ""); //Hit needed
                var medleywriters = _recsRepository.RetrieveWriters(tracks.Values.FirstOrDefault().PipsCode); //Hit needed

                //add license writer rates below                                                                                              //add license writers
                AddLicenseWriters(medleywriters, createdDate, createdBy,
                    medleyLicenseProductRecording.LicenseRecordingId);
            }
        }

        public void AddLicenseRecordingForProductSafe(int productId, int licenseProductId)
        {
            var createdBy = 1;
            var createdDate = DateTime.Now;
            var worksRecordings = _recsRepository.RetrieveTracks(productId);

            foreach (var worksRecording in worksRecordings)
            {
                //if not already present.
                if (!IsAlreadyPresent(worksRecording.Track.Id, licenseProductId))
                {
                    var newlicenseProductRecording = new LicenseProductRecording()
                    {
                        LicenseProductId = licenseProductId,
                        TrackId = worksRecording.Track.Id,
                        CreatedBy = createdBy,
                        CreatedDate = createdDate
                    };

                    var licenseProductRecording = _licenseProductRecordingRepository.Add(newlicenseProductRecording);
                    //Hit needed

                    var worksWriters = new List<UMPG.USL.Models.Recs.WorksWriter>();

                    if (worksRecording.Track.Copyrights != null)
                    {
                        var workcode = worksRecording.Track.Copyrights[0].WorkCode.ToString();

                        //var worksWriters = _recsRepository.RetrieveWriters(workcode);
                        //moved out of loop so we do not have to call again if we have samples/medleys

                        //licenseProductRecording.LicenseRecordingId

                        worksWriters = _recsRepository.RetrieveWriters(workcode);

                        CreateWorksRecordingForWriter(worksWriters, licenseProductRecording.LicenseRecordingId,
                            createdBy, createdDate);
                    }

                    //
                    // Samples/Medleys
                    //
                    var licenseRecordingMedleys =
                        _licenseRecordingMedleyRepository.GetMedleysByTrackId(worksRecording.Track.Id); //Hit needed
                    ProcessSamplesAndMedleys(licenseRecordingMedleys, createdDate, createdBy, licenseProductId);
                }
            }
        }

        private bool IsAlreadyPresent(int trackId, int licenseProductId)
        {
            return _licenseProductRecordingRepository.IsAlreadyPresent(trackId, licenseProductId);
        }

        public void AddLicenseRecordingForProduct(int productId, DateTime createdDate, int createdBy, int licenseProductId)
        {
            var worksRecordings = _recsRepository.RetrieveTracks(productId);

            foreach (var worksRecording in worksRecordings)
            {
                var newlicenseProductRecording = new LicenseProductRecording()
                {
                    LicenseProductId = licenseProductId,
                    TrackId = worksRecording.Track.Id,
                    CreatedBy = createdBy,
                    CreatedDate = createdDate
                };

                var licenseProductRecording = _licenseProductRecordingRepository.Add(newlicenseProductRecording); //Hit needed

                //assign new licenserecording
                worksRecording.LicenseRecording = licenseProductRecording;

                var worksWriters = new List<UMPG.USL.Models.Recs.WorksWriter>();

                if (worksRecording.Track.Copyrights != null)
                {
                    var workcode = worksRecording.Track.Copyrights[0].WorkCode.ToString();

                    //var worksWriters = _recsRepository.RetrieveWriters(workcode);
                    //moved out of loop so we do not have to call again if we have samples/medleys

                    //licenseProductRecording.LicenseRecordingId

                    worksWriters = _recsRepository.RetrieveWriters(workcode);
                    worksRecording.Writers = worksWriters;
                    CreateWorksRecordingForWriter(worksWriters, licenseProductRecording.LicenseRecordingId,
                        createdBy, createdDate);
                }

                //
                // Samples/Medleys
                //
                var licenseRecordingMedleys = _licenseRecordingMedleyRepository.GetMedleysByTrackId(worksRecording.Track.Id); //Hit needed
                ProcessSamplesAndMedleys(licenseRecordingMedleys, createdDate, createdBy, licenseProductId);
            }

            //take worksrecording snapshot if nessesary
            if (!DoesSnapshotLicenseHaveRecordings(licenseProductId))
            {
                _snapshotManager.TakeWorksRecordingSnapshot(worksRecordings, productId, licenseProductId, createdBy,
                    createdDate);
            }
        }

        private bool DoesSnapshotLicenseHaveRecordings(int licenseProductId)
        {
            return _licenseProductRepository.DoeseLicenseProductHaveRecordings(licenseProductId);
        }

        private void CreateWorksRecordingForWriter(List<WorksWriter> worksWriters, int licenseRecordingId, int createdBy, DateTime createdDate)
        {
            foreach (var worksWriter in worksWriters)
            {
                var newLicenseProductRecordingWriter = new LicenseProductRecordingWriter()
                {
                    LicenseRecordingId = licenseRecordingId,
                    CAECode = worksWriter.CaeNumber,
                    ExecutedSplit = Convert.ToDecimal(worksWriter.Contribution),
                    CreatedBy = createdBy,
                    CreatedDate = createdDate
                };

                var licenseProductRecordingWriter = _licensePRWriterRepository.Add(newLicenseProductRecordingWriter); //Hit needed
            }
        }

        private LicenseProduct CreateLicenseProduct(UpdateLicenseProductConfigurationRequest request)
        {
            var newlicenseProduct = new LicenseProduct
            {
                LicenseId = request.addTolicenseId,
                ProductId = request.productId,
                ScheduleId = GetMaxValueForLicenseProduct(request.addTolicenseId),
                CreatedDate = DateTime.Now,
                CreatedBy = 1,
            };

            return newlicenseProduct;
        }

        private int GetMaxValueForLicenseProduct(int licenseId)
        {
            var max = 1;
            var productsForLicense = _licenseProductRepository.GetLicenseProducts(licenseId);//Hit needed
            if (productsForLicense != null)
            {
                var schedules = productsForLicense.Select(x => x.ScheduleId).ToList();
                if (schedules.Count > 0)
                {
                    max = schedules.Max() + 1;
                }
            }
            return max;
        }

        private UpdateLicenseProductConfigurationResult CreateUpdateLicenseProductConfigurationResult()
        {
            return new UpdateLicenseProductConfigurationResult
            {
                success = false,
                errorMessage = String.Empty,
                productHeader = new Models.Recs.ProductHeader(),
                licenseProductConfiguration = new LicenseProductConfiguration()
            };
        }

        private bool DoesLicenseProductSnapshotExist(int licenseProductId)
        {
            if (licenseProductId != 0)
            {
                return _snapshotLicenseProductRepository.DoesLicenseProductSnapshotExist(licenseProductId);
            }
            return false;
        }

        private bool DoesProductHeaderSnapshotExist(int licenseProductId)
        {
            return _snapshotLicenseProductRepository.DoesProductHeaderSnapshotExist(licenseProductId);
        }

        private bool DoesProductHeaderSnapshotExistCheckByProductHeaderId(int cloneProductHeaderId)
        {
            return _snapshotLicenseProductRepository.DoesProductHeaderSnapshotExistById(cloneProductHeaderId);
        }

        private List<ExpandoObject> UpdateExistingConfigs(ProductHeader productHeader, UpdateLicenseProductConfigurationRequest request)
        {
            dynamic config, configtype;
            var configs = new List<ExpandoObject>();
            foreach (var current_config in productHeader.Configurations)
            {
                config = new ExpandoObject();
                config.id = current_config.configuration_id;
                config.databaseVersion = current_config.DatabaseVersion;

                //update existing Recs UPC and Release Date from Edit Configs modal
                if (request.product_configuration_id == config.id)
                {
                    config.upc = request.upc;
                    if (request.releaseDate != null && request.releaseDate.Length > 0)
                    {
                        config.releaseDate = Convert.ToDateTime(request.releaseDate).ToString("dd-MMM-yyyy");
                    }
                }
                else
                {
                    config.upc = current_config.UPC;
                    if (current_config.ReleaseDate != null)
                    {
                        config.releaseDate = Convert.ToDateTime(current_config.ReleaseDate).ToString("dd-MMM-yyyy");
                    }
                }

                if (current_config.Configuration != null)
                {
                    configtype = new ExpandoObject();
                    configtype.id = current_config.Configuration.ConfigId;
                    configtype.name = current_config.Configuration.name;
                    config.configuration = configtype;
                }
                configs.Add(config);
            }
            return configs;
        }

        private List<ExpandoObject> AddNewConfigToRecs(UpdateLicenseProductConfigurationRequest request)
        {
            dynamic newconfig, newconfigtype;
            var configs = new List<ExpandoObject>();
            //add new config here
            newconfig = new ExpandoObject();
            newconfigtype = new ExpandoObject();

            if (request.upc != null && request.upc.Length > 0)
            {
                newconfig.upc = request.upc;
            }
            if (request.releaseDate != null && request.releaseDate.Length > 0)
            {
                newconfig.releaseDate = Convert.ToDateTime(request.releaseDate).ToString("dd-MMM-yyyy");
            }

            newconfigtype.id = request.configuration_id;
            newconfigtype.name = request.configuration_name;
            newconfig.configuration = newconfigtype;

            configs.Add(newconfig);
            return configs;
        }

        private int GetSnapshotProductHeaderId(int licenseProductId)
        {
            return _snapshotLicenseProductRepository.GetSnapshotProductHeaderId(licenseProductId);
        }

        private int GetSnapshotCloneProductHeaderId(int snapshotProductHeaderId)
        {
            return _snapshotLicenseProductRepository.GetSnapshotCloneProductHeaderId(snapshotProductHeaderId);
        }

        private void UpdateSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct,
            int snapshotProductHeaderId)
        {

            if (snapshotLicenseProduct == null)
            {
                //Go grab it if null, its null because there is already a product on the license, so we did not have to create a snapshot for the license product
                snapshotLicenseProduct =
                    _snapshotLicenseProductRepository.GetSnapshotLicenseProductBySnapshotProductHeaderId(
                        snapshotProductHeaderId);
            }


            snapshotLicenseProduct.SnapshotProductHeaderId =
               snapshotProductHeaderId;

            snapshotLicenseProduct.ProductHeaderId =
                GetSnapshotCloneProductHeaderId(snapshotProductHeaderId);

            //update LicenseProduct
            _snapshotLicenseProductRepository.UpdateSnapshotLicenseProduct(snapshotLicenseProduct);
        }

        private void DeleteLicenseConfigurationFromSnapshot(UpdateLicenseProductConfigurationRequest request,
            LicenseProductConfiguration licenseProductConfiguration)
        {
            //get productHeaderId
          var snapshotProductHeaderId =_snapshotLicenseProductRepository.GetSnapshotProductHeaderId(licenseProductConfiguration.LicenseProductId);
            //get productHeader
            var productHeader =
                _snapshotProductHeaderRepository.GetSnapshotProductHeaderBySnapshotProductHeaderId(
                    snapshotProductHeaderId);



            _snapshotManager.DeleteRecsConfigAndChildrenForProductHeader(productHeader, (int)licenseProductConfiguration.product_configuration_id);


        }

        private void AddConfigToSnapshot(UpdateLicenseProductConfigurationRequest request,
            LicenseProductConfiguration licenseProductConfiguration)
        {
            //get productHeader
            var snapshotProductHeaderId = _snapshotLicenseProductRepository.GetSnapshotProductHeaderId(licenseProductConfiguration.LicenseProductId);
            
            //get productHeader
            var productHeader =
                _snapshotProductHeaderRepository.GetSnapshotProductHeaderBySnapshotProductHeaderId(
                    snapshotProductHeaderId);


            //Check if snapshot exists, if it does, exit
            if (CheckIfConfigExists(productHeader, (int) licenseProductConfiguration.product_configuration_id))
            {
                //config exists, no need to add it to snapshot Product header
                return;
            }
            else
            {
                //config does not exist, add it to snapshot product header


                //Build snapshot_config
                var newSnapshotConfiguration = new Snapshot_Configuration
                {
                    CloneConfigId = request.configuration_id,
                    Name = request.configuration_name,
                    Type = licenseProductConfiguration.ConfigurationType
                };

                var savedSnapshotConfig =
                    _snapshotConfigurationRepository.SaveSnapshotConfiguration(newSnapshotConfiguration);

                //build snapshotRecsConfig
                var newSnapshotConfig = new Snapshot_RecsConfiguration
                {
                    CloneRecsConfigurationId = (int)licenseProductConfiguration.product_configuration_id,
                    ConfigurationId = request.configuration_id,
                    DatabaseVersion = 1,
                    LicenseProductConfigurationId = licenseProductConfiguration.LicenseProductConfigurationId,
                    LicenseProductId = request.licenseProductId,
                    Name = null,
                    UPC = request.upc,
                    ProductHeaderId = productHeader.CloneProductHeaderId,
                    SnapshotProductHeaderId = snapshotProductHeaderId,
                    SnapshotConfigurationId = savedSnapshotConfig.SnapshotConfigId
                };

                _snapshotRecsConfigurationRepository.SaveSnapshotRecsConfiguration(newSnapshotConfig);
            }
        }

        //private Snapshot_Configuration createSnapshotConfiguration(UpdateLicenseProductConfigurationRequest request,
        //    LicenseProductConfiguration licenseProductConfiguration)
        //{
            
        //}



        /*
        private List<Snapshot_RecsConfiguration> CastToRecsConfigurationsSnapshot(
    List<RecsConfiguration> recsConfigurations, int productHeaderId)
        {
            var snapshotList = new List<Snapshot_RecsConfiguration>();

            foreach (var config in recsConfigurations)
            {
                var snapshot = new Snapshot_RecsConfiguration();
                snapshot.CloneRecsConfigurationId = (int)config.configuration_id;
                if (config.LicenseProductConfiguration != null)
                {
                    snapshot.LicenseProductId = config.LicenseProductConfiguration.LicenseProductId;
                }
                snapshot.ConfigurationId = (int)config.Configuration.ConfigId;
                snapshot.Configuration = CastToConfigurationSnapshot(config.Configuration);
                snapshot.ProductHeaderId = productHeaderId;
                snapshot.Name = config.name;
                snapshot.UPC = config.UPC;
                snapshot.ReleaseDate = config.ReleaseDate;
                snapshot.DatabaseVersion = config.DatabaseVersion;
                if (config.LicenseProductConfiguration != null)
                {
                    snapshot.LicenseProductConfigurationId =
                        config.LicenseProductConfiguration.LicenseProductConfigurationId;
                }
                //snapshot.LicenseProductConfiguration = config.LicenseProductConfiguration;  temp off
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }
        */
        private bool CheckIfConfigExists(Snapshot_ProductHeader productHeader, int configurationId)
        {
            //Check if snapshot exists, if it does, exit
            foreach (var config in productHeader.Configurations)
            {
                if (config.CloneRecsConfigurationId == configurationId)
                {
                    return true;
                }

            }
            return false;
        }

        private dynamic UpdateRecsProductHeader(ProductHeader productHeader, UpdateLicenseProductConfigurationRequest request)
        {
          
            dynamic artist = new ExpandoObject();
            artist.id = productHeader.Artist.id;
            artist.name = productHeader.Artist.name.ToString().Trim();

            dynamic product = new ExpandoObject();
            product.id = productHeader.Id;
            product.title = productHeader.Title.ToString().Trim();
            product.databaseVersion = productHeader.DatabaseVersion;
            product.artist = artist;

            if (productHeader.Label != null)
            {
                dynamic label = new ExpandoObject();
                label.id = productHeader.Label.label_id;
                label.name = productHeader.Label.name.ToString().Trim();
                product.label = label;
            }

            if (productHeader.Configurations != null)
            {
                var configs = UpdateExistingConfigs(productHeader, request);

                // add new config to recs if current
                if (request.product_configuration_id == 0)
                {
                    configs.AddRange(AddNewConfigToRecs(request));
                }
                product.configurations = configs;
            }
            else   //first productConfiguration
            {
                //new config here
            }

            return product;
        }

        private void UpdateLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request)
        {
            var licenseProductConfiguration = _licenseProductConfigurationRepository.Get(request.licenseProductConfigurationId); //Hit needed
            licenseProductConfiguration.CatalogNumber = request.catalogNumber;
            licenseProductConfiguration.ModifiedBy = 1;
            licenseProductConfiguration.ModifiedDate = DateTime.Now;
            _licenseProductConfigurationRepository.Update(licenseProductConfiguration);
        }

        private void SetUpdateLicenseProductConfigurationResult(UpdateLicenseProductConfigurationResult updateLicenseProductConfigurationResult,
            LicenseProductConfiguration licenseProductConfiguration, ProductHeader newProductHeader)
        {
            updateLicenseProductConfigurationResult.success = true;
            updateLicenseProductConfigurationResult.errorMessage = string.Empty;
            updateLicenseProductConfigurationResult.licenseProductConfiguration = licenseProductConfiguration;
            updateLicenseProductConfigurationResult.productHeader = newProductHeader;
        }

        private void HandleFailedUpdateLicenseProductConfigurationResult(
            UpdateLicenseProductConfigurationResult updateLicenseProductConfigurationResult,
            UpdateLicenseProductConfigurationRequest request, Exception ex)
        {
            updateLicenseProductConfigurationResult.success = false;
            updateLicenseProductConfigurationResult.errorMessage = ex.Message.ToString();

            //delete licenseProduct
            var mylicenseId = request.licenseId > 0 ? request.licenseId : request.addTolicenseId;
            var b = DeleteLicenseProduct(mylicenseId, request.productId);
        }
    }
}