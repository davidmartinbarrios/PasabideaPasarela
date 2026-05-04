namespace Lantik.Pasarela.Application.DTOs
{
    public class ResponseBaseDTO<T>
    {
        public T Data { get; set; }
        public Query_ResponseDTO Query_Result { get; set; }

        public ResponseBaseDTO()
        {
            Query_Result = new Query_ResponseDTO();
        }

        public ResponseBaseDTO(T data, Entities.POCOs.Query_Response result)
        {
            this.Data = data;

            this.Query_Result = new Query_ResponseDTO();
            this.Query_Result.ParseResponse(result);
        }
    }
}
