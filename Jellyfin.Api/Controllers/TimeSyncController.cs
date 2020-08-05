﻿using System;
using System.Globalization;
using MediaBrowser.Model.SyncPlay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jellyfin.Api.Controllers
{
    /// <summary>
    /// The time sync controller.
    /// </summary>
    [Route("GetUtcTime")]
    public class TimeSyncController : BaseJellyfinApiController
    {
        /// <summary>
        /// Gets the current utc time.
        /// </summary>
        /// <response code="200">Time returned.</response>
        /// <returns>An <see cref="UtcTimeResponse"/> to sync the client and server time.</returns>
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public ActionResult<UtcTimeResponse> GetUtcTime()
        {
            // Important to keep the following line at the beginning
            var requestReceptionTime = DateTime.UtcNow.ToUniversalTime().ToString("o", DateTimeFormatInfo.InvariantInfo);

            var response = new UtcTimeResponse();
            response.RequestReceptionTime = requestReceptionTime;

            // Important to keep the following two lines at the end
            var responseTransmissionTime = DateTime.UtcNow.ToUniversalTime().ToString("o", DateTimeFormatInfo.InvariantInfo);
            response.ResponseTransmissionTime = responseTransmissionTime;

            // Implementing NTP on such a high level results in this useless
            // information being sent. On the other hand it enables future additions.
            return response;
        }
    }
}