
using System.Text.Json.Serialization;

namespace LyncasSales.Domain.DTO.Response
{
    public class UserLoginResponse
    {
        public bool Sucesso => Erros.Count == 0;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AccessToken { get; private set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefreshToken { get; private set; }

        public List<string> Erros { get; private set; }

        public UserLoginResponse() =>
            Erros = new List<string>();

        public UserLoginResponse(bool sucess, string accessToken, string refreshToken) : this()
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public void AddError(string erro) =>
            Erros.Add(erro);

        public void AddError(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
