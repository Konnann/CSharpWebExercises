namespace MyServer.Server.HTTP.Response
{
    using Enums;
    using Exceptions;
    using Server.Contracts;

    public class ViewResponse : HttpResponse
    {
        public ViewResponse(HttpStatusCode responseCode, IView view) : base(responseCode, view)
        { }

        private void ValidateStatusCode(HttpStatusCode statusCode)
        {
            var statusCodeNumber = (int)statusCode;

            if(299 < statusCodeNumber && statusCodeNumber < 400)
            {
                throw new InvalidResponseException("View Responses need a status code below 300.");
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}{this.view.View()}";
        }
    }
}
