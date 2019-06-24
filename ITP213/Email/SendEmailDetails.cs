using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.Email
{
    /// <summary>
    /// The details about the email to send
    /// </summary>
    public class SendEmailDetails
    {
        /// <summary>
        /// The name of the sender
        /// </summary>
        public string FromName { get; set; }
        /// <summary>
        /// The email of the sender
        /// </summary>
        public string FromEmail { get; set; }
        /// <summary>
        /// The name of the receiver
        /// </summary>
        public string ToName { get; set; }
        /// <summary>
        /// The email of the receiver
        /// </summary>
        public string ToEmail { get; set; }
        /// <summary>
        /// The email subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The email body content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Indicates if the contents if a HTML email
        /// </summary>
        public bool IsHTML { get; set; }
    }
}