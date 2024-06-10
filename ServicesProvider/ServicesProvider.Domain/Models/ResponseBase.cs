namespace ServicesProvider.Domain.Models
{
    public class ResponseBase<TData>
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public TData Data { get; set; }

        public ResponseBase(int code, string description, TData data)
        {
            Code = code;
            Description = description;
            Data = data;
        }

        public ResponseBase(int code, string description) : this(code, description, default(TData))
        {
        }
    }


    public class ResponseBase
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public ResponseBase(int code, string description)
        {
            Code = code;
            Description = description;
        }
    }

}