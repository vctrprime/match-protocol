// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Test;

namespace MatchProtocol.IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "match-protocol-web-client",
                    ClientName = "Match Protocol WEB UI",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:9001/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:9001/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "games.service",
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = "games.service",
                    ClientName = "Match Protocol Games Service",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("games.service-secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        "get-weather.game-settings.service",
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("get-weather.game-settings.service", "GameSettings Service. Get Weather"),
                new ApiScope("games.service", "Games Service")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                //new ApiResource("movieAPI", "Movie API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "demo",
                    Password = "demo",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "demo"),
                        new Claim(JwtClaimTypes.FamilyName, "demo")
                    }
                }

            };
    }

}