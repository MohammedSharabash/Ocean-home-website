using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;

namespace Ocean_Home.Helper
{
    public class JwtToken
    {
        private IJwtEncoder encoder;
        private IJwtDecoder decoder;

        /// <remarks>
        /// This requires a key value randomly generated and stored in your configuration settings. 
        /// Consider that it is a good practice use keys as at least long as the output digest bytes 
        /// length produced by the hashing algorithm used. Since we use an HMAC-SHA-512 algorithm, 
        /// then we can provide it a key at least 64 bytes long.
        /// <see cref="https://tools.ietf.org/html/rfc4868#page-7"/>
        /// </remarks>
        public string SecretKey { get; set; }

        public JwtToken()
        {
            IJwtAlgorithm algorithm = new HMACSHA512Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider datetimeProvider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, datetimeProvider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

            encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            decoder = new JwtDecoder(serializer, validator, urlEncoder);
            SecretKey = "";
        }

        public JwtToken(string secretKey) : this()
        {
            SecretKey = secretKey;
        }

        public bool IsTokenValid(string token)
        {
            return !string.IsNullOrWhiteSpace(DecodeToken(token));
        }

        public string GetToken(object payload)
        {
            try
            {
                return encoder.Encode(payload, SecretKey);
            }
            catch (Exception)
            {
                return encoder.Encode(new DataModel(payload), SecretKey);
            }
        }

        public string DecodeToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token) || token == "null")
                {
                    return null;
                }
                return decoder.Decode(token, SecretKey, true);
            }
            catch (TokenExpiredException)
            {
                return null;
            }
            catch (SignatureVerificationException)
            {
                return null;
            }
        }

        public T DecodeToken<T>(string token) where T : class
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return null;
                }
                return decoder.DecodeToObject<T>(token, SecretKey, true);
            }
            catch (TokenExpiredException)
            {
                return null;
            }
            catch (SignatureVerificationException)
            {
                return null;
            }
            catch (Exception)
            {
                var data = decoder.DecodeToObject<DataModel>(token, SecretKey, true).Data;
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));
            }
        }
    }

    public class DataModel
    {
        public DataModel(object data)
        {
            Data = data;
        }
        public object Data { get; set; }
    }
}
