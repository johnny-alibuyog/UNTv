namespace UNTv.WP81.DataProviders.Contracts.Messages
{
    public abstract class MessageRequestBase<>
    {
        public virtual string QueryString { get; set; }
        public abstract string LocalPersitenceFilename { get; }
    }
}
