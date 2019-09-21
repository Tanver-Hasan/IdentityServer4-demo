// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("http://dev.product.com", "Product")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    ClientId="application1",
                    ClientName="application1",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedScopes= { "http://dev.product.com" }
                },
                new Client
                {
                    AllowedGrantTypes= GrantTypes.Implicit,
                    ClientId="spa-implicit",
                    ClientName="spa",
                    ClientSecrets={ new Secret("appsecret".Sha256())},
                    RedirectUris= { "http://localhost:4200"},
                    AllowedScopes = new List<string>
                      {
                       IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.StandardScopes.Profile
                    },
                    PostLogoutRedirectUris= {"http://localhost:4200/logout"},
                    AllowAccessTokensViaBrowser=true,

                },
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:5003/callback.html","http://localhost:4200/callback" },

                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003","http://localhost:4200" },


                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "http://dev.product.com"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
             {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",

                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
        }
    }
}