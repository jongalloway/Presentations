// <copyright>
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace BigShelf
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ServiceModel.DomainServices.Server;
    using System.Threading;

    /// <summary>
    /// This partial class contains test/demo code only, to make experimentation with
    /// the BigShelf sample easier. Code in this class wouldn't be part of an actual
    /// deployed service.
    /// </summary>
    public partial class BigShelfService
    {
        private int cudLatency = 500;
        private int queryLatency = 500;

        [Invoke]
        public void ResetData()
        {
            this.DbContext.Database.Initialize(true);
        }

        [Invoke]
        public string SetLatency(int queryDelay, int cudDelay)
        {
            if (queryDelay >= 0 && cudDelay >= 0)
            {
                queryLatency = queryDelay;
                cudLatency = cudDelay;
                return "Latency successfully set.";
            }
            else
            {
                return "Delay must be greater than or equal to 0";
            }
        }

        public override IEnumerable Query(QueryDescription queryDescription, out IEnumerable<ValidationResult> validationErrors, out int totalCount)
        {
            Thread.Sleep(queryLatency);
            return base.Query(queryDescription, out validationErrors, out totalCount);
        }

        protected override bool PersistChangeSet()
        {
            Thread.Sleep(cudLatency);
            return base.PersistChangeSet();
        }
    }
}