﻿//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 12.0
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

using System;
using System.Collections.Generic;
using Microsoft.BingAds.V12.Internal.Bulk;
using Microsoft.BingAds.V12.Internal.Bulk.Mappings;
using Microsoft.BingAds.V12.Internal.Bulk.Entities;
using Microsoft.BingAds.V12.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V12.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a responsive search ad. 
    /// This class exposes the <see cref="BulkResponsiveSearchAd.ResponsiveSearchAd"/> property that can be read and written as fields of the Responsive Search Ad record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Responsive Search Ad</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkResponsiveSearchAd : BulkAd<ResponsiveSearchAd>
    {
        /// <summary>
        /// <para>
        /// The responsive search ad. 
        /// </para>
        /// </summary>
        public ResponsiveSearchAd ResponsiveSearchAd
        {
            get { return Ad; }
            set { Ad = value; }
        }

        private static readonly IBulkMapping<BulkResponsiveSearchAd>[] Mappings =
        {
            new SimpleBulkMapping<BulkResponsiveSearchAd>(StringTable.Description,
                c => c.ResponsiveSearchAd.Descriptions == null ? null : c.ResponsiveSearchAd.Descriptions.ToTextAssetLinksBulkString(),
                (v, c) => c.ResponsiveSearchAd.Descriptions = v.ParseTextAssetLinks()
            ),

            new SimpleBulkMapping<BulkResponsiveSearchAd>(StringTable.Headline,
                c => c.ResponsiveSearchAd.Headlines == null ? null : c.ResponsiveSearchAd.Headlines.ToTextAssetLinksBulkString(),
                (v, c) => c.ResponsiveSearchAd.Headlines = v.ParseTextAssetLinks()
            ),

            new SimpleBulkMapping<BulkResponsiveSearchAd>(StringTable.Path1,
                c => c.ResponsiveSearchAd.Path1.ToOptionalBulkString(),
                (v, c) => c.ResponsiveSearchAd.Path1 = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkResponsiveSearchAd>(StringTable.Path2,
                c => c.ResponsiveSearchAd.Path2.ToOptionalBulkString(),
                (v, c) => c.ResponsiveSearchAd.Path2 = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkResponsiveSearchAd>(StringTable.Domain,
                c => c.ResponsiveSearchAd.Domain.ToOptionalBulkString(),
                (v, c) => c.ResponsiveSearchAd.Domain = v.GetValueOrEmptyString()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            ResponsiveSearchAd = new ResponsiveSearchAd { Type = AdType.ResponsiveSearch };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }


        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(ResponsiveSearchAd, "ResponsiveSearchAd");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
