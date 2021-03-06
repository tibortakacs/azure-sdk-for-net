// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Analytics.Synapse.AccessControl.Models;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Analytics.Synapse.AccessControl
{
    internal partial class AccessControlRestClient
    {
        private string endpoint;
        private string apiVersion;
        private ClientDiagnostics _clientDiagnostics;
        private HttpPipeline _pipeline;

        /// <summary> Initializes a new instance of AccessControlRestClient. </summary>
        /// <param name="clientDiagnostics"> The handler for diagnostic messaging in the client. </param>
        /// <param name="pipeline"> The HTTP pipeline for sending and receiving REST requests and responses. </param>
        /// <param name="endpoint"> The workspace development endpoint, for example https://myworkspace.dev.azuresynapse.net. </param>
        /// <param name="apiVersion"> Api Version. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="endpoint"/> or <paramref name="apiVersion"/> is null. </exception>
        public AccessControlRestClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string endpoint, string apiVersion = "2020-02-01-preview")
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }
            if (apiVersion == null)
            {
                throw new ArgumentNullException(nameof(apiVersion));
            }

            this.endpoint = endpoint;
            this.apiVersion = apiVersion;
            _clientDiagnostics = clientDiagnostics;
            _pipeline = pipeline;
        }

        internal HttpMessage CreateCreateRoleAssignmentRequest(RoleAssignmentOptions createRoleAssignmentOptions)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Post;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(endpoint, false);
            uri.AppendPath("/rbac/roleAssignments", false);
            uri.AppendQuery("api-version", apiVersion, true);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteObjectValue(createRoleAssignmentOptions);
            request.Content = content;
            return message;
        }

        /// <summary> Create role assignment. </summary>
        /// <param name="createRoleAssignmentOptions"> Details of role id and object id. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="createRoleAssignmentOptions"/> is null. </exception>
        public async Task<Response<RoleAssignmentDetails>> CreateRoleAssignmentAsync(RoleAssignmentOptions createRoleAssignmentOptions, CancellationToken cancellationToken = default)
        {
            if (createRoleAssignmentOptions == null)
            {
                throw new ArgumentNullException(nameof(createRoleAssignmentOptions));
            }

            using var message = CreateCreateRoleAssignmentRequest(createRoleAssignmentOptions);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        RoleAssignmentDetails value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = RoleAssignmentDetails.DeserializeRoleAssignmentDetails(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Create role assignment. </summary>
        /// <param name="createRoleAssignmentOptions"> Details of role id and object id. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="createRoleAssignmentOptions"/> is null. </exception>
        public Response<RoleAssignmentDetails> CreateRoleAssignment(RoleAssignmentOptions createRoleAssignmentOptions, CancellationToken cancellationToken = default)
        {
            if (createRoleAssignmentOptions == null)
            {
                throw new ArgumentNullException(nameof(createRoleAssignmentOptions));
            }

            using var message = CreateCreateRoleAssignmentRequest(createRoleAssignmentOptions);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        RoleAssignmentDetails value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = RoleAssignmentDetails.DeserializeRoleAssignmentDetails(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateGetRoleAssignmentsRequest(string roleId, string principalId, string continuationToken)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(endpoint, false);
            uri.AppendPath("/rbac/roleAssignments", false);
            uri.AppendQuery("api-version", apiVersion, true);
            if (roleId != null)
            {
                uri.AppendQuery("roleId", roleId, true);
            }
            if (principalId != null)
            {
                uri.AppendQuery("principalId", principalId, true);
            }
            request.Uri = uri;
            if (continuationToken != null)
            {
                request.Headers.Add("x-ms-continuation", continuationToken);
            }
            return message;
        }

        /// <summary> List role assignments. </summary>
        /// <param name="roleId"> Synapse Built-In Role Id. </param>
        /// <param name="principalId"> Object ID of the AAD principal or security-group. </param>
        /// <param name="continuationToken"> Continuation token. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async Task<ResponseWithHeaders<IReadOnlyList<RoleAssignmentDetails>, AccessControlGetRoleAssignmentsHeaders>> GetRoleAssignmentsAsync(string roleId = null, string principalId = null, string continuationToken = null, CancellationToken cancellationToken = default)
        {
            using var message = CreateGetRoleAssignmentsRequest(roleId, principalId, continuationToken);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            var headers = new AccessControlGetRoleAssignmentsHeaders(message.Response);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        IReadOnlyList<RoleAssignmentDetails> value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        List<RoleAssignmentDetails> array = new List<RoleAssignmentDetails>();
                        foreach (var item in document.RootElement.EnumerateArray())
                        {
                            array.Add(RoleAssignmentDetails.DeserializeRoleAssignmentDetails(item));
                        }
                        value = array;
                        return ResponseWithHeaders.FromValue(value, headers, message.Response);
                    }
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> List role assignments. </summary>
        /// <param name="roleId"> Synapse Built-In Role Id. </param>
        /// <param name="principalId"> Object ID of the AAD principal or security-group. </param>
        /// <param name="continuationToken"> Continuation token. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<IReadOnlyList<RoleAssignmentDetails>, AccessControlGetRoleAssignmentsHeaders> GetRoleAssignments(string roleId = null, string principalId = null, string continuationToken = null, CancellationToken cancellationToken = default)
        {
            using var message = CreateGetRoleAssignmentsRequest(roleId, principalId, continuationToken);
            _pipeline.Send(message, cancellationToken);
            var headers = new AccessControlGetRoleAssignmentsHeaders(message.Response);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        IReadOnlyList<RoleAssignmentDetails> value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        List<RoleAssignmentDetails> array = new List<RoleAssignmentDetails>();
                        foreach (var item in document.RootElement.EnumerateArray())
                        {
                            array.Add(RoleAssignmentDetails.DeserializeRoleAssignmentDetails(item));
                        }
                        value = array;
                        return ResponseWithHeaders.FromValue(value, headers, message.Response);
                    }
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateGetRoleAssignmentByIdRequest(string roleAssignmentId)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(endpoint, false);
            uri.AppendPath("/rbac/roleAssignments/", false);
            uri.AppendPath(roleAssignmentId, true);
            uri.AppendQuery("api-version", apiVersion, true);
            request.Uri = uri;
            return message;
        }

        /// <summary> Get role assignment by role assignment Id. </summary>
        /// <param name="roleAssignmentId"> The ID of the role assignment. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="roleAssignmentId"/> is null. </exception>
        public async Task<Response<RoleAssignmentDetails>> GetRoleAssignmentByIdAsync(string roleAssignmentId, CancellationToken cancellationToken = default)
        {
            if (roleAssignmentId == null)
            {
                throw new ArgumentNullException(nameof(roleAssignmentId));
            }

            using var message = CreateGetRoleAssignmentByIdRequest(roleAssignmentId);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        RoleAssignmentDetails value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = RoleAssignmentDetails.DeserializeRoleAssignmentDetails(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Get role assignment by role assignment Id. </summary>
        /// <param name="roleAssignmentId"> The ID of the role assignment. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="roleAssignmentId"/> is null. </exception>
        public Response<RoleAssignmentDetails> GetRoleAssignmentById(string roleAssignmentId, CancellationToken cancellationToken = default)
        {
            if (roleAssignmentId == null)
            {
                throw new ArgumentNullException(nameof(roleAssignmentId));
            }

            using var message = CreateGetRoleAssignmentByIdRequest(roleAssignmentId);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        RoleAssignmentDetails value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = RoleAssignmentDetails.DeserializeRoleAssignmentDetails(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateDeleteRoleAssignmentByIdRequest(string roleAssignmentId)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Delete;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(endpoint, false);
            uri.AppendPath("/rbac/roleAssignments/", false);
            uri.AppendPath(roleAssignmentId, true);
            uri.AppendQuery("api-version", apiVersion, true);
            request.Uri = uri;
            return message;
        }

        /// <summary> Delete role assignment by role assignment Id. </summary>
        /// <param name="roleAssignmentId"> The ID of the role assignment. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="roleAssignmentId"/> is null. </exception>
        public async Task<Response> DeleteRoleAssignmentByIdAsync(string roleAssignmentId, CancellationToken cancellationToken = default)
        {
            if (roleAssignmentId == null)
            {
                throw new ArgumentNullException(nameof(roleAssignmentId));
            }

            using var message = CreateDeleteRoleAssignmentByIdRequest(roleAssignmentId);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                case 204:
                    return message.Response;
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Delete role assignment by role assignment Id. </summary>
        /// <param name="roleAssignmentId"> The ID of the role assignment. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="roleAssignmentId"/> is null. </exception>
        public Response DeleteRoleAssignmentById(string roleAssignmentId, CancellationToken cancellationToken = default)
        {
            if (roleAssignmentId == null)
            {
                throw new ArgumentNullException(nameof(roleAssignmentId));
            }

            using var message = CreateDeleteRoleAssignmentByIdRequest(roleAssignmentId);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                case 204:
                    return message.Response;
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateGetCallerRoleAssignmentsRequest()
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Post;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(endpoint, false);
            uri.AppendPath("/rbac/getMyAssignedRoles", false);
            uri.AppendQuery("api-version", apiVersion, true);
            request.Uri = uri;
            return message;
        }

        /// <summary> List role assignments of the caller. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async Task<Response<IReadOnlyList<string>>> GetCallerRoleAssignmentsAsync(CancellationToken cancellationToken = default)
        {
            using var message = CreateGetCallerRoleAssignmentsRequest();
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        IReadOnlyList<string> value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        List<string> array = new List<string>();
                        foreach (var item in document.RootElement.EnumerateArray())
                        {
                            array.Add(item.GetString());
                        }
                        value = array;
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> List role assignments of the caller. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IReadOnlyList<string>> GetCallerRoleAssignments(CancellationToken cancellationToken = default)
        {
            using var message = CreateGetCallerRoleAssignmentsRequest();
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        IReadOnlyList<string> value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        List<string> array = new List<string>();
                        foreach (var item in document.RootElement.EnumerateArray())
                        {
                            array.Add(item.GetString());
                        }
                        value = array;
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateGetRoleDefinitionsRequest()
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(endpoint, false);
            uri.AppendPath("/rbac/roles", false);
            uri.AppendQuery("api-version", apiVersion, true);
            request.Uri = uri;
            return message;
        }

        /// <summary> List roles. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async Task<Response<RolesListResponse>> GetRoleDefinitionsAsync(CancellationToken cancellationToken = default)
        {
            using var message = CreateGetRoleDefinitionsRequest();
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        RolesListResponse value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = RolesListResponse.DeserializeRolesListResponse(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> List roles. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<RolesListResponse> GetRoleDefinitions(CancellationToken cancellationToken = default)
        {
            using var message = CreateGetRoleDefinitionsRequest();
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        RolesListResponse value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = RolesListResponse.DeserializeRolesListResponse(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateGetRoleDefinitionByIdRequest(string roleId)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(endpoint, false);
            uri.AppendPath("/rbac/roles/", false);
            uri.AppendPath(roleId, true);
            uri.AppendQuery("api-version", apiVersion, true);
            request.Uri = uri;
            return message;
        }

        /// <summary> Get role by role Id. </summary>
        /// <param name="roleId"> Synapse Built-In Role Id. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="roleId"/> is null. </exception>
        public async Task<Response<SynapseRole>> GetRoleDefinitionByIdAsync(string roleId, CancellationToken cancellationToken = default)
        {
            if (roleId == null)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            using var message = CreateGetRoleDefinitionByIdRequest(roleId);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        SynapseRole value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = SynapseRole.DeserializeSynapseRole(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Get role by role Id. </summary>
        /// <param name="roleId"> Synapse Built-In Role Id. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="roleId"/> is null. </exception>
        public Response<SynapseRole> GetRoleDefinitionById(string roleId, CancellationToken cancellationToken = default)
        {
            if (roleId == null)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            using var message = CreateGetRoleDefinitionByIdRequest(roleId);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        SynapseRole value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = SynapseRole.DeserializeSynapseRole(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateGetRoleDefinitionsNextPageRequest(string nextLink)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(endpoint, false);
            uri.AppendRawNextLink(nextLink, false);
            request.Uri = uri;
            return message;
        }

        /// <summary> List roles. </summary>
        /// <param name="nextLink"> The URL to the next page of results. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="nextLink"/> is null. </exception>
        public async Task<Response<RolesListResponse>> GetRoleDefinitionsNextPageAsync(string nextLink, CancellationToken cancellationToken = default)
        {
            if (nextLink == null)
            {
                throw new ArgumentNullException(nameof(nextLink));
            }

            using var message = CreateGetRoleDefinitionsNextPageRequest(nextLink);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        RolesListResponse value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = RolesListResponse.DeserializeRolesListResponse(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> List roles. </summary>
        /// <param name="nextLink"> The URL to the next page of results. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="nextLink"/> is null. </exception>
        public Response<RolesListResponse> GetRoleDefinitionsNextPage(string nextLink, CancellationToken cancellationToken = default)
        {
            if (nextLink == null)
            {
                throw new ArgumentNullException(nameof(nextLink));
            }

            using var message = CreateGetRoleDefinitionsNextPageRequest(nextLink);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        RolesListResponse value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = RolesListResponse.DeserializeRolesListResponse(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }
    }
}
