﻿using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.ViewModel.Auth;

namespace Soteria.Web.Auth
{
    public static class JwtManager
    {
        public static string GenerateToken(SessionToken sessionToken)
        {
            string Secret = ConfigHelper.GetAppSettingByKey("JWTTokenSecret");
            string tokenExpirationText = ConfigHelper.GetAppSettingByKey("JWTTokenExpiration");
            var Payload = new Dictionary<string, object>
            {
                { "UserSession", sessionToken }
            };
            IJwtAlgorithm algorithm = new JWT.Algorithms.HMACSHA256Algorithm();
            IJsonSerializer serializer = new JWT.Serializers.JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(Payload, Secret);
        }
        public static SessionToken GetSessionToken(string token)
        {
            try
            {
                string Secret = ConfigHelper.GetAppSettingByKey("JWTTokenSecret");
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                IDictionary<string, object> payload = decoder.DecodeToObject<IDictionary<string, object>>(token, Encoding.ASCII.GetBytes(Secret), true);
                JObject obj = (JObject)payload["UserSession"];
                return obj.ToObject<SessionToken>();
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");

            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
            return null;
        }
    }
}