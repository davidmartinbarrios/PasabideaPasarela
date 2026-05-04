namespace Lantik.Pasarela.Application.DTOs
{
    public class Query_ResponseDTO
    {
            public int RC { get; set; }
            public int SQLCode { get; set; }
            public string SQLState { get; set; }
            public string SQLMessage { get; set; }

            public Query_ResponseDTO()
            {
                this.RC = 0;
                this.SQLCode = 0;
                this.SQLState = "";
                this.SQLMessage = "";
            }
            public void ParseResponse(int rc, int sqlcode, string sqlstate, string sqlmessage)
            {
                this.RC = rc;
                this.SQLCode = sqlcode;
                this.SQLState = sqlstate;
                this.SQLMessage = sqlmessage;
            }
            public void ParseResponse(Entities.POCOs.Query_Response obj)
            {
                this.RC = obj.RC;
                this.SQLCode = obj.SQLCode;
                this.SQLState = obj.SQLState;
                this.SQLMessage = obj.SQLMessage;
            }
        }
}
