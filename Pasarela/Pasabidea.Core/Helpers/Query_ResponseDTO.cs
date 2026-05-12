namespace Lantik.Pasabidea.Data
{
    public class Query_ResponseDTO
    {
        public int RC { get; set; }
        public int SQLCode { get; set; }
        public string SQLState { get; set; }
        public string SQLMessage { get; set; }

        public Query_ResponseDTO()
        {
            RC = 0;
            SQLCode = 0;
            SQLState = "";
            SQLMessage = "";
        }
        public void ParseResponse(int rc, int sqlcode, string sqlstate, string sqlmessage)
        {
            RC = rc;
            SQLCode = sqlcode;
            SQLState = sqlstate;
            SQLMessage = sqlmessage;
        }
        //public void ParseResponse(Entities.POCOs.Query_Response obj)
        //{
        //    this.RC = obj.RC;
        //    this.SQLCode = obj.SQLCode;
        //    this.SQLState = obj.SQLState;
        //    this.SQLMessage = obj.SQLMessage;
        //}
    }
}
